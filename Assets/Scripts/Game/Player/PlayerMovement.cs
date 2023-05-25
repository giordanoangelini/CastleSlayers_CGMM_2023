using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Animator _animator;
    private Camera _camera;
    private AnimationController _animationController;

    private void Awake() {
        // Player Rigidbody2D
        _rigidBody = GetComponent<Rigidbody2D>();

        // Player animations
        _animator = _rigidBody.GetComponent<Animator>();
        _animationController = GetComponent<AnimationController>();

        // Following camera
        _camera = Camera.main;
    }

    private void FixedUpdate() {
        //_animationController.FlippingCharacterOnMove(_rigidBody);
        _animationController.FlippingCharacterOnAim(_rigidBody);
        _animationController.MoveAnimations(_rigidBody, _animator);
        SetPlayerVelocity();
    }

    private void SetPlayerVelocity() {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f
        );
        _rigidBody.velocity = _smoothedMovementInput * _speed;
    }

    private void OnMove(InputValue inputValue) {
        _movementInput =  inputValue.Get<Vector2>();
    }
}
