using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODLevelReset : MonoBehaviour
{

    FMOD.Studio.Bus masterBus;

    // Start is called before the first frame update
    void Awake()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
