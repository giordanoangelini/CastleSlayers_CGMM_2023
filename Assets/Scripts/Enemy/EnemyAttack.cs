using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    private Rigidbody2D _rigidBody;
    private ControlPlayerAwareness _controlPlayerAwareness;
    private Vector2 _targetDirection;
    private Animator _animator;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = _rigidBody.GetComponent<Animator>();
        _controlPlayerAwareness = GetComponent<ControlPlayerAwareness>();
    }

    private void Update() {
        Attack();
    }

    private void Attack() {
        if (_controlPlayerAwareness.AttackPlayer) {
            Collider2D[] hitPlayers = Physics2D.OverlapAreaAll(new Vector2(transform.position.x, transform.position.y - _attackRange), GameUtils.AttackArea(transform.localScale.x, transform.position.x, transform.position.y, _attackRange));
            foreach (Collider2D player in hitPlayers) {
                if (player.name.ToLower().Contains("player")) {

                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    player.transform.Find("Hands").gameObject.SetActive(false);
                    GameUtils.DieAnimations(player.gameObject, player.GetComponent<Animator>());
                    Destroy(player.gameObject, 5f);   
                }
            }
        }
    }
}
