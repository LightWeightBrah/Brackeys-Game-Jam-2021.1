using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCymbal : Enemy
{
    [SerializeField] int damage;

    [SerializeField] float rangeToAttackPlayer;

    bool hasLanded;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;

    [SerializeField] float range;

    bool isInRange;

    Rigidbody2D rb;

    Animator animator;

    [SerializeField] GameObject checkForPlayer;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask whatIsPlayer;

    Vector2 position;

    private void Start()
    {
        position = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!hasLanded)
        {
            if (Physics2D.OverlapBox(checkForPlayer.transform.position, boxSize, 0f, whatIsPlayer))
            {
                player.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }

        if (!isInRange)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > range)
            {
                rb.velocity = Vector2.zero;
                transform.position = position;
                return;
            }
            else
            {
                isInRange = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasLanded)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/en_cymbal_crash", transform.position);
            animator.SetBool("isGrounded", true);
            return;
        }
        else
        {
            hasLanded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(checkForPlayer.transform.position, boxSize);
    }
}
