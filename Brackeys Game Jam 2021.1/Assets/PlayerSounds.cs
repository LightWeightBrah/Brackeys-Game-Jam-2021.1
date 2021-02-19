using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    private FMOD.Studio.EventInstance footsteps;
    private FMOD.Studio.EventInstance jump;
    private FMOD.Studio.EventInstance shoot;

    // Start is called before the first frame update
    void Start()
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Player/char_footsteps");
        jump = FMODUnity.RuntimeManager.CreateInstance("event:/Player/player_jump");
        shoot = FMODUnity.RuntimeManager.CreateInstance("event:/Player/char_shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayFootstepsEvent()
    {
        footsteps.start();
    }

    void PlayJumpEvent()
    {
        jump.start();
    }

    void PlayShootEvent()
    {
        shoot.start();
    }
}
