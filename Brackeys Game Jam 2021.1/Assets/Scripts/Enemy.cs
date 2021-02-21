using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    protected GameObject player;

    public int health;

    public GameObject deathParticle;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy();
    }

    public void Destroy()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
