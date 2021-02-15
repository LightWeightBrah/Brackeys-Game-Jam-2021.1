using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;

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