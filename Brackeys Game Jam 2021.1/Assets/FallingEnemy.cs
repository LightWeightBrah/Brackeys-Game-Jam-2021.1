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

    private void Start()
    {
        waitCounter = timeToWaitAfterLanding;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangeToAttackPlayer);
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < rangeToAttackPlayer)
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
        }

        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (!hasLanded) return;

        if(waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            return;
        }

        if(transform.position.x < player.transform.position.x)
        {
            //enemy is to the left side of the player => move right
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
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
