using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string _defaultChar = "Blake";
    private void Awake() {
        Cursor.visible = true;
        PlayerPrefs.SetString("character", _defaultChar);
    }
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Bye");
        Application.Quit();
    }

    public void SelectChar(string name) {
        Vector3 selected = new Vector3(6.5f,6.5f,6.5f);
        Vector3 unselected = new Vector3(5f,5f,5f);
        foreach (string button in new string[]{"Blake", "Spike", "Pink"}) {
            transform.Find(button).gameObject.GetComponent<RectTransform>().localScale = unselected;
        }
        transform.Find(name).gameObject.GetComponent<RectTransform>().localScale = selected;
        PlayerPrefs.SetString("character", name);
    }
}
