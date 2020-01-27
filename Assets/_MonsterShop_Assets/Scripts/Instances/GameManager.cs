﻿using core;
using System;
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
    //public UIController CurUI;

    //UIs
    public HomeUI homeUI;
    public RunnerUI runnerUI;

    //MonsterManager
    public MM_Home homeMonsterManager;
    public MM_Runner runnerMonsterManager;
    public Monster_Kompendium monsterKompendium;

    [Header("Monster Values")]
    public CameraMovement HomeCam;

    public int CurPlayerID;
    public int PlayerMoney = 0;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;
    public bool DungeonlordWaiting;
    public eScene curScreen;

    //left, middle, right screen in home
    public ecurMonsterSlot curMonsterSlot;
    //uses above value as (int) to get the currently selected MonsterSlot ID
    public int curMonsterID { get { return CurMonsters[(int)curMonsterSlot].SlotID; } }

    public Vector3 CurCamHomePos;

    public Rigidbody[] curMonsterRigid;
    public Animator[] curMonsterAnim;

    //momentane unlocked slots and creatures, die der Spieler hat
    //public CurrentMonster[] CurMonsters = new CurrentMonster[3];
    public MonsterSlot[] CurMonsters;


    public int StrokesPerPettingSession = 5;
    public float XPGainPerPettingSession = 100.0f;
    public float XPAffectionBonus = 50.0f;
    /// <summary>
    /// Reihenfolge wie eMonsterType
    /// </summary>
    public bool[] UnlockedLogEntries = new bool[9];

    //For the timers
    public System.DateTime DungeonLordWaitTimeEnd;

    //Timer
    public MonsterTimer monsterTimer;
    //How long until you can pet again
    public float petWaitInMinutes = 2.0f;
    //How long until you can play again
    public float playWaitInMinutes = 4.0f;

    //Scripte für die Minigames
    public RunnerController runnerController;

    //Lautstärke vom Spieler eingestellt
    //public float BGMVolume;
    //public float SFXVolume;

    //private bool isLoading;
    //private AsyncOperation asyncOperation;

    //private bool isLoading = false;
    //private AsyncOperation asyncOperation;

    private void Awake()
    {
        Application.targetFrameRate = 60;        
        WriteEmptySlots();
        curScreen = eScene.home;
        DungeonlordWaiting = true;
        petWaitInMinutes = 3.0f;
        playWaitInMinutes = 2.0f;
    }

    private void Start()
    {
        HomeCam.SetScreen(ecurMonsterSlot.middle);
        CurCamHomePos = Camera.main.transform.position;
        homeUI.TrainButtonActive(false);
        ChangePlayerGold(+500);
    }

    private void WriteEmptySlots()
    {
        CurMonsters = new MonsterSlot[3];

        for (int i = 0; i < 3; i++)
        {
            CurMonsters[i] = new MonsterSlot();
            CurMonsters[i].SlotID = i;
            //print("slot"+i+" :" + CurMonsters[i]);
        }

        CurMonsters[1].Unlocked = true;
        CurMonsters[0].UnlockPrice = 50;
        CurMonsters[2].UnlockPrice = 150;
    }

    /// <summary>
    /// Sets the pet timer to now-time plus waiting time
    /// </summary>
    /// <param name="slotID"></param>
    public void SetPetTimer(int slotID)
    {
        CurMonsters[slotID].PetTimerEnd = DateTime.Now.AddMinutes(petWaitInMinutes);
    }

    /// <summary>
    /// Sets the play timer to now-time plus waiting time
    /// </summary>
    /// <param name="slotID"></param>
    public void SetPlayTimer(int slotID)
    {
        CurMonsters[slotID].PlayTimerEnd = DateTime.Now.AddMinutes(playWaitInMinutes);
    }

    public void ChangePlayerGold(int value)
    {
        PlayerMoney += value;
        //print("player gold: " + PlayerMoney);
        homeUI.SetGoldCounter();
    }

    public void LoadHomeScene()
    {
        print("loading new scene");
        SetPlayTimer(curMonsterID);
        curScreen = eScene.home;
        SceneManager.LoadScene(0);
    }


    //public IEnumerator TestLoad()
    //{
    //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
    //    //yield return new WaitUntil(asyncOperation.isDone);
    //    while (!asyncOperation.isDone)
    //    {
    //        print("loading in progress");
    //        //    //if (homeUI != null)
    //        //    //{
    //        //    //    print("home ui loaded");
    //        //    //}            
    //        //    //if (homeMonsterManager != null)
    //        //    //{
    //        //    //    print("home mm loaded");
    //        //    //}
    //        //    //SpawnAllCurrentMonsters();
    //        //    //homeMonsterManager.CalculateMonsterValue();
    //        //    //homeUI.SetGoldCounter();
    //        yield return null;
    //    }
    //    print("loading done");
    //    //GetImportantScripts();
    //    //SceneManager.LoadScene(0);
    //    //print("loading scene");
    //    //yield return new WaitForSeconds(1.5f);
    //    //GetImportantScripts();
    //    //print("loading done");
    //    //homeUI.DisableLoadingScreen();
    //}    
}
