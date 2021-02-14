using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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
}
