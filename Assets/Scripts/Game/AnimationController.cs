using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private float _epsilon = 0.05f;
    public void FlippingCharacter(Rigidbody2D rigidbody) {
        // Flipping image
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (rigidbody.velocity.x * _currentScale.x < 0) {
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
