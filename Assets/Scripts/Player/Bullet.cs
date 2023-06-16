using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private float _bulletLife = 2f;
    private void Awake() {
        Destroy(gameObject, _bulletLife);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "enemy") {
            collision.GetComponent<EnemyAttack>().EnemyDeath();
            Destroy(gameObject);
        }

        if (collision.tag == "wall") {
            Destroy(gameObject);
        }
    }
}
