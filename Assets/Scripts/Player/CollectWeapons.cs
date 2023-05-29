using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapons : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.ToLower().Contains("player")) {
            Debug.Log(gameObject.name);
            Transform playerHands = other.transform.Find("Hands");
            DeactivateAll(playerHands);
            playerHands.Find(gameObject.name).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void DeactivateAll(Transform parent) {
        foreach (Transform child in parent) {
            if (child.gameObject.activeSelf) child.gameObject.SetActive(false);
        }
    }

}
