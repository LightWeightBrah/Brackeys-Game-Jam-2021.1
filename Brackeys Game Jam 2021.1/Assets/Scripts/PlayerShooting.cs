using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float timeBtwShots;
    float shotsCounter;

    public Transform firePoint;
    public GameObject projectile;

    void Update()
    {
        if(shotsCounter > 0)
        {
            shotsCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                shotsCounter = timeBtwShots;
            }
        }

    }
}
