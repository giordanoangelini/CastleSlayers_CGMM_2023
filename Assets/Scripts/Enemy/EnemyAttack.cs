using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private GameObject _blastPrefab;
    [SerializeField] private float _blastSpeed;
    public enum AttackType {fight, shoot};
    [SerializeField] private AttackType _attackType;
    private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _center;
    private ControlPlayerAwareness _controlPlayerAwareness;
    private Animator _animator;
    private float _lastFireTime;
    private bool _dead = false;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controlPlayerAwareness = GetComponent<ControlPlayerAwareness>();
        _lastFireTime = Time.time;
    }

    private void FixedUpdate() {
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
                _center.position,
                _attackRange
            );
            foreach (Collider2D coll in hitPlayers) {
                if (coll.tag == "player") {
                    coll.GetComponent<PlayerAttack>().PlayerDeath();
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_center.position, _attackRange);
    }
    
    private void FireBlast() {
        if (_controlPlayerAwareness.ShootPlayer) {
            _lastFireTime = Time.time;
            GameObject bullet = Instantiate(_blastPrefab, _center.position, new Quaternion());
            bullet.GetComponent<Rigidbody2D>().velocity = _blastSpeed * _controlPlayerAwareness.DirectionToShoot;
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
        GameUtils.DeathAnimation(gameObject, GetComponent<Animator>());
        Destroy(gameObject, 2f);
    }
}
