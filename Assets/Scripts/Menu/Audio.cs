using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    [SerializeField] public  AudioSource BgMusic;
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
}
