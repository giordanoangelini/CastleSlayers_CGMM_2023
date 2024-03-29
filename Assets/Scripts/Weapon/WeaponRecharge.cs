using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRecharge : MonoBehaviour
{
    private Slider _slider;
    public Transform _player;
    private WeaponParameters _weaponParameters;

    private void Start() {
        _player = GameUtils.player.transform;
        this.GetComponent<FixedJoint2D>().connectedBody = _player.GetComponent<Rigidbody2D>();
        _slider = this.GetComponent<Slider>();
        _slider.minValue = -0.1f;
    }
    
    private void FixedUpdate() {
        if (_player) UpdateSlider();
    }

    private void UpdateSlider() {
        _slider.maxValue = _player.GetComponent<PlayerAttack>().weaponParameters.timeBetweenAttacks;
        _slider.value = Time.time - GameUtils.lastFireTime;
    }
}
