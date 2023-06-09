using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private string[] _utilities = {"Crosshair", "Recharge"};
    private void Awake() {
        foreach (Transform player in transform) {
            if (GameUtils.character == player.name) player.gameObject.SetActive(true);
            else if (!_utilities.Contains(player.name)) Destroy(player.gameObject);
        }
    }
}
