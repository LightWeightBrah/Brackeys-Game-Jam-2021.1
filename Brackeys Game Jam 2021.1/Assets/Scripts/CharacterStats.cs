using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Animations")]
    public Animator animator;
    public string idleAnim;
    public string runAnim;
    public string jumpAnim;
    public string shootAnim;

    [Header("Movement")]
    public float speed;
    public float jumpForce;

    public int extraJumps;

    [Header("Shooting")]
    public float timeBtwShots;
    public Transform firePoint;
    public GameObject projectile;

    [Header("UI")]
    public Sprite characterIcon;

    PlayerShooting shooting;

    private void Awake()
    {
        shooting = GetComponentInParent<PlayerShooting>();
    }

    public void Shoot()
    {
        shooting.Shoot();
    }

    public void StopShooting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return;
        }
        shooting.isShooting = false;
    }
}
