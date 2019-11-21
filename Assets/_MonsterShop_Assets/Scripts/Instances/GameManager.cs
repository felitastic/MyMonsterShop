using core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // um zu verhindern, dass andere/Kind Elemente den Konstruktor des GameManagers aufruft
    protected GameManager() { }

    [Header("Home Screen Values")]
    //ui controller schreibt sich bei onenable/awake hier rein
    //delete later
    public UIController CurUI;

    //get the real ones later
    public HomeUI homeUI;
    public RunnerUI runnerUI;

    [Header("Monster Values")]
    public CameraMovement HomeCam;
    
    public int CurPlayerID;
    public int PlayerMoney = 0;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;
    //public eScene CurScene;
    public eCurHomeScreen curHomeScreen;

    //momentane unlocked slots and creatures, die der Spieler hat
    //public CurrentMonster[] CurMonsters = new CurrentMonster[3];
    public MonsterSlot[] CurMonsters = new MonsterSlot[3];

    public CurrentMonster thisMonster = new CurrentMonster();

    //Lautstärke vom Spieler eingestellt
    public float BGMVolume;
    public float SFXVolume;

    public bool loading;

    private GameObject monsterBody;

    //Scripte für die Minigames
    public RunnerController runnerController;

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetGold(+100);
    }

    public void SaveCurrentMonsters()
    {
        print("Saving current Monster values");
        thisMonster.CurMonster = CurMonsters[(int)curHomeScreen].CurMonster;
        thisMonster.Unlocked = CurMonsters[(int)curHomeScreen].Unlocked;
        thisMonster.LevelThreshold_current = CurMonsters[(int)curHomeScreen].LevelThreshold_current;
        thisMonster.MonsterStage = CurMonsters[(int)curHomeScreen].MonsterStage;
        thisMonster.Rarity = CurMonsters[(int)curHomeScreen].Rarity;
        thisMonster.CreatureLevel = CurMonsters[(int)curHomeScreen].CreatureLevel;
        thisMonster.CreatureXP = CurMonsters[(int)curHomeScreen].CreatureXP;
        thisMonster.thisSlot = CurMonsters[(int)curHomeScreen].SlotID;
    }

    public void WriteCurrentMonsters()
    {
        if (thisMonster.thisSlot < 3)
        {
            print("Writing current Monster values back");

            CurMonsters[thisMonster.thisSlot].CurMonster = thisMonster.CurMonster;
            CurMonsters[thisMonster.thisSlot].Unlocked = thisMonster.Unlocked;
            CurMonsters[thisMonster.thisSlot].LevelThreshold_current = thisMonster.LevelThreshold_current;
            CurMonsters[thisMonster.thisSlot].MonsterStage = thisMonster.MonsterStage;
            CurMonsters[thisMonster.thisSlot].Rarity = thisMonster.Rarity;
            CurMonsters[thisMonster.thisSlot].CreatureLevel = thisMonster.CreatureLevel;
            CurMonsters[thisMonster.thisSlot].CreatureXP = thisMonster.CreatureXP;
        }
    }

    public void SetGold(int value)
    {
        PlayerMoney += value;
        print("player gold: " + PlayerMoney);
        homeUI.SetGoldCounter();
    }

    public IEnumerator cLoadHomeScene()
    {
        SceneManager.LoadScene(0);

        WriteCurrentMonsters();
        //SetGold(PlayerMoney);
        //CurMonsters[(int)curHomeScreen].SpawnCurrentMonster(CurMonsters[(int)curHomeScreen].MonsterSpawn);
        yield return new WaitForSeconds(0.25f);
    }


    //public void SetScene(eScene curScene)
    //{
    //    CurScene = curScene;

    //    switch (CurScene)
    //    {
    //        case eScene.home:

    //            break;
    //        case eScene.shop:
    //            break;
    //        case eScene.minigames:
    //            break;
    //        case eScene.endlessRunner:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
