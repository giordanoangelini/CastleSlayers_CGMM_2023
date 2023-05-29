using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private float _lastFireTime;
    private WeaponParameters _weaponParameters;
    private Transform _hands;
    private Transform _weapon;
    private Transform _fireSpot;
    
    private void Awake() {
        _hands = transform.Find("Hands");
        DetectWeapon();
    }

    private void DetectWeapon() {
        foreach (Transform child in _hands.transform) {
            if (child.gameObject.activeSelf) {
                _weapon = child;
            }
        }
        _weaponParameters = _weapon.GetComponent<WeaponParameters>();
        _fireSpot = _weapon.Find("FireSpot");
    }

    private void FireBullet() {
        DetectWeapon();
        GameObject bullet = Instantiate(_weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _weaponParameters.bulletSpeed * _fireSpot.transform.right;
    }
    private void OnFire(InputValue inputValue) {
        FireBullet();
    }
}