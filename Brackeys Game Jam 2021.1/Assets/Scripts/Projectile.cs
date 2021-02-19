using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] int damage;

    [SerializeField] float rayDistance;
    [SerializeField] LayerMask whatIsSolid;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, rayDistance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponentInParent<IDamageable>().TakeDamage(damage);
            }
            DestroyProjectile();
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
