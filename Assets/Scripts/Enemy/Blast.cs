using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class Blast : MonoBehaviour
{
    private float _blastLife = 2f;
    private void Awake() {
        Audio.instance.enemyBlast.Play();
        Destroy(gameObject, _blastLife);
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "player") {
            Destroy(gameObject);
            collision.GetComponent<PlayerAttack>().PlayerDeath();
        }

        if (collision.tag == "wall") {
            Destroy(gameObject);
        }
    }
}
