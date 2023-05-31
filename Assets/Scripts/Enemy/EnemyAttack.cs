using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private GameObject _blastPrefab;
    [SerializeField] private float _blastSpeed;
    [SerializeField] private float _bodyOffset;
    public enum AttackType {fight, shoot};
    [SerializeField] private AttackType _attackType;
    private Rigidbody2D _rigidBody;
    private ControlPlayerAwareness _controlPlayerAwareness;
    private Vector2 _targetDirection;
    private Animator _animator;
    private float _lastFireTime;
    private bool _dead = false;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = _rigidBody.GetComponent<Animator>();
        _controlPlayerAwareness = GetComponent<ControlPlayerAwareness>();
        _lastFireTime = Time.time;
    }

    private void Update() {
        CheckAttack();
    }

    private bool CanAttack() {
        float timeSinceLastFire = Time.time - _lastFireTime;
        if (timeSinceLastFire >= _timeBetweenAttacks && !_dead) return true;
        return false;
    }

    private void Attack() {
        if (_controlPlayerAwareness.AttackPlayer) {
            _animator.SetTrigger("attack");
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(
                new Vector2(transform.position.x, transform.position.y - _bodyOffset),
                _attackRange
            );
            foreach (Collider2D player in hitPlayers) {
                if (player.name.ToLower().Contains("player")) {
                    Debug.Log("preso");
                    //player.GetComponent<PlayerAttack>().PlayerDeath();
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - _bodyOffset), _attackRange);
    }
    
    private void FireBlast() {
        if (_controlPlayerAwareness.ShootPlayer) {
            _lastFireTime = Time.time;
            GameObject bullet = Instantiate(_blastPrefab, transform.position + new Vector3(0,-_bodyOffset,0), new Quaternion());
            bullet.GetComponent<Rigidbody2D>().velocity = _blastSpeed * GetComponent<EnemyMovement>().targetDirection;
        }
    }

    private void CheckAttack() {
        switch (_attackType) {
            case AttackType.fight:
                Attack();
                break;
            case AttackType.shoot:
                if (CanAttack()) FireBlast();
                break;
            default: break;
        }
    }

    public void EnemyDeath() {
        _dead = true;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GameUtils.DieAnimations(gameObject, GetComponent<Animator>());
        Destroy(gameObject, 2f);
    }
}
