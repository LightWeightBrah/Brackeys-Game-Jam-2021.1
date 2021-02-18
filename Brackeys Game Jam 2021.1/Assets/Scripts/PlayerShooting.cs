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
    PlayerMovement playerMovement;
    public float shootingAnimationTime;

    private void Awake()
    {
        charSwitch = GetComponent<CharacterSwitch>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        timeBtwShots = 0.35f;
    }

    bool shootanimplaying = false;
    void Update()
    {
        if (isPaused) return;

        if (Input.GetKeyDown(KeyCode.Space) && shotsCounter <= 0)
        {
            if (!shootanimplaying)
            {
                charSwitch.ChangeAnimationState(charSwitch.shootAnim);
                shootanimplaying = true;
            }
            playerMovement.walkMode = PlayerMovement.WalkMode.Shoot;
            shotsCounter = timeBtwShots - .075f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerMovement.walkMode = PlayerMovement.WalkMode.Reset;
            charSwitch.ChangeAnimationState("");
            shootanimplaying = false;
        }

        if (shotsCounter > 0)
        {
            shotsCounter -= Time.deltaTime;
        }
        else if (playerMovement.walkMode == PlayerMovement.WalkMode.Shoot)
        {
            if (!shootanimplaying)
            {
                charSwitch.ChangeAnimationState(charSwitch.shootAnim);
                shootanimplaying = true;
            }
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            shotsCounter = timeBtwShots;
        }
    }
}
