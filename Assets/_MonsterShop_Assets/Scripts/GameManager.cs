using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    //ui controller schreibt sich bei onenable/awake hier rein
    public UIController CurUI;
    
    public int CurPlayerID;

    public int CurCreatureSlot;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;
    public eScene CurScene;

    //momentane creatures, die der Spieler hat
    public List<MonsterSlot> CurMonsters = new List<MonsterSlot>(3);

    //Lautstärke vom Spieler eingestellt
    public float BGMVolume;
    public float SFXVolume;




    void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScene(eScene curScene)
    {
        CurScene = curScene;

        switch (CurScene)
        {
            case eScene.home:

                break;
            case eScene.shop:
                break;
            case eScene.minigames:
                break;
            case eScene.endlessRunner:
                break;
            default:
                break;
        }
    }
}
