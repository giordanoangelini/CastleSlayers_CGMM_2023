using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake() {
        foreach (Transform player in transform) {
            if (GameUtils.character == player.name) player.gameObject.SetActive(true);
        }
    }
}
