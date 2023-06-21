using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            audio.UnPause();
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        Cursor.visible = true;
        _pauseUI.SetActive(true);

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            audio.Pause();
        }
    }

    public void MainMenuFromPause() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            audio.Stop();
        }
    }

    public void Quit() {
        Debug.Log("Bye");
        Application.Quit();
    }

    public void GameOver() {
        Time.timeScale = 0f;
        Cursor.visible = true;
        _gameOverUI.SetActive(true);

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            audio.Stop();
        }
        Audio.instance.gameOverSound.Play();
    }

    public void YouWon() {
        Time.timeScale = 0f;
        Cursor.visible = true;
        _youWonUI.transform.Find("TIME").GetComponent<Text>().text = FloatToTimestamp(Time.time-GameUtils.startTime);
        _youWonUI.SetActive(true);
        
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            audio.Stop();
        }
        Audio.instance.youWonSound.Play();
    }

    private string FloatToTimestamp(float time) {
		return TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss\,ff");
	}

    public void Replay() {
        MainMenu.PlayGame();
    }

}
