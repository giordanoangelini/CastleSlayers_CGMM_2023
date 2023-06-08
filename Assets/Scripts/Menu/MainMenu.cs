using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake() {
        Cursor.visible = true;
    }
    public void PlayGame() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Bye");
        Application.Quit();
    }

    public void SelectChar(string prefs) {
        Vector3 selected = new Vector3(6.5f,6.5f,6.5f);
        Vector3 unselected = new Vector3(5f,5f,5f);
        foreach (string button in new string[]{"Blake", "Spike", "Pink"}) {
            transform.Find(button).gameObject.GetComponent<RectTransform>().localScale = unselected;
        }
        string name = prefs.Split(";")[0];
        string weapon = prefs.Split(";")[1];
        transform.Find(name).gameObject.GetComponent<RectTransform>().localScale = selected;
        GameUtils.character = name;
        GameUtils.weapon = weapon;
    }
}
