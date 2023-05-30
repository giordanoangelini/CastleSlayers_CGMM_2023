using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private WeaponParameters _weaponParameters;
    private Transform _hands;
    private Transform _weapon;
    private Transform _fireSpot;

    private bool _fireContinuously;
    private float _lastFireTime;
    private float _flowStartTime;
    private float _maxFlowTime = 3f;

    
    private void Awake() {
        _hands = transform.Find("Hands");
        _lastFireTime = Time.time;
        DetectWeapon();
    }
    
    private void Update() {
        if (_fireContinuously) {
            if (Time.time - _flowStartTime < _maxFlowTime) FireBullet();
            else _fireContinuously = false;
        }
    }

    private bool CanAttack() {
        float timeSinceLastFire = Time.time - _lastFireTime;
        if (timeSinceLastFire >= _weaponParameters.timeBetweenAttacks) return true;
        return false;
    }

    private void DetectWeapon() {
        foreach (Transform child in _hands.transform) {
            if (child.gameObject.activeSelf) {
                _weapon = child;
            }
        }
        _weaponParameters = _weapon.GetComponent<WeaponParameters>();
        if (_weapon.Find("FireSpot")) {
            _fireSpot = _weapon.Find("FireSpot");
        }
    }

    private void FireBullet() {
        _lastFireTime = Time.time;
        GameObject bullet = Instantiate(_weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
        bullet.GetComponent<Rigidbody2D>().velocity = _weaponParameters.bulletSpeed * _fireSpot.transform.right;
    }

    private void FireMultipleBullets() {
        _lastFireTime = Time.time;
        Vector3[] directions = new Vector3[3];
        directions[0] = _fireSpot.transform.right;
        directions[1] = _fireSpot.transform.right + new Vector3(0.2f, 0.2f, 0);
        directions[2] = _fireSpot.transform.right - new Vector3(0.2f, 0.2f, 0);
        for (int i = 0; i < 3; i++) {
            GameObject bullet = Instantiate(_weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
            bullet.GetComponent<Rigidbody2D>().velocity = _weaponParameters.bulletSpeed * directions[i];
        }
    }

    private void Attack() {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(
            new Vector2(_hands.position.x, _hands.position.y -_weaponParameters.attackRange),
            GameUtils.AttackArea(transform.localScale.x, _hands.position.x, _hands.position.y, _weaponParameters.attackRange)
        );
        foreach (Collider2D enemy in hitEnemies) {
            if (enemy.name.ToLower().Contains("enemy")) {
                GameUtils.DieAnimations(enemy.gameObject, enemy.GetComponent<Animator>());
                Destroy(enemy.gameObject, 2f);   
            }
        }
    }

    private void OnFire(InputValue inputValue) {
        DetectWeapon();
        switch (_weaponParameters.attackMethod) {
            case WeaponParameters.FireMethods.single:
                if (CanAttack()) FireBullet();
                break;
            case WeaponParameters.FireMethods.multiple:
                if (CanAttack()) FireMultipleBullets();
                break;
            case WeaponParameters.FireMethods.non_stop:
                if (CanAttack()) {
                    _flowStartTime = Time.time;
                    _fireContinuously = inputValue.isPressed;
                }
                break;
            case WeaponParameters.FireMethods.white:
                if (CanAttack()) Attack();
                break;
            default: break;
        }
    }
}