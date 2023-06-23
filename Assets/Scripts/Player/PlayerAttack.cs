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
    private bool _machineAudio = false;

    
    private void Awake() {
        _hands = transform.Find("Hands");
        _hands.Find(GameUtils.weapon).gameObject.SetActive(true);
        DetectWeapon();
    }
    
    private void FixedUpdate() {
        MachineGun();
    }

    private void Update() {
        if (Time.timeScale == 0) this.GetComponent<PlayerInput>().enabled = false;
        else this.GetComponent<PlayerInput>().enabled = true;
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

    private void MachineGun() {
        if (fireContinuously) {
            if (_machineAudio) {
                Audio.PlaySound(Audio.instance.machinegun, 1);
                _machineAudio = false;
            }
            if (Time.time - _flowStartTime < _maxFlowTime) FireBullet();
            else fireContinuously = false;
        }
    }

    private void FireBullet() {
        GameUtils.lastFireTime = Time.time;
        _weapon.GetComponent<Animator>().SetTrigger("shoot");
        if (!fireContinuously) Audio.PlaySound(Audio.instance.gunshot, 1);
        GameObject bullet = Instantiate(weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
        bullet.GetComponent<Rigidbody2D>().velocity = _bulletSpeed * _fireSpot.transform.right;
    }

    private void FireMultipleBullets() {
        GameUtils.lastFireTime = Time.time;
        _weapon.GetComponent<Animator>().SetTrigger("shoot");
        Audio.PlaySound(Audio.instance.pompagun, 1);
        Vector3 dir = _fireSpot.transform.right;
        foreach (int i in new int[]{-1,0,1}) {
            GameObject bullet = Instantiate(weaponParameters.bulletPrefab, _fireSpot.transform.position, new Quaternion());
            bullet.GetComponent<Rigidbody2D>().velocity = _bulletSpeed * (Quaternion.AngleAxis(i*10, transform.forward) * dir);
        }
    }

    private void Attack() {
        _weapon.GetComponent<Animator>().SetTrigger("attack");
        Audio.PlaySound(Audio.instance.swoosh, 1);
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
                    fireContinuously = true;
                    _machineAudio = true;
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
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<PlayerAttack>().enabled = false;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _hands.gameObject.SetActive(false);
        transform.parent.Find("Crosshair").gameObject.SetActive(false);
        transform.parent.Find("Recharge").gameObject.SetActive(false);
        GameUtils.DeathAnimation(gameObject, GetComponent<Animator>());
        Destroy(gameObject, 2f);

        foreach (AudioSource audio in FindObjectsOfType<AudioSource>()){
            if (audio != Audio.instance.BgMusic) audio.Stop();
        }
        Audio.PlaySound(Audio.instance.playerHit, 1);
    }

    private void OnDestroy() {
        if(gameObject.scene.isLoaded)
            GameObject.Find("Pause").GetComponent<PauseMenu>().GameOver();
    }
}