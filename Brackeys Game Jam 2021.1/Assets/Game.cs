using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeSpeed;
    bool fadeToBlack, fadeOutBlack;

    [SerializeField] float waitToLoad = 1f;

    int counter;

    public GameObject[] cutscenes;

    public bool areCutScenesEnabled;

    [SerializeField] GameObject GameOverScreen;

    public GameObject everythingInCutScene;

    [SerializeField] CharacterSwitch characterSwitch;

    [SerializeField] string currentSceneName;

    [SerializeField] string nextSceneToLoad;

    public bool isAfterBoss;

    public bool isBoss;

    float counterAfterBossScene;

    FMOD.Studio.PARAMETER_DESCRIPTION pd;
    FMOD.Studio.PARAMETER_ID pID;

    void Start()
    {
        counterAfterBossScene = 1.5f;
        fadeOutBlack = true;
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("pauseMenu", out pd);
        pID = pd.id;
    }

    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        counterAfterBossScene -= Time.deltaTime;

        if (areCutScenesEnabled && Input.anyKeyDown)
        {
            if(!isAfterBoss)
            {
                StartCoroutine(ShowNextCutscene());
            }
            else
            {
                if(counterAfterBossScene < 0)
                {
                    StartCoroutine(ShowNextCutscene());
                }
            }
        }
    }

    public IEnumerator ShowNextCutscene()
    {
        if(!isAfterBoss)
        {
            characterSwitch.SetIsPaused();
        }

        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        StartFadeOutBlack();

        if (counter + 1 == cutscenes.Length)
        {
            if(isBoss)
            {
                FindObjectOfType<BugBoss>().canMove = true;
            }

            if(!isAfterBoss)
            {
                characterSwitch.UNSetIsPaused();
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
            everythingInCutScene.gameObject.SetActive(false);
            areCutScenesEnabled = false;
        }
        else
        {
            cutscenes[counter].gameObject.SetActive(false);
            counter++;
            cutscenes[counter].gameObject.SetActive(true);
        }
    }

    public void GameOver()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/char_die", transform.position);
        GameOverScreen.gameObject.SetActive(true);
    }

    public void WinGame()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(nextSceneToLoad);
    }

   
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void StartFadeOutBlack()
    {
        fadeToBlack = false;
        fadeOutBlack = true;
    }

    public void GoToMenu()
    {
        StartCoroutine(StartGoToMenu());
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(pID, 0f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    IEnumerator StartGoToMenu()
    {
        Time.timeScale = 1f;
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene("MainMenu");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WinGame();
        }
    }

}
