using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    protected GameObject player;

    public int health;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
