using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;

    private void Awake() {
        Cursor.visible = true;
    }
    public void Resume() {
        _pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() {
        _pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Debug.Log("Bye");
        Application.Quit();
    }

}
