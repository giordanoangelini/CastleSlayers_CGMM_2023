using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    public bool AttackPlayer {get; private set;}
    public bool ShootPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}
    [SerializeField] private float _playerAwarenessDistance;
    [SerializeField] private float _playerShootDistance;
    [SerializeField] private float _playerAttackDistance;
    private float _bodyOffset;
    private Transform _player;

    private void Awake() {
        _player = FindObjectOfType<PlayerMovement>().transform;
        _bodyOffset = GetComponent<EnemyAttack>().bodyOffset;
    }

    void Update() {
        CheckPlayer();
    }

    private void CheckPlayer() {
        if (_player && _player.GetComponent<PlayerMovement>().enabled) {

            Vector3 dir = Vector3.zero;
            if(transform.localScale.x < 0) dir = -transform.right;
            else dir = transform.right;

            List<string> nearest = new List<string>();
            for (int i = -10; i < 10; i++) {
                
                Debug.DrawRay(
                transform.position + new Vector3(0, -_bodyOffset, 0),
                (Quaternion.AngleAxis(i*10, transform.forward) * dir) * _playerAwarenessDistance,
                Color.red
                );

                RaycastHit2D[] hit = Physics2D.RaycastAll(
                    transform.position + new Vector3(0, -_bodyOffset, 0),
                    (Quaternion.AngleAxis(i*10, transform.forward) * dir),
                    _playerAwarenessDistance
                );

                foreach (RaycastHit2D coll in hit) {
                    string coll_name = coll.collider.name.ToLower();
                    if(coll_name.Contains("wall") || coll_name.Contains("player")) {
                        nearest.Add(coll_name);
                        break;
                    }
                }
            }

            foreach (string str in nearest) {
                if (str.Contains("player")) {
                    AwareOfPlayer = true;
                    break;
                }
            }
            
            Vector2 enemyToPlayerDistance = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerDistance.normalized;

            if (AwareOfPlayer && enemyToPlayerDistance.magnitude <= _playerShootDistance) ShootPlayer = true;
            else ShootPlayer = false;

            if (AwareOfPlayer && enemyToPlayerDistance.magnitude <= _playerAttackDistance) AttackPlayer = true;
            else AttackPlayer = false;

            if (enemyToPlayerDistance.magnitude > _playerAwarenessDistance) AwareOfPlayer = false;

        } else {
            AwareOfPlayer = false;
            AttackPlayer = false;
            ShootPlayer = false;
        }
    }
}
