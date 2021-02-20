using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float timeBetweenTakingDamage = 0.5f;
    float takeDamageCounter;

    public int health;

    public bool isShielded;

    void Update()
    {
        takeDamageCounter -= Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        if (isShielded) return;

        if(takeDamageCounter > 0)
        {
            return;
        }
        else
        {
            Debug.Log("Player takes damage");

            health -= damage;
            takeDamageCounter = timeBetweenTakingDamage;
        }

        if(health <= 0)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        Debug.Log("Player has died");
    }
}
