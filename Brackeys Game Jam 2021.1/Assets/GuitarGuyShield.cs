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

    private FMOD.Studio.EventInstance gtrShieldSound;
    

    [HideInInspector]
    public bool canUseShield;


    FMOD.Studio.PARAMETER_DESCRIPTION pd;
    FMOD.Studio.PARAMETER_ID pID;



    void Start()
    {
        shield.gameObject.SetActive(false);
        durationCounter = durationOfShield;
        gtrShieldSound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/char_gtr_shield");
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("GtrShieldEnd", out pd);
        pID = pd.id;
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
                gtrShieldSound.start();
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
                    FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 1f);
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
