using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarGuyShield : MonoBehaviour
{
    [SerializeField] GameObject shield;

    [SerializeField] float howOftenHeCanUseShield;
    float shieldCounter;

    [SerializeField] float durationOfShield;
    float durationCounter;

    [SerializeField] AllPlayerStats allPlayerStats;

    bool hasPressedSpace;

    [HideInInspector]
    public bool canUseShield;

    void Start()
    {
        shield.gameObject.SetActive(false);
        durationCounter = durationOfShield;
    }

    void Update()
    {
        if(shieldCounter > 0)
        {
            shieldCounter -= Time.deltaTime;
        }
        else if(canUseShield)
        {
            if(Input.GetKeyDown(KeyCode.Space) && hasPressedSpace == false)
            {
                hasPressedSpace = true;
                shield.gameObject.SetActive(true);
                //here add SFX turning on shield;
            }

            if(hasPressedSpace)
            {
                if (durationCounter > 0)
                {
                    durationCounter -= Time.deltaTime;
                    allPlayerStats.isShielded = true;
                }
                else
                {
                    SetShieldTimers();
                }
            }
        }
    }

    private void SetShieldTimers()
    {
        shield.gameObject.SetActive(false);
        shieldCounter = howOftenHeCanUseShield;
        durationCounter = durationOfShield;
        hasPressedSpace = false;
        allPlayerStats.isShielded = false;
    }

    public void TurnOffShieldOnNewCharacter()
    {
        shield.gameObject.SetActive(false);
        durationCounter = durationOfShield;
        hasPressedSpace = false;
        allPlayerStats.isShielded = false;
    }
}
