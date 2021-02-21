using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    int selectedPlayer;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] GuitarGuyShield guitarGuyShield;
    [SerializeField] GeneralManager generalManager;
    [SerializeField] AllPlayerStats allPlayerStats;
    CharacterStats charStats;

    public Animator animator;
    public string currentAnimationState;

    public SpriteRenderer sr;

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
        // Find the current player and the new player
        Transform current = null;
        Transform next = null;
        int i = 0;
        foreach (Transform player in transform)
        {
            if (player.gameObject.activeSelf) current = player;
            if (i++ == selectedPlayer) next = player;
        }
        if (next == null) { Debug.Log("Next not found, expected: " + selectedPlayer); return; }
        next.gameObject.SetActive(true);
        if (current != null)
        {
            next.position = current.position;
            next.rotation = current.rotation;
            next.parent.GetComponent<Rigidbody2D>().velocity = current.parent.GetComponent<Rigidbody2D>().velocity;
            current.gameObject.SetActive(false);
        }

        // Find what was the previous state
        bool wasIdle = currentAnimationState == idleAnim;
        bool wasRun = currentAnimationState == runAnim;
        bool wasJump = currentAnimationState == jumpAnim;
        bool wasShoot = currentAnimationState == shootAnim;

        charStats = next.GetComponent<CharacterStats>();
        SwitchStats();

        if (wasIdle) ChangeAnimationState(idleAnim);
        else if (wasRun) ChangeAnimationState(runAnim);
        else if (wasJump) ChangeAnimationState(jumpAnim);
        else if (wasShoot) ChangeAnimationState(shootAnim);
    }

    void SwitchStats()
    {
        animator = charStats.animator;
        idleAnim = charStats.idleAnim;
        runAnim = charStats.runAnim;
        jumpAnim = charStats.jumpAnim;
        shootAnim = charStats.shootAnim;

        sr = charStats.sr;

        playerMovement.speed = charStats.speed;
        playerMovement.jumpForce = charStats.jumpForce;
        playerMovement.extraJumps = charStats.extraJumps;

        playerShooting.firePoint = charStats.firePoint;
        playerShooting.projectile = charStats.projectile;
        playerShooting.cantShoot = charStats.cantShoot;

        guitarGuyShield.canUseShield = charStats.canUseShield;
        if(guitarGuyShield.isGuitarGuyAvaiable)
        {
            guitarGuyShield.TurnOffShieldOnNewCharacter();
        }
        allPlayerStats.isShielded = false;

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
