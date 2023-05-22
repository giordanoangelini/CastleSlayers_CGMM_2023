using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    private float _inputHorizontal;
    private float _inputVertical;
    private Vector3 _currentScale;

    private Animator _animator;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = _rigidBody.GetComponent<Animator>();
    }

    private void FixedUpdate() {

        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");

        MoveAnimations(_inputHorizontal, _inputVertical);

        _currentScale = _rigidBody.transform.localScale;

        if (_inputHorizontal * _currentScale.x < 0) {
            _rigidBody.transform.localScale = new Vector3(-_currentScale.x, _currentScale.y, _currentScale.z);
        }

        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f
        );
        _rigidBody.velocity = _smoothedMovementInput * _speed;
    }

    private void MoveAnimations(float _inputHorizontal, float _inputVertical) {
        if (_inputHorizontal != 0 || _inputVertical != 0) {
            _animator.ResetTrigger("stop");
            _animator.SetTrigger("moving");
        }
        else {
            _animator.ResetTrigger("moving");
            _animator.SetTrigger("stop");
        }
    }

    private void OnMove(InputValue inputValue) {
        _movementInput =  inputValue.Get<Vector2>();
    }
}
