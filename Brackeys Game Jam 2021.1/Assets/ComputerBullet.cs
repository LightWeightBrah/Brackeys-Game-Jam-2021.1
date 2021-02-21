using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] int damage;

    [SerializeField] float rayDistance;
    [SerializeField] LayerMask whatIsSolid;

    bool canMove;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, rayDistance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            transform.parent = hitInfo.collider.transform;

            if (!hitInfo.collider.CompareTag("Player"))
            {
                canMove = true;
            }

            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponentInParent<IDamageable>().TakeDamage(damage);
                DestroyProjectile();
            }
        }

        if(!canMove)
        {
            transform.position += transform.right * speed * Time.deltaTime;

        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
