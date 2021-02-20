using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    public bool isPaused;
    CharacterSwitch charSwitch;
    PlayerMovement playerMovement;
    public float shootingAnimationTime;

    public bool cantShoot;

    private void Awake()
    {
        charSwitch = GetComponent<CharacterSwitch>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
    }

    bool shootanimplaying = false;
    void Update()
    {
        if (isPaused) return;

        if (cantShoot) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!shootanimplaying)
            {
                charSwitch.ChangeAnimationState(charSwitch.shootAnim);
                shootanimplaying = true;
            }
            playerMovement.walkMode = PlayerMovement.WalkMode.Shoot;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerMovement.walkMode = PlayerMovement.WalkMode.Reset;
            charSwitch.ChangeAnimationState("");
            shootanimplaying = false;
        }

  
        if (playerMovement.walkMode == PlayerMovement.WalkMode.Shoot)
        {
            if (!shootanimplaying)
            {
                charSwitch.ChangeAnimationState(charSwitch.shootAnim);
                shootanimplaying = true;
            }
        }
    }
}
