using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GoalDoor : MonoBehaviour
{
    private void FixedUpdate() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0) {
            Destroy(gameObject);
            Destroy(GameObject.FindGameObjectWithTag("door"));
        }
    }
}
