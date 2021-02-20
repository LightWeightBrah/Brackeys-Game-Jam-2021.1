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

    void Update()
    {
        if(!shouldUseBoxCollider)
        {
            if (Physics2D.OverlapCircle(checkForPlayer.transform.position, circleSize, whatIsPlayer))
            {
                player.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
        else
        {
            if (Physics2D.OverlapBox(checkForPlayer.transform.position, boxSize, whatIsPlayer))
            {
                player.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
        

        transform.position += transform.right * speed * Time.deltaTime;

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
}
