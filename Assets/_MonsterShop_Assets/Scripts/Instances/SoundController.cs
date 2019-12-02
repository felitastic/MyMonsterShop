using core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instanz that controlls SFX and BGM
/// </summary>
public class SoundController : Singleton<SoundController>
{
    protected SoundController() { }

    public AudioClip[] SFXclips;
    public AudioClip[] BGMclips;

    /* Steal from previous Soundmanager */
    //array for the SFX clips
    //enums for the SFX clips
    //array for the BGM clips
    //enums for the BGM clips
    //check if enum and array have same length

    void Start()
    {
        print("started sound manager");
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
