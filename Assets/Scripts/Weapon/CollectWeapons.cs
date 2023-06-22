using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapons : MonoBehaviour
{
    private void FixedUpdate() {
        GameUtils.Float(transform);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player") {
            Transform playerHands = other.transform.Find("Hands");
            DeactivateAll(parent: playerHands);
            GameObject newWeapon = playerHands.Find(gameObject.tag).gameObject;
            newWeapon.gameObject.SetActive(true);
            Audio.PlaySound(Audio.instance.collectSound);
            GameUtils.weapon = newWeapon.tag;
            GameUtils.lastFireTime = 0;
            playerHands.GetComponentInParent<PlayerAttack>().fireContinuously = false;
            Destroy(gameObject);
        }
    }

    private void DeactivateAll(Transform parent) {
        foreach (Transform child in parent) {
            if (child.gameObject.activeSelf) child.gameObject.SetActive(false);
        }
        foreach (Bullet bullet in FindObjectsOfType<Bullet>()) {
            Destroy(bullet);
        }
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            if (audio != Audio.instance.BgMusic) audio.Stop();
        }
    }

}
