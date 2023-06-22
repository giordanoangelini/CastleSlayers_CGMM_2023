using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    private void Awake() {
        FindAnyObjectByType<InputField>().text = PlayerPrefs.GetString("username");
        FindAnyObjectByType<Slider>().value = GameUtils.master_volume;
    }
    public void SetVolume(float volume) {
        GameUtils.master_volume = volume;
    }

    public void SetUsername (string username) {
        Debug.Log(username);
        PlayerPrefs.SetString("username", username);
    }
}
