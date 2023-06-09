using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRecharge : MonoBehaviour
{
    public Transform _target;
    private float _offset = 0.3f;
    private Slider _slider;
    private PlayerAttack _playerAttack;
    private WeaponParameters _weaponParameters;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _slider.minValue = -0.1f;
    }
    void FixedUpdate() {
        Position();
        Value();
    }

    public void Position() {
        if (FindObjectOfType<PlayerMovement>()) {
            if (!_target) _target = FindObjectOfType<PlayerMovement>().transform;
            else {
                transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + _offset, transform.position.z);
            }
        }
    }

    public void Value() {
        if (FindObjectOfType<WeaponParameters>() != _weaponParameters)
            _slider.maxValue = FindObjectOfType<WeaponParameters>().timeBetweenAttacks;
        if (FindObjectOfType<PlayerAttack>() != _playerAttack) {
            _playerAttack = FindObjectOfType<PlayerAttack>();
        }
        _slider.value = Time.time - _playerAttack.lastFireTime;
    }
}
