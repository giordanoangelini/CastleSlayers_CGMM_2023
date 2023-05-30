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

        if (collision.name.ToLower().Contains("enemy")) {
            Destroy(gameObject);
            GameUtils.DieAnimations(collision.gameObject, collision.gameObject.GetComponent<Animator>());
            Destroy(collision.gameObject, 2f);
        }

        if (collision.name.ToLower().Contains("wall")) {
            Destroy(gameObject);
        }
    }
}
