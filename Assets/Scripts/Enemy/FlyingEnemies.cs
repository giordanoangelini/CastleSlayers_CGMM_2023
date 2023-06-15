using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlyingEnemies : MonoBehaviour
{
    private void Awake() {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("non_walkable").GetComponent<Collider2D>());
    }
}
