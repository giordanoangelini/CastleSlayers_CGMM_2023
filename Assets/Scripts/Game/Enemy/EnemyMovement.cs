using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;
    private ControlPlayerAwareness _controlPlayerAwareness;
    private Vector2 _targetDirection;
    private Vector2 _movement;
    private Vector2 _smoothedMovement;
    private AnimationController _animationController;
    private Vector2 _movementSmoothVelocity;
    private Animator _animator;
    private Camera _camera;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = _rigidBody.GetComponent<Animator>();
        _animationController = GetComponent<AnimationController>();
        _controlPlayerAwareness = GetComponent<ControlPlayerAwareness>();
        _camera = Camera.main;
    }

    private void FixedUpdate() {
        _animationController.MoveAnimations(_rigidBody, _animator);
        _animationController.FlippingCharacter(_rigidBody);
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
