using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParameters : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float timeBetweenShots;
    public enum FireMethods {sigle, multiple, non_stop};
    [SerializeField] public FireMethods fireMethod;
}
