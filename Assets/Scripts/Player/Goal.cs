using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.ToLower().Contains("player")) {
            //Debug.Log(SceneManager.sceneCount);
            //if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1) GameObject.Find("Pause").GetComponent<PauseMenu>().YouWon();
            //else 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
