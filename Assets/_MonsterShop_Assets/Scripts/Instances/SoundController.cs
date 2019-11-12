using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instanz that controlls SFX and BGM
/// </summary>
public class SoundController : MonoBehaviour
{
    public static SoundController inst;

    /* Steal from previous Soundmanager */
    //array for the SFX clips
    //enums for the SFX clips
    //array for the BGM clips
    //enums for the BGM clips
    //check if enum and array have same length

    void Start()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

    }

    public void StartBGM()
    {

    }

    public void FadeOverToBGM()
    {

    }

    public void StopBGM()
    {

    }

    public void OneShotSFX()
    {

    }
    
    public void LoopSFX()
    {

    }
}
