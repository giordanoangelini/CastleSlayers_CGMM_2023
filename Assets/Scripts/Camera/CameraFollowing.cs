using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowing : MonoBehaviour
{
    public Transform _target;

    private void Start() {
        _target = GameUtils.player.transform;
    }
    private void FixedUpdate() {
        if (_target) UpdatePosition();
    }

    private void UpdatePosition() {
        transform.position = new Vector3(
            _target.transform.position.x,
            _target.transform.position.y,
            transform.position.z
        );
    }
}
