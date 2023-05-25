using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {
    public Vector3 _mousePos;
    private Transform _aimTransform;

    private Rigidbody2D _rigidBody;
   
    private void Awake() {
        _aimTransform = transform.Find("Aim");
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
    HandleAiming();
    //HandleShooting();
    }

    private void HandleAiming() {
        
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        if(_rigidBody.transform.localScale.x<0){
            _mousePos = new Vector3(-_mousePos.x, -_mousePos.y, _mousePos.z);
        }
        Vector3 aimDirection = (_mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _aimTransform.eulerAngles= new Vector3(0, 0, angle);
    }
        
    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
 
        }
    }
}