using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShootingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
