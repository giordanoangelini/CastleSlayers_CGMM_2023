using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private Transform _fireSpot;
    private Transform _weapon;
    private float _lastFireTime;
    private WeaponParameters _weaponParameters;
    
    private void Awake() {
        _weapon = transform.Find("Weapon");
        _fireSpot = _weapon.transform.Find("FireSpot");
        _weaponParameters = _weapon.GetComponent<WeaponParameters>();
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _weaponParameters.bulletSpeed * _fireSpot.transform.right;
    }
    private void OnFire(InputValue inputValue)
    {
        if (inputValue.isPressed) {
            FireBullet();
        }
    }
}