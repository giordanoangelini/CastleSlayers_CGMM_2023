using System;
using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {
    private Vector3 _mousePos;
    private Rigidbody2D _rigidBody;
   
    private void Awake() {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
    }

    private void Update() {
        HandleAiming();
        //HandleShooting();
    }

    private void HandleAiming() {
        if(_rigidBody.transform.localScale.x * transform.localScale.x < 0){
            transform.localScale = Vector3.Scale(
                transform.localScale, new Vector3(-1,-1,1));
        }
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 aimDirection = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
 
        }
    }
}