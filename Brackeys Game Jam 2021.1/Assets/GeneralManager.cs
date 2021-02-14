using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Animator pauseMenuAnimator;

    public Image currentPlayerIcon;

    bool isPaused;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] CharacterSwitch characterSwitch;

    bool canUseEscape = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(canUseEscape)
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            StartCoroutine(StartPause());
        }
        else
        {
            StartCoroutine(StartUNPause());
        }
    }

    IEnumerator StartPause()
    {
        canUseEscape = false;
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        SetPauseOnOtherScripts();
        pauseMenuAnimator.SetBool("isPaused", true);
        yield return new WaitForSecondsRealtime(0.67f);
        canUseEscape = true;
    }

    IEnumerator StartUNPause()
    {
        canUseEscape = false;
        SetPauseOnOtherScripts();
        pauseMenuAnimator.SetBool("isPaused", false);
        yield return new WaitForSecondsRealtime(0.67f);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        canUseEscape = true;
    }

    void SetPauseOnOtherScripts()
    {
        playerMovement.isPaused = !playerMovement.isPaused;
        playerShooting.isPaused = !playerShooting.isPaused;
        characterSwitch.isPaused = !characterSwitch.isPaused;
    }

    public void SwitchPlayerIcon(Sprite newPlayerIcon)
    {
        currentPlayerIcon.sprite = newPlayerIcon;
    }
}
