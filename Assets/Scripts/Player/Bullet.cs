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
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.name.ToLower().Contains("enemy")) {
            Destroy(gameObject);
            collision.GetComponent<EnemyAttack>().EnemyDeath();
        }

        if (collision.name.ToLower().Contains("wall")) {
            Destroy(gameObject);
        }
    }
}
