using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairPosition : MonoBehaviour {

    private void Awake() {
        Cursor.visible = false;
    }
    void FixedUpdate() {
        Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = _mousePos;
    }
}
