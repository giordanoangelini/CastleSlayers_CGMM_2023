using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class AnimationController
{
    private static float _epsilon = 0.05f;
    private static Vector3 _mousePos;

    public static void FlippingCharacterOnMove(Rigidbody2D rigidbody) {
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (rigidbody.velocity.x * _currentScale.x < 0) {
            rigidbody.transform.localScale = new Vector3(
                -_currentScale.x, _currentScale.y, _currentScale.z);  
        }
    }

    public static void FlippingCharacterOnAim(Rigidbody2D rigidbody) {
        _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (ViewportHandler(_mousePos.x) * _currentScale.x < 0) {
            rigidbody.transform.localScale = new Vector3(
                -_currentScale.x, _currentScale.y, _currentScale.z);  
        }
    }

    private static int ViewportHandler(float num) {
        if (num < 0.5f) return -1;
        else return 1;
    }

    public static void MoveAnimations(Rigidbody2D rigidbody, Animator animator) {
        // Switchin moving/idle animations
        if (Mathf.Abs(rigidbody.velocity.x) > _epsilon || Mathf.Abs(rigidbody.velocity.y) > _epsilon) {
            animator.ResetTrigger("stop");
            animator.SetTrigger("move");
        } else {
            animator.ResetTrigger("move");
            animator.SetTrigger("stop");
        }
    }
}
