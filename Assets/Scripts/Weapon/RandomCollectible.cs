using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectible : MonoBehaviour
{
    [System.Serializable] public struct WeaponStruct {
        public GameObject prefab;
        public int weight;
    }
    [SerializeField] private WeaponStruct[] weapons;
    private List<GameObject> pickables = new List<GameObject>();
    private System.Random random = new System.Random();
    public GameObject getRandomWeapon(){
        foreach (WeaponStruct weapon in weapons) {
            for (int i = 0; i < weapon.weight; i++) {
                if (GameUtils.weapon != weapon.prefab.tag) pickables.Add(weapon.prefab);
            }
        }
        int picked = random.Next(0, pickables.Count);
        return pickables[picked];
    }

    private void Awake() {   
        Instantiate(getRandomWeapon(), transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
    
}

 