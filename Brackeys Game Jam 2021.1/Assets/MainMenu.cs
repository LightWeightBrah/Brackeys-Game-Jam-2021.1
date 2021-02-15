using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    
    FMOD.Studio.Bus busMaster;
    FMOD.Studio.Bus busSFX;
    FMOD.Studio.Bus busMusic;

    

    private void Start()
    {
        busMaster = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        busSFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        busMusic = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
    }

    public void SetMasterVolume(float volume)
    {
        busMaster.setVolume(volume);
        Debug.Log("Setting Master volume to " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        busSFX.setVolume(volume);
        Debug.Log("Setting SFX volume to " + volume);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/sfx_volume", transform.position);
    }

    public void SetMusicVolume(float volume)
    {
        busMusic.setVolume(volume);
        Debug.Log("Setting Music volume to " + volume); 
    }
}
