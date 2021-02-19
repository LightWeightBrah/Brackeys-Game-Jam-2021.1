using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] allPlayers;
    [SerializeField] List<GameObject> availablePlayers = new List<GameObject>();

    int selectedPlayer;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] GeneralManager generalManager;
    CharacterStats charStats;

    public Animator animator;
    public string currentAnimationState;

    public string idleAnim;
    public string runAnim;
    public string jumpAnim;
    public string shootAnim;

    public bool isPaused;
    public bool isTalking;

    FMOD.Studio.PARAMETER_DESCRIPTION pd;
    FMOD.Studio.PARAMETER_ID pID;


    void Start()
    {
        SelectPlayer();
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("SelectedPlayer", out pd);
        pID = pd.id;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, selectedPlayer);
    }

    void Update()
    {
        if (isPaused) return;

        int previousSelectedPlayer = selectedPlayer;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (selectedPlayer >= transform.childCount - 1)
                selectedPlayer = 0;
            else
                selectedPlayer++;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, selectedPlayer);

        }

        if (previousSelectedPlayer != selectedPlayer)
        {
            SelectPlayer();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/char_change", transform.position);
        }
    }

    void SelectPlayer()
    {
        int i = 0;
        foreach (Transform player in transform)
        {
            if (i == selectedPlayer)
            {
                player.gameObject.SetActive(true);
                charStats = player.GetComponent<CharacterStats>();
                Debug.Log(charStats);
                Debug.Log(playerMovement);
                SwitchStats();
            }
            else
            {
                player.gameObject.SetActive(false);
            }

            i++;
        }
    }

    void SwitchStats()
    {
        animator = charStats.animator;
        idleAnim = charStats.idleAnim;
        runAnim = charStats.runAnim;
        jumpAnim = charStats.jumpAnim;
        shootAnim = charStats.shootAnim;

        playerMovement.speed = charStats.speed;
        playerMovement.jumpForce = charStats.jumpForce;
        playerMovement.extraJumps = charStats.extraJumps;

        playerShooting.timeBtwShots = charStats.timeBtwShots;
        playerShooting.firePoint = charStats.firePoint;
        playerShooting.projectile = charStats.projectile;

        generalManager.SwitchPlayerIcon(charStats.characterIcon);
    }

    public void ChangeAnimationState(string newState)
    {
        if (newState == runAnim || newState == idleAnim)
        {
            animator.SetBool("running", newState == runAnim);
        }
        else if (newState == "")
        {
            if (animator.GetBool("running")) newState = runAnim;
            else newState = idleAnim;
        }

        if(newState != charStats.jumpAnim)
        {
            if (currentAnimationState == newState) return; // Post check
        }

        currentAnimationState = newState;
        animator.Play(newState, 0, 0);
    }

    public void SetIsPaused()
    {
        isPaused = true;
        playerMovement.isPaused = true;
        playerShooting.isPaused = true;
        playerMovement.rb.velocity = Vector2.zero;
    }

    public void UNSetIsPaused()
    {
        isPaused = false;
        playerMovement.isPaused = false;
        playerShooting.isPaused = false;
    }
}
