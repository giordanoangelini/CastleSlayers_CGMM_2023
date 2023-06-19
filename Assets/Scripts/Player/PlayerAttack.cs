using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public WeaponParameters weaponParameters {get; private set;}
    private Transform _hands;
    private Transform _weapon;
    private Transform _fireSpot;

    public bool fireContinuously {get; set;}
    private float _flowStartTime;
    private float _maxFlowTime = 3f;
    private float _bulletSpeed = 20f;
    private bool _dead = false;

    [SerializeField] private AudioSource _gunshot;
    [SerializeField] private AudioSource _pompagun;
    [SerializeField] private AudioSource _swoosh;

    
    private void Awake() {
        _hands = transform.Find("Hands");
        _hands.Find(GameUtils.weapon).gameObject.SetActive(true);
        DetectWeapon();
    }
    
    private void FixedUpdate() {
        if (fireContinuously) {
            if (Time.time - _flowStartTime < _maxFlowTime) FireBullet();
            else fireContinuously = false;
        }
    }

    private bool CanAttack() {
        float timeSinceLastFire = Time.time - GameUtils.lastFireTime;
        if (timeSinceLastFire >= weaponParameters.timeBetweenAttacks && !_dead) return true;
        return false;
    }

    private void DetectWeapon() {
        foreach (Transform child in _hands.transform) {
            if (child.gameObject.activeSelf) {
                _weapon = child;
            }
        }
        weaponParameters = _weapon.GetComponent<WeaponParameters>();
        if (_weapon.Find("FireSpot")) {
            _fireSpot = _weapon.Find("FireSpot");
        }
    }

    private void FireBullet() {
        GameUtils.lastFireTime = Time.time;
        _weapon.GetComponent<Animator>().SetTrigger("shoot");
        _gunshot.Play();
        GameObject bullet = Instantiate(weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
        bullet.GetComponent<Rigidbody2D>().velocity = _bulletSpeed * _fireSpot.transform.right;
    }

    private void FireMultipleBullets() {
        GameUtils.lastFireTime = Time.time;
        _weapon.GetComponent<Animator>().SetTrigger("shoot");
        _pompagun.Play();
        Vector3 dir = _fireSpot.transform.right;
        foreach (int i in new int[]{-1,0,1}) {
            GameObject bullet = Instantiate(weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
            bullet.GetComponent<Rigidbody2D>().velocity = _bulletSpeed * (Quaternion.AngleAxis(i*10, transform.forward) * dir);
        }
    }

    private void Attack() {
        _weapon.GetComponent<Animator>().SetTrigger("attack");
        _swoosh.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            _hands.position,
            weaponParameters.attackRange
        );
        foreach (Collider2D coll in hitEnemies) {
            if (coll.tag == "enemy") {
                coll.GetComponent<EnemyAttack>().EnemyDeath(); 
            }
        }
    }

    private void OnDrawGizmos() {
        if (_hands) Gizmos.DrawWireSphere(_hands.position, weaponParameters.attackRange);
    }

    private void OnFire(InputValue inputValue) {
        DetectWeapon();
        switch (weaponParameters.attackMethod) {
            case WeaponParameters.FireMethods.single:
                if (CanAttack()) FireBullet();
                break;
            case WeaponParameters.FireMethods.multiple:
                if (CanAttack()) FireMultipleBullets();
                break;
            case WeaponParameters.FireMethods.non_stop:
                if (CanAttack()) {
                    _flowStartTime = Time.time;
                    fireContinuously = inputValue.isPressed;
                }
                break;
            case WeaponParameters.FireMethods.white:
                if (CanAttack()) Attack();
                break;
            default: break;
        }
    }

    public void PlayerDeath() {
        Camera.main.orthographicSize = 4;
        _dead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _hands.gameObject.SetActive(false);
        transform.parent.Find("Crosshair").gameObject.SetActive(false);
        transform.parent.Find("Recharge").gameObject.SetActive(false);
        GameUtils.DeathAnimation(gameObject, GetComponent<Animator>());
        Destroy(gameObject, 2f);    
    }

    private void OnDestroy() {
        GameObject.Find("Pause").GetComponent<PauseMenu>().GameOver();
    }
}