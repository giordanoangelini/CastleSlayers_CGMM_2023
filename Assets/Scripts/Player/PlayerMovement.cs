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
    
    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        GameUtils.FlippingCharacterOnAim(_rigidBody);
        GameUtils.MoveAnimations(_rigidBody, _animator);
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
