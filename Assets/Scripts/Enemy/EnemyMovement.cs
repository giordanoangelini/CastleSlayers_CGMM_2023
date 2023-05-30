using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
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
        GameUtils.MoveAnimations(_rigidBody, _animator);
        GameUtils.FlippingCharacterOnMove(_rigidBody);
        UpdateTragetDirection();
        SetEnemyVelocity();
    }

    private void UpdateTragetDirection() {
        if (_controlPlayerAwareness.AwareOfPlayer) {
            _targetDirection = _controlPlayerAwareness.DirectionToPlayer;
        } else {
            _targetDirection = Vector2.zero;
        }
    }

    private void SetEnemyVelocity() {
        if (_targetDirection == Vector2.zero) {
            _rigidBody.velocity = Vector2.zero;
        } else {
            _rigidBody.velocity = _targetDirection * _speed;
        }
    }

}
