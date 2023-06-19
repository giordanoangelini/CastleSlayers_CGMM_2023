using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] public AudioSource music;
    private static Music musicInstance;
    private void Awake() {
        DontDestroyOnLoad(this);
        music.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
        
        if (musicInstance == null) musicInstance = this;
        else Destroy(gameObject);
    }
}