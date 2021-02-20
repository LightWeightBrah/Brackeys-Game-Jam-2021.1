using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllPlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] float timeBetweenTakingDamage = 0.5f;
    float takeDamageCounter;

    public int health;
    [SerializeField] TextMeshProUGUI hpText;

    public bool isShielded;

    [SerializeField] CharacterSwitch charSwitch;
    [SerializeField] Color colorWhenHit;
    [SerializeField] Color defaultColor;

    void Start()
    {
        hpText.text = health.ToString();
    }

    void Update()
    {
        takeDamageCounter -= Time.deltaTime;

        if(takeDamageCounter < 0)
        {
            charSwitch.sr.color = defaultColor;
        }

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

            charSwitch.sr.color = colorWhenHit;
            health -= damage;
            takeDamageCounter = timeBetweenTakingDamage;
        }


        if(health >= 0)
        {
            hpText.text = health.ToString();
        }

        if (health <= 0)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        Debug.Log("Player has died");
    }
}
