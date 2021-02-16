using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float timeBetweenTakingDamage = 0.5f;
    float takeDamageCounter;

    public int health;
    public void TakeDamage(int damage)
    {
        if(takeDamageCounter > 0)
        {
            takeDamageCounter -= Time.deltaTime;
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
