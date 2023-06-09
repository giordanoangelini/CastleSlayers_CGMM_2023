using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Vector3 _selected = new Vector3(6.5f,6.5f,6.5f);
    private Vector3 _unselected = new Vector3(5f,5f,5f);
    private void Awake() {
        DeselectAll();
        SelectPlayer(GameUtils.character);
    }
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void SelectChar(string prefs) {
        DeselectAll();
        string name = prefs.Split(";")[0];
        string weapon = prefs.Split(";")[1];
        SelectPlayer(name);
        GameUtils.character = name;
        GameUtils.weapon = weapon;
    }
}
