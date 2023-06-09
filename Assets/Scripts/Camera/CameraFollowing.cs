using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowing : MonoBehaviour
{
    public Transform _target;

    IEnumerator Start() {
        yield return new WaitUntil(() => GameUtils.isInstantiated);
        _target = GameUtils.player.transform;
    }
    private void FixedUpdate() {
        if (_target) transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
    }
}
