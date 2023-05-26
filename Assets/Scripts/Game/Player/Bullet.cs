using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletLife;
    private void Start() {
        Destroy(gameObject, _bulletLife);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyMovement>()) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.GetComponent<TilemapCollider2D>()) {
            Destroy(gameObject);
        }
    }
}
