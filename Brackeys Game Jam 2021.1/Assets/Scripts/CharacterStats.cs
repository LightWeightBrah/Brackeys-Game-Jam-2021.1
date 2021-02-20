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
    public SpriteRenderer sr;

    [Header("Movement")]
    public float speed;
    public float jumpForce;

    public int extraJumps;

    [Header("Shooting")]
    public float timeBtwShots;
    public Transform firePoint;
    public GameObject projectile;
    public bool cantShoot;

    [Header("Shield")]
    public bool canUseShield;

    [Header("UI")]
    public Sprite characterIcon;

    PlayerShooting shooting;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        shooting = GetComponentInParent<PlayerShooting>();
    }

    public void Shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }

}
