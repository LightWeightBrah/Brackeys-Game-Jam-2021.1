using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : Enemy
{
    [SerializeField] int damage;

    [SerializeField] GameObject checkForPlayer;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask whatIsPlayer;
    private void Update()
    {
        if(Physics2D.OverlapBox(checkForPlayer.transform.position,boxSize, 0f, whatIsPlayer))
        {
            player.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(checkForPlayer.transform.position, boxSize);
    }

}
