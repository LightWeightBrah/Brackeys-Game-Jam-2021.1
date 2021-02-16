using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] int damage;

    [SerializeField] float rayDistance;
    [SerializeField] LayerMask whatIsSolid;

    Vector2 destination;

    Vector2 direction;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        destination = player.transform.position;
        Invoke("DestroyProjectile", lifeTime);
        direction = player.transform.position - transform.position;
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, rayDistance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<IDamageable>().TakeDamage(damage);
            }
            DestroyProjectile();
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
