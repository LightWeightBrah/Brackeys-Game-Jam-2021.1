using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void SetMasterVolume(float volume)
    {
        Debug.Log("Setting Master volume to " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("Setting SFX volume to " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Setting Music volume to " + volume);
    }
}
