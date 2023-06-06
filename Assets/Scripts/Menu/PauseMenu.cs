using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject _pauseUI;
    private GameObject _gameOverUI;

    private void Awake() {
        _pauseUI = transform.Find("PauseMenu").gameObject;
        _gameOverUI = transform.Find("GameOverMenu").gameObject;
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();    
    }

    public void Resume() {
        Cursor.visible = true;
        _pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() {
        Cursor.visible = true;
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

    public void GameOver() {
        Cursor.visible = true;
        Debug.Log("arrivato");
        _gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Replay() {
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
