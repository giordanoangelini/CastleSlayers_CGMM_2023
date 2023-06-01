using System;
using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {
    private Vector3 _mousePos;
    private Rigidbody2D _rigidBody;
    private Transform _hands;
   
    private void Awake() {
       _rigidBody = GetComponent<Rigidbody2D>();
       _hands = transform.Find("Hands");
    }

    private void FixedUpdate() {
        HandleAiming();
    }

    private void HandleAiming() {
        if(_rigidBody.transform.localScale.x * _hands.transform.localScale.x < 0){
            _hands.transform.localScale = Vector3.Scale(
                _hands.transform.localScale, new Vector3(-1,-1,1));
        }
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 aimDirection = (_mousePos - _hands.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _hands.transform.rotation = Quaternion.Euler(0,0,angle);
    }
}