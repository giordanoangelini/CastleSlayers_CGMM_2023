using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _fireSpot;
    [SerializeField] private float _timeBetweenShots;
    private float _lastFireTime;
    private bool _fireContinuously;
    
    void Update()
    {
        if (_fireContinuously) {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if (timeSinceLastFire > _timeBetweenShots) {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _fireSpot.transform.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _bulletSpeed * _fireSpot.transform.right;
    }
    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;
    }
}