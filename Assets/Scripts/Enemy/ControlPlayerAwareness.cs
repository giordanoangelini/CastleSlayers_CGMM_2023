using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    public bool AttackPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}
    [SerializeField] private float _playerAwarenessDistance;
    [SerializeField] private float _playerAttackDistance;
    private Transform _player;

    private void Awake() {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update() {
        if (_player && _player.GetComponent<PlayerMovement>().enabled) {
            Vector2 enemyToPlayerDistance = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerDistance.normalized;

            if (enemyToPlayerDistance.magnitude <= _playerAwarenessDistance) AwareOfPlayer = true;
            else AwareOfPlayer = false;

            if (enemyToPlayerDistance.magnitude <= _playerAttackDistance) AttackPlayer = true;
            else AttackPlayer = false;
        } else {
            AwareOfPlayer = false;
            AttackPlayer = false;
        }
    }
}
