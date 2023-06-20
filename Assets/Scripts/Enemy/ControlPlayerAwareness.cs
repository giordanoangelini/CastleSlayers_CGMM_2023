using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ControlPlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    public bool AttackPlayer {get; private set;}
    public bool ShootPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}
    public Vector2 DirectionToShoot {get; private set;}
    private float _playerAwarenessDistance = 5;
    [SerializeField] private float _playerShootDistance;
    [SerializeField] private float _playerAttackDistance;
    [SerializeField] private Transform _center;
    private Transform _player;
    private Path _path;
    private Seeker _seeker;
    private Renderer _renderer;

    IEnumerator Start() {
        _seeker = _center.GetComponent<Seeker>();
        yield return new WaitUntil(() => GameUtils.isInstantiated);
        _player = GameUtils.player.transform;
        _renderer = this.GetComponent<Renderer>();
    }

    void FixedUpdate() {
        if (_player) {
            UpdateAwarnessDistence();
            CheckPlayer();
        }
    }

    private void UpdateAwarnessDistence() {
        if (_renderer.isVisible) 
            _playerAwarenessDistance = (_player.Find("Hands").position - _center.position).magnitude;
        else 
            _playerAwarenessDistance = 0;
    }

    private void CheckPlayer() {
        if (_player.GetComponent<PlayerMovement>().enabled) {
            if (!AwareOfPlayer) EnemySeePlayer();
            if (AwareOfPlayer) FindPath();
            CheckDistances();
        } else {
            AwareOfPlayer = false;
            AttackPlayer = false;
            ShootPlayer = false;
        }
    }

    private void EnemySeePlayer() {
        Vector3 dir = Vector3.zero;
        if(transform.localScale.x < 0) dir = -transform.right;
        else dir = transform.right;

        List<string> nearest = new List<string>();
        for (int i = -20; i <= 20; i++) {
            
            Debug.DrawRay(
                _center.position,
                (Quaternion.AngleAxis(i*5, transform.forward) * dir) * _playerAwarenessDistance,
                Color.red
            );

            RaycastHit2D[] hit = Physics2D.RaycastAll(
                _center.position,
                (Quaternion.AngleAxis(i*5, transform.forward) * dir),
                _playerAwarenessDistance
            );

            foreach (RaycastHit2D coll in hit) {
                string coll_tag = coll.collider.tag;
                if (coll_tag == "wall" || coll_tag == "player") {
                    nearest.Add(coll_tag);
                    break;
                }
            }
        }

        if (nearest.Contains("player")) AwareOfPlayer = true;
    }

    private void FindPath() {
        _seeker.StartPath(_center.position, _player.position, OnPathComplete);
    }

    private void OnPathComplete(Path p) {
        if (!p.error) _path = p;
        DirectionToPlayer = (_path.vectorPath[1] - _center.position).normalized;
    }

    private void CheckDistances() {
        Vector2 enemyToPlayerDistance = _player.Find("Hands").position - _center.position;
        DirectionToShoot = enemyToPlayerDistance.normalized;

        if (AwareOfPlayer && enemyToPlayerDistance.magnitude <= _playerShootDistance) ShootPlayer = true;
        else ShootPlayer = false;

        if (AwareOfPlayer && enemyToPlayerDistance.magnitude <= _playerAttackDistance) AttackPlayer = true;
        else AttackPlayer = false;

        if (enemyToPlayerDistance.magnitude > _playerAwarenessDistance) AwareOfPlayer = false;
    }
    
}
