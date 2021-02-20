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
    FMOD.Studio.EventInstance shoot;
    FMOD.Studio.EventInstance dieSound;

    private void Update()
    {
        shootCounter -= Time.deltaTime;

        if(Vector2.Distance(transform.position, player.transform.position) < distanceToPlayer)
        {
            if(shootCounter <= 0f)
            {
                Instantiate(bullet,firePoint.transform.position, Quaternion.identity);
                shootCounter = howOftenShoot;
                shoot = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/en_pencil_shoot");
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(shoot, transform, GetComponent<Rigidbody2D>());
                shoot.start();
                shoot.release();
            }
        }
    }

    private void OnDestroy()
    {
        dieSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/en_die");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(dieSound, transform, GetComponent<Rigidbody2D>());
        dieSound.start();
        dieSound.release();
    }
}
