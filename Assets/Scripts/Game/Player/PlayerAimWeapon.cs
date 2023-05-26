using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour {
    private Vector3 _mousePos;
    private Transform _aimTransform;
    private Rigidbody2D _rigidBody;
    private Camera _mainCam;
   
    private void Awake() {
        _aimTransform = transform.Find("Aim");
        _rigidBody = GetComponent<Rigidbody2D>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update() {
        HandleAiming();
        //HandleShooting();
    }

    private void HandleAiming() {
        if(_rigidBody.transform.localScale.x * _aimTransform.transform.localScale.x < 0){
            _aimTransform.transform.localScale = Vector3.Scale(
                _aimTransform.transform.localScale, new Vector3(-1,-1,1));
        }
        _mousePos = _mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 aimDirection = _mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _aimTransform.rotation = Quaternion.Euler(0,0,angle);
    }
        
    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
 
        }
    }
}