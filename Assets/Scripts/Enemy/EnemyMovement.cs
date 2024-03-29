using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;
    private ControlPlayerAwareness _controlPlayerAwareness;
    public Vector2 targetDirection {get; private set;}
    private Animator _animator;

    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
        _controlPlayerAwareness = this.GetComponent<ControlPlayerAwareness>();
    }

    private void FixedUpdate() {
        GameUtils.MoveAnimations(_rigidBody, _animator);
        GameUtils.FlippingCharacterOnMove(_rigidBody);
        UpdateTragetDirection();
        SetEnemyVelocity();
    }

    private void UpdateTragetDirection() {
        if (_controlPlayerAwareness.AwareOfPlayer) {
            targetDirection = _controlPlayerAwareness.DirectionToPlayer;
        } else {
            RandomSeek();
            targetDirection = Vector2.zero;
        }
    }

    private void RandomSeek() {
        if (UnityEngine.Random.Range(0,50) == 0) 
                transform.localScale = GameUtils.Flip_x(transform.localScale);
    }

    private void SetEnemyVelocity() {
        if (targetDirection == Vector2.zero) {
            _rigidBody.velocity = Vector2.zero;
        } else {
            _rigidBody.velocity = targetDirection * _speed;
        }
    }

}
