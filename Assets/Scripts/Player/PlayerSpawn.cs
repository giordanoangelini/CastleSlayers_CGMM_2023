using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _utilities;
    private void Start() {
        foreach (Transform player in transform) {
            if (GameUtils.character == player.name){
                player.gameObject.SetActive(true);
                GameUtils.player = player.gameObject;
            } else {
                if (_utilities.Contains(player.gameObject) == false) 
                    Destroy(player.gameObject);
            }
        }
        GameUtils.isInstantiated = true;
        Time.timeScale = 1f;
    }
}
