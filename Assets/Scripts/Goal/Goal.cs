using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "player") {
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) YouWon();
            else {
                Time.timeScale = 0f;
                GameUtils.isInstantiated = false;
                Audio.PlaySound(Audio.instance.changeSceneSound);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void YouWon() {
        GameObject.Find("Pause").GetComponent<PauseMenu>().YouWon();
    }
}
