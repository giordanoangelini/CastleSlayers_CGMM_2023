using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectible : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private int len = 4;
    public GameObject getRandomWeapon(){
        System.Random random = new System.Random();
        int index = random.Next(0, len);
        return weapons[index];
    } 
    void Awake()
    {
        Instantiate(getRandomWeapon(), transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}

 