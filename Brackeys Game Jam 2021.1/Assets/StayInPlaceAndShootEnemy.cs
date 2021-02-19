using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInPlaceAndShootEnemy : Enemy
{
    [SerializeField] float distanceToPlayer = 4f;

    [SerializeField] float howOftenShoot;
    float shootCounter;

    [SerializeField] GameObject bullet;

    [SerializeField] GameObject firePoint;

    private void Update()
    {
        shootCounter -= Time.deltaTime;

        if(Vector2.Distance(transform.position, player.transform.position) < distanceToPlayer)
        {
            if(shootCounter <= 0f)
            {
                Instantiate(bullet,firePoint.transform.position, Quaternion.identity);
                shootCounter = howOftenShoot;
            }
        }
    }
}
