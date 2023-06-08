using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class Blast : MonoBehaviour
{
    private float _blastLife = 2f;
    private void Awake() {
        Destroy(gameObject, _blastLife);
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag.ToLower().Contains("player")) {
            Destroy(gameObject);
            collision.GetComponent<PlayerAttack>().PlayerDeath();
        }

        if (collision.tag.ToLower().Contains("wall")) {
            Destroy(gameObject);
        }
    }
}
