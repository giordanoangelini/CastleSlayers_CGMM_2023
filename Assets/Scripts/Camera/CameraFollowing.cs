using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowing : MonoBehaviour
{
    public Transform _target;
    void Update() {
        if (FindObjectOfType<PlayerMovement>()) {
            if (!_target) _target = FindObjectOfType<PlayerMovement>().transform;
            else {
                transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
            }
        }
    }
}
