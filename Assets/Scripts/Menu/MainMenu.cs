using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Vector3 _selected = new Vector3(6.5f,6.5f,6.5f);
    private Vector3 _unselected = new Vector3(5f,5f,5f);
    private static AudioSource _music;
    private void Awake() {
        DeselectAll();
        SelectPlayer(name: GameUtils.character);

        _music = GameObject.Find("BG Music").GetComponent<AudioSource>();
    }
    public static void PlayGame() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameUtils.weapon = GameUtils.default_players[GameUtils.character];
        SceneManager.LoadScene("Level_1");
        GameUtils.startTime = Time.time;
        _music.Play();
    }

    public void QuitGame() {
        Debug.Log("Bye");
        Application.Quit();
    }

    private void DeselectAll() {
        foreach (Transform button in transform) {
            if (button.tag == "player") button.GetComponent<RectTransform>().localScale = _unselected;
        }
    }

    private void SelectPlayer(string name) {
        transform.Find(name).gameObject.GetComponent<RectTransform>().localScale = _selected;
    }

    public void SelectChar(string name) {
        DeselectAll();
        string weapon = GameUtils.default_players[name];
        SelectPlayer(name);
        GameUtils.character = name;
        GameUtils.weapon = weapon;
    }
}
