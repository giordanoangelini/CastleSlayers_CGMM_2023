using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {
    private Vector3 _mousePos;
    private Transform _aimTransform;
   
    private void Awake() {
        _aimTransform = transform.Find("Aim");
    }

    private void Update() {
    HandleAiming();
    //HandleShooting();
    }

    private void HandleAiming() {
        
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 aimDirection = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _aimTransform.eulerAngles= new Vector3(0, 0, angle);
    }
        
    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
 
        }
    }
}