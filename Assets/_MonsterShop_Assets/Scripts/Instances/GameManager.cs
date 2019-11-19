using core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //ui controller schreibt sich bei onenable/awake hier rein
    public UIController CurUI;

    public CameraMovement HomeCam;
    
    public int CurPlayerID;
    public int PlayerMoney;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;
    public eScene CurScene;
    public eCurHomeScreen curHomeScreen;

    //momentane unlocked slots and creatures, die der Spieler hat
    public MonsterSlot[] CurMonsters = new MonsterSlot[3];

    //Lautstärke vom Spieler eingestellt
    public float BGMVolume;
    public float SFXVolume;

    // um zu verhindern, dass andere/Kind Elemente den Konstruktor des GameManagers aufruft
    protected GameManager() { }

    private void Start()
    {
        Application.targetFrameRate = 60;
        PlayerMoney = 500;
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
