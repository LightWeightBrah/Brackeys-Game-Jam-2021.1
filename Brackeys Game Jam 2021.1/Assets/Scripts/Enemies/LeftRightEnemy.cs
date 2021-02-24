using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemy : Enemy
{
    [SerializeField] int damage;
    [SerializeField] float speed;

    [SerializeField] bool isMovingRight = true;

    [SerializeField] Transform groundCheck;

    [SerializeField] GameObject checkForPlayer;
    [SerializeField] float circleSize;
    [SerializeField] LayerMask whatIsPlayer;

    [SerializeField] bool shouldUseBoxCollider;

    [SerializeField] Vector2 boxSize;

    [SerializeField] LayerMask whatIsGround;

    [SerializeField] GameObject checkForWall;
    FMOD.Studio.EventInstance dieSound;
    FMOD.Studio.EventInstance ballSound;

    private bool soundHasPlayed = false;

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 2f)
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
            Debug.Log("Bicko");
        }


        if (!shouldUseBoxCollider)
        {
            if (Physics2D.OverlapCircle(checkForPlayer.transform.position, circleSize, whatIsPlayer))
            {
                RaycastHit2D circle = Physics2D.Raycast(checkForPlayer.transform.position, boxSize, whatIsPlayer);

                if (circle.collider.gameObject.tag == "Player")
                {
                    player.GetComponent<IDamageable>().TakeDamage(damage);
                    Debug.Log("Bicko2");
                }

            }
        }
        else
        {
            if (Physics2D.OverlapBox(checkForPlayer.transform.position, boxSize, whatIsPlayer))
            {
                RaycastHit2D box = Physics2D.Raycast(checkForPlayer.transform.position, boxSize, whatIsPlayer);

                if (box.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Bicko2");
                    player.GetComponent<IDamageable>().TakeDamage(damage);

                }

            }
        }


        transform.position += transform.right * speed * Time.deltaTime;
        if (!soundHasPlayed)
        {
            ballSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/en_blob");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(ballSound, transform, GetComponent<Rigidbody2D>());
            ballSound.start();
            soundHasPlayed = true;
        }
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, whatIsGround);

        RaycastHit2D isWall = Physics2D.Raycast(checkForWall.transform.position, Vector2.down, 1f, whatIsGround);

        if(ground.collider == false || isWall)
        {
            if(isMovingRight)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180, transform.eulerAngles.z);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                isMovingRight = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(!shouldUseBoxCollider)
        {
            Gizmos.DrawWireSphere(checkForPlayer.transform.position, circleSize);
        }
        else
        {
            Gizmos.DrawWireCube(checkForPlayer.transform.position, boxSize);
        }
    }

    private void OnDestroy()
    {
        ballSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        dieSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/en_die");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(dieSound, transform, GetComponent<Rigidbody2D>());
        dieSound.start();
        dieSound.release();
    }
}
