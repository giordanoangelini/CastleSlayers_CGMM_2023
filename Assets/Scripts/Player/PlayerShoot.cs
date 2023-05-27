using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    private Transform _fireSpot;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private Transform _weapon;
    private float _lastFireTime;
    private bool _fireContinuously;
    private bool _fireSingle;
    private Animator _animator;
    
    private void Awake() {
        _animator = _weapon.GetComponent<Animator>();
        _fireSpot = _weapon.Find("FireSpot");
    }
    void Update()
    {
        /*
        if (_fireContinuously || _fireSingle) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire > _timeBetweenShots) {
                FireBullet();
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }*/
    }

    private void FireBullet()
    {
        _animator.SetTrigger("shoot");
        GameObject bullet = Instantiate(_bulletPrefab, _fireSpot.transform.position, new Quaternion());
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * _fireSpot.transform.right;
    }
    private void OnFire(InputValue inputValue)
    {
        //_fireContinuously = inputValue.isPressed;
        if (inputValue.isPressed) {
            FireBullet();
        }
    }
}