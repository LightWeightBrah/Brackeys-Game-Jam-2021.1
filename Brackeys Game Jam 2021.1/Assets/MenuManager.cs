using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject HowToPlay;
    public GameObject Settings;
    public Image fadeScreen;
    public float fadeSpeed;
    bool fadeToBlack, fadeOutBlack;

    public GameObject[] thingsInMenu;

    public float waitToLoad;

    private void Start()
    {
        Credits.SetActive(false);
        fadeOutBlack = true;
        fadeToBlack = false;
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

    public void PlayGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/ui_sel", transform.position);
        StartCoroutine(StartGame());
    }

    public void GoBackFrom(GameObject g)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/ui_sel", transform.position);
        StartCoroutine(UnloadUI(g));
    }

    public void GoTo(GameObject g)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/ui_sel", transform.position);
        StartCoroutine(LoadUI(g));
    }

    IEnumerator StartGame()
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene("LV 1");
    }

    IEnumerator LoadUI(GameObject g)
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        foreach(GameObject something in thingsInMenu)
        {
            something.gameObject.SetActive(false);
        }

        g.SetActive(true);

        StartFadeOutBlack();

    }

    IEnumerator UnloadUI(GameObject g)
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        g.SetActive(false);

        MainMenu.gameObject.SetActive(true);

        StartFadeOutBlack();
    }


}
