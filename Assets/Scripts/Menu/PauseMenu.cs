using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject _pauseUI;
    private GameObject _gameOverUI;
    private GameObject _youWonUI;

    private void Awake() {
        _pauseUI = transform.Find("PauseMenu").gameObject;
        _gameOverUI = transform.Find("GameOverMenu").gameObject;
        _youWonUI = transform.Find("YouWonMenu").gameObject;
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();    
    }

    public void Resume() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        _pauseUI.SetActive(false);
    }

    public void Pause() {
        Time.timeScale = 0f;
        Cursor.visible = true;
        _pauseUI.SetActive(true);
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
        Time.timeScale = 0f;
        Cursor.visible = true;
        _gameOverUI.SetActive(true);
    }

    public void YouWon() {
        Time.timeScale = 0f;
        Cursor.visible = true;
        _youWonUI.SetActive(true);
    }

    public void Replay() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene("Level_1");
    }

}
