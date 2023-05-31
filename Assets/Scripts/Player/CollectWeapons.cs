using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapons : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.ToLower().Contains("player")) {
            Transform playerHands = other.transform.Find("Hands");
            DeactivateAll(playerHands);
            GameObject newWeapon = playerHands.Find(gameObject.name).gameObject;
            newWeapon.gameObject.SetActive(true);
            playerHands.GetComponentInParent<PlayerAttack>()._lastFireTime = Time.time - newWeapon.GetComponent<WeaponParameters>().timeBetweenAttacks;
            playerHands.GetComponentInParent<PlayerAttack>()._fireContinuously = false;
            Destroy(gameObject);
        }
    }

    private void DeactivateAll(Transform parent) {
        foreach (Transform child in parent) {
            if (child.gameObject.activeSelf) child.gameObject.SetActive(false);
        }
    }

}
