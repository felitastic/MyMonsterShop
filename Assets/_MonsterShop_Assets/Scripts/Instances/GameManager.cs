using core;
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

    //VFX
    public VFX_Home vfx_home;
    public VFX_Runner vfx_runner;

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

    //how many taps til egg hatches
    public int TapsToHatch = 3;

    public int StrokesPerPettingSession = 3;
    public float XPGainPerPettingSession = 10.0f;
    public float XPAffectionBonus = 5.0f;
    /// <summary>
    /// Reihenfolge wie eMonsterType
    /// </summary>
    public bool[] UnlockedLogEntries = new bool[9];

    //Timer
    //For the dungeon lord timer
    public System.DateTime DungeonLordWaitTimeEnd;
    public bool DLIsGone;
    public float DLWaitInMinutes = 10.0f;

    //Other timers
    public MonsterTimer monsterTimer;
    //How long until you can pet again
    public float petWaitInMinutes = 2.0f;
    //How long until you can play again
    public float playWaitInMinutes = 4.0f;

    //Scripte für die Minigames
    public RunnerController runnerController;


    //Cheats
    public bool getEpic;
    public bool getLegendary;
    public bool TutorialOn;

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
        DLIsGone = true;
        SetDLTimer();
        petWaitInMinutes = 3.0f;
        playWaitInMinutes = 2.0f;
        TutorialOn = true;
    }

    private void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0, -20);
        CurCamHomePos = Camera.main.transform.position;
        HomeCam.SetScreen(ecurMonsterSlot.middle);
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

    public void SetDLTimer()
    {
        DungeonLordWaitTimeEnd = DateTime.Now.AddMinutes(DLWaitInMinutes);
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

    /// <summary>
    /// Used to change money to its actual value via cheat or on reload
    /// </summary>
    /// <param name="value"></param>
    public void ChangePlayerGold(int value)
    {
        PlayerMoney += value;
        homeUI.SetGoldCounter();
    }

    /// <summary>
    /// Used to change money with fancy count animation when you sell a monster
    /// </summary>
    /// <param name="value"></param>
    /// <param name="oldValue"></param>
    public void ChangePlayerGold(int value, int oldValue)
    {
        PlayerMoney += value;
        //print("player gold: " + PlayerMoney);
        StartCoroutine(homeUI.cSetGoldCounter(oldValue));
    }

    public void LoadHomeScene()
    {
        print("loading new scene");
        SetPlayTimer(curMonsterID);
        curScreen = eScene.home;
        SceneManager.LoadScene(0);
    }    
}
