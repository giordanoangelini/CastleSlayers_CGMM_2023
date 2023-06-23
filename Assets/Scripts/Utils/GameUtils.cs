using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public static class GameUtils
{
    private static float _epsilon = 0.05f;
    private static Vector3 _mousePos;
    public static GameObject player;
    public static bool isPlayerDead = false;
    public static bool isInstantiated = false;
    public static float startTime;
    public static float master_volume = 1;

    public static Dictionary<string, string> default_players = new Dictionary<string, string>(){
        {"Blake","weapon_1"},
        {"Pink","weapon_4"},
        {"Spike","weapon_5"}
    };

    public static string character = default_players.Keys.First();
    public static string weapon = default_players[character];
    public static float lastFireTime = Time.time;

    public static void FlippingCharacterOnMove(Rigidbody2D rigidbody) {
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (rigidbody.velocity.x * _currentScale.x < 0) {
            rigidbody.transform.localScale = Flip_x(_currentScale);
        }
    }

    public static void FlippingCharacterOnAim(Rigidbody2D rigidbody) {
        _mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        Vector3 _currentScale = rigidbody.transform.localScale;
        if (ViewportHandler(_mousePos.x) * _currentScale.x < 0) {
            rigidbody.transform.localScale = Flip_x(_currentScale); 
        }
    }

    public static Vector3 Flip_x(Vector3 scale) {
        return new Vector3(
            -scale.x,
            scale.y,
            scale.z
        );
    }

    private static int ViewportHandler(float num) {
        if (num < 0.5f) return -1;
        else return 1;
    }

    public static void MoveAnimations(Rigidbody2D rigidbody, Animator animator) {
        if (Mathf.Abs(rigidbody.velocity.x) > _epsilon || Mathf.Abs(rigidbody.velocity.y) > _epsilon) {
            animator.ResetTrigger("stop");
            animator.SetTrigger("move");
        } else {
            animator.ResetTrigger("move");
            animator.SetTrigger("stop");
        }
    }

    public static void DeathAnimation(GameObject obj, Animator animator) {
        animator.SetTrigger("death");
        obj.GetComponent<Collider2D>().enabled = false;
    }

    public static Vector2 AttackArea(float scale_x, float position_x, float position_y, float attack_range) {
        if (scale_x < 0) {
            return new Vector2(position_x - attack_range, position_y + attack_range);
        }
        return new Vector2(position_x + attack_range, position_y + attack_range);
    }

    public static void Float(Transform visual, float pulsation = 8f, float amplitude = 0.01f, float phase = 0f) {
        visual.position = new Vector3(
            visual.position.x, 
            visual.position.y + Mathf.Sin(Time.time * pulsation + phase) * amplitude, 
            0
        );
    }
}
