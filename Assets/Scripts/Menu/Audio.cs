using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    [SerializeField] public AudioSource BgMusic;
    [SerializeField] public AudioSource changeSceneSound;
    [SerializeField] public AudioSource gameOverSound;
    [SerializeField] public AudioSource youWonSound;
    [SerializeField] public AudioSource gunshot;
    [SerializeField] public AudioSource pompagun;
    [SerializeField] public AudioSource swoosh;
    [SerializeField] public AudioSource machinegun;
    [SerializeField] public AudioSource collectSound;
    [SerializeField] public AudioSource enemyBlast;
    [SerializeField] public AudioSource playerHit;
    public static Audio instance;
    private void Awake() {
        DontDestroyOnLoad(this);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static void PlaySound(AudioSource audio, float relative_volume = 1) {
        audio.volume = relative_volume * GameUtils.master_volume;
        audio.Play();
    }

    
}
