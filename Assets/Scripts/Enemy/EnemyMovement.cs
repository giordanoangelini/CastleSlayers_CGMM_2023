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
    private float _bodyOffset;

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = _rigidBody.GetComponent<Animator>();
        _controlPlayerAwareness = GetComponent<ControlPlayerAwareness>();
        _bodyOffset = GetComponent<EnemyAttack>().bodyOffset;
    }

    private void Update() {
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
