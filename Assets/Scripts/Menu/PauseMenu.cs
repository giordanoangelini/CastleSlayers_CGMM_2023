using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject _pauseUI;

    private void Awake() {
        _pauseUI = transform.Find("PauseMenu").gameObject;
        Cursor.visible = true;
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();    
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
