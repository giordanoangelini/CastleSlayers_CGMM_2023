using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private void Awake() {
        Cursor.visible = true;
    }
    public void SetVolume(float volume) {
        Debug.Log(volume);
    }
}
