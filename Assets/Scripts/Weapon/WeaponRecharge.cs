using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRecharge : MonoBehaviour
{
    private float _offset = 0.3f;
    private Slider _slider;
    public Transform _player;
    private WeaponParameters _weaponParameters;

    IEnumerator Start() {
        yield return new WaitUntil(() => GameUtils.isInstantiated);
        _player = GameUtils.player.transform;
        _slider = GetComponent<Slider>();
        _slider.minValue = -0.1f;
    }
    
    private void FixedUpdate() {
        if (_player) UpdateSlider();
    }

    private void UpdateSlider() {
        transform.position = new Vector3(
            _player.transform.position.x,
            _player.transform.position.y + _offset,
            transform.position.z
        );
        _slider.maxValue = _player.GetComponent<PlayerAttack>().weaponParameters.timeBetweenAttacks;
        _slider.value = Time.time - GameUtils.lastFireTime;
    }
}
