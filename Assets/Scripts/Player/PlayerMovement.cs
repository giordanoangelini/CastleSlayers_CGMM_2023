using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _walkSound;
    public float lastSoundTime {get; set;}
    private float _epsilon = 0.1f;
    private float _timeBetweenSound = 0.3f;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Animator _animator;
    
    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        lastSoundTime = Time.time;
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
        Debug.Log(_rigidBody.velocity.x);
        Debug.Log(_rigidBody.velocity.y);
        if((Math.Abs(_rigidBody.velocity.x) > _epsilon || Math.Abs(_rigidBody.velocity.y) > _epsilon) && CanPlaySound()){
            _walkSound.Play();
            lastSoundTime = Time.time;
        }
    }

    private bool CanPlaySound() {
        float timeSinceLastSound = Time.time - lastSoundTime;
        if (timeSinceLastSound >= _timeBetweenSound) return true;
        return false;
    }

    private void OnMove(InputValue inputValue) {
        _movementInput =  inputValue.Get<Vector2>();
    }
}
