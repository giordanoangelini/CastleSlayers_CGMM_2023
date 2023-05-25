using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private float _epsilon = 0.05f;
    private Vector3 _mousePos;
    private Camera _mainCam;
    private void Awake() {
         _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void FlippingCharacterOnMove(Rigidbody2D rigidbody) {
        // Flipping image
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (rigidbody.velocity.x * _currentScale.x < 0) {
            rigidbody.transform.localScale = new Vector3(
                -_currentScale.x, _currentScale.y, _currentScale.z);  
        }
    }

    public void FlippingCharacterOnAim(Rigidbody2D rigidbody) {
        _mousePos = _mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        // Flipping image
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (_mousePos.x * _currentScale.x < 0) {
            rigidbody.transform.localScale = new Vector3(
                -_currentScale.x, _currentScale.y, _currentScale.z);  
        }
    }

    public void MoveAnimations(Rigidbody2D rigidbody, Animator animator) {
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
