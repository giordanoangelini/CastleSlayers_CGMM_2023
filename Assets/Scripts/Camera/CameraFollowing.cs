using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    public Transform target;
    private Transform _playerSpot;

    private void Awake() {
        _playerSpot = GameObject.Find("Player").transform;
        GameObject prefab = players[0];
        switch (PlayerPrefs.GetString("character")) {
            case "Blake": prefab = players[0]; break;
            case "Pink": prefab = players[1]; break;
            case "Spike": prefab = players[2]; break;
        }
         target = Instantiate(prefab.transform, _playerSpot.position, Quaternion.identity, _playerSpot);
    }
    
    void FixedUpdate() {
        if (target) {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
    }
}
