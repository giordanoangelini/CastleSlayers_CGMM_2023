using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blast : MonoBehaviour
{
    private float _blastLife = 2f;
    private void Start() {
        Destroy(gameObject, _blastLife);
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.name.ToLower().Contains("player")) {
            Destroy(gameObject);
            Debug.Log("preso");
            //collision.GetComponent<PlayerAttack>().PlayerDeath();
        }

        if (collision.name.ToLower().Contains("wall")) {
            Destroy(gameObject);
        }
    }
}
