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

    [SerializeField] GameObject settings;

    bool canUseEscape = true;

    FMOD.Studio.PARAMETER_DESCRIPTION pd;
    FMOD.Studio.PARAMETER_ID pID;

    private void Start()
    {
        Debug.Log(settings.name);
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("pauseMenu", out pd);
        pID = pd.id;
    }

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
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 1f);
        }
        else
        {
            StartCoroutine(StartUNPause());
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 0f);
        }
    }

    public void GoToSettings()
    {
        canUseEscape = false;
        settings.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 0f);
    }

    public void GoFromSettings()
    {
        canUseEscape = true;
        settings.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 1f);
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
