using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag.ToLower().Contains("player")) {
            SceneManager.LoadScene("Level_2");
        }
    }
}
