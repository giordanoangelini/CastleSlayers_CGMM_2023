using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParameters : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float timeBetweenAttacks;
    public enum FireMethods {single, multiple, non_stop, white};
    [SerializeField] public FireMethods attackMethod;
    [SerializeField] public float attackRange;

}
