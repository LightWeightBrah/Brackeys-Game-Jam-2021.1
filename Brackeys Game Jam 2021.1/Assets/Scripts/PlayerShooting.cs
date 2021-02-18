using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float timeBtwShots;
    float shotsCounter;

    public Transform firePoint;
    public GameObject projectile;

    public bool isPaused;

    CharacterSwitch charSwitch;

    public bool isShooting;


    private void Awake()
    {
        charSwitch = GetComponent<CharacterSwitch>();
    }

    void Update()
    {
        if (isPaused) return;


        if (Input.GetKey(KeyCode.Space))
        {
            charSwitch.animator.Play(charSwitch.shootAnim);
            isShooting = true;
        }


    }

    public void Shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);

    }
}
