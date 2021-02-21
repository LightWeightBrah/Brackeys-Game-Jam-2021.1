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

    [SerializeField] GameObject deathScreen;
    bool dieSoundPlayed = false;
    FMOD.Studio.Bus busSFX;
    FMOD.Studio.PARAMETER_DESCRIPTION pd2;
    FMOD.Studio.PARAMETER_ID pID2;

    void Start()
    {
        hpText.text = health.ToString();
        busSFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        busSFX.setVolume(1f);
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("pauseMenu", out pd2);
        pID2 = pd2.id;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID2, 0f);
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
            if (health > 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/char_damage", transform.position);
            }
            

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
        if (!dieSoundPlayed)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/char_die", transform.position);
            dieSoundPlayed = true;
        }
        
        deathScreen.gameObject.SetActive(true);
        Invoke("LowerSfx", 1f);
        charSwitch.SetIsPaused();
        Debug.Log("Player has died");
    }

    public void LowerSfx()
    {
        busSFX.setVolume(0f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID2, 1f);
    }
}
