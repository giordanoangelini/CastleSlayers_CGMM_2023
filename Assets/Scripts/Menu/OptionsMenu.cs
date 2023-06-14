using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private void Awake() {
        FindAnyObjectByType<InputField>().text = PlayerPrefs.GetString("username");
    }
    public void SetVolume(float volume) {
        Debug.Log(volume);
    }

    public void SetUsername (string username) {
        Debug.Log(username);
        PlayerPrefs.SetString("username", username);
    }
}
