using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : Enemy
{
    [SerializeField] int damage;

    [SerializeField] float speed;
    [SerializeField] float rangeToAttackPlayer;


    bool hasLanded;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;

    [SerializeField] float timeToWaitAfterLanding;
    float waitCounter;

    [SerializeField] float range;

    bool isInRange;

    Rigidbody2D rb;

    bool facingRight = true;

    bool canRotate;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = timeToWaitAfterLanding;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangeToAttackPlayer);
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    private void Update()
    {


        if (Vector2.Distance(transform.position, player.transform.position) < rangeToAttackPlayer)
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
        }

        ChasePlayer();
    }

    void ChasePlayer()
    {

        if (!isInRange)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > range)
            {
                rb.velocity = Vector2.zero;
                return;
            }
            else
            {
                isInRange = true;
            }

        }

        if (!hasLanded) return;

        if (Vector2.Distance(transform.position, player.transform.position) > 3f)
        {
            canRotate = true;
        }
        else
        {
            canRotate = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Can rotate " + canRotate);
        }

        if (canRotate)
        {
            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }


        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            return;
        }

        if (transform.position.x < player.transform.position.x)
        {
            //enemy is to the left side of the player => move right
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            //enemy is to the right side of the player => move left
            transform.position += -transform.right * speed * Time.deltaTime;
        }


    }

    private void FixedUpdate()
    {
        if (hasLanded) return;
        else
        {
            hasLanded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }
    }


}
