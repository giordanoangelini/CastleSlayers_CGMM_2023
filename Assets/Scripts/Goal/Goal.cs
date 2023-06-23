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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
                    if (audio != Audio.instance.BgMusic) audio.Stop();
                }
                Audio.PlaySound(Audio.instance.changeSceneSound);
            }
        }
    }

    private void YouWon() {
        GameObject.Find("Pause").GetComponent<PauseMenu>().YouWon();
    }
}
