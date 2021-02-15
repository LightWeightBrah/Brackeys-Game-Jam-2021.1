using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightEnemy : Enemy
{
    [SerializeField] float speed;

    bool isMovingRight = true;

    [SerializeField] Transform groundCheck;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f);
        if(ground.collider == false)
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
}
