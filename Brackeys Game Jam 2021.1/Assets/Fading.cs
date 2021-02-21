using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeSpeed;
    protected bool fadeToBlack, fadeOutBlack;

    public float waitToLoad;

    public string sceneToLoadName;

    private void Start()
    {
        StartFadeOutBlack();
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

    public virtual void Update()
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

    public void EnterTheGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadSceneAsync(sceneToLoadName);
    }
}
