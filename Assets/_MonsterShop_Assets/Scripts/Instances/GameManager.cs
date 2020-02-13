using core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // um zu verhindern, dass andere/Kind Elemente den Konstruktor des GameManagers aufruft
    protected GameManager() { }

    [Header("For changes in inspector")]
    [Header("Timer Values")]
    [Tooltip("Timer for the Sell Option in Minutes")]
    public float DLWaitInMinutes = 10.0f;
    [Tooltip("Timer until Monster is sad again in Minutes")]
    public float petWaitInMinutes = 1.0f;
    [Tooltip("Timer until Monster can train again in Minutes")]
    public float playWaitInMinutes = 2.0f;

    [Header("Pet Scene")]
    [Tooltip("How many times monster needs to be stroked per session")]
    public int StrokesPerPettingSession = 3;
    [Tooltip("Base XP gain per petting session")]
    public float XPGainPerPettingSession = 10.0f;
    [Tooltip("Bonus XP gain per petting, every 5 times")]
    public float XPAffectionBonus = 5.0f;

    [Header("Egg Hatching")]
    [Tooltip("How many times to tap the egg until it hatches")]
    public int TapsToHatch = 3;

    [Header("Do not touch")]
    [Header("Scripts")]

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

    //Other timers
    public MonsterTimer monsterTimer;

    //Scripte für die Minigames
    public RunnerController runnerController;
    
    [Header("Dungeonlord Timer")]
    public System.DateTime DungeonLordWaitTimeEnd;
    public bool DLIsGone;

    [Header("Minigame Values")]
    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;

    [Header("Monster stuff")]
    public Rigidbody[] curMonsterRigid;
    public Animator[] curMonsterAnim;
    public MonsterSlot[] CurMonsters;

    //uses above value as (int) to get the currently selected MonsterSlot ID
    public int curMonsterID { get { return CurMonsters[(int)curMonsterSlot].SlotID; } }
    
    [Header("Camera & Scene")]
    public CameraMovement HomeCam;
    [Tooltip("Left = 0, Middle = 1, Right = 2; Screens in Home")]
    public ecurMonsterSlot curMonsterSlot;
    [Tooltip("Which scene: home, runner, other games ...")]
    public eScene curScreen;
    public Vector3 CurCamHomePos;

    [Tooltip("Player ID for local savegames")]
    public int CurPlayerID;

    [Tooltip("Money the player owns")]
    public int PlayerMoney = 0;

    [Tooltip("Kompendium Entries that are unlocked")]
    public bool[] UnlockedLogEntries = new bool[12];

    [Tooltip("True if the monster is sad and can be stroked")]
    public bool petting;

    [Header("For the cheats")]
    public bool getEpic;
    public bool getLegendary;

    [Header("Other")]
    public bool TutorialOn;

    [Tooltip("Notifications for the pet timer")]
    AndroidNotification[] PetNotifs = new AndroidNotification[3];
    AndroidNotification DungeonNotif;
        
    private void Awake()
    {
        Application.targetFrameRate = 60;        
        WriteEmptySlots();
        curScreen = eScene.home;
        DLIsGone = true;
        SetDLTimer();
        TutorialOn = true;
        petting = false;
    }

    private void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0, -20);
        CurCamHomePos = Camera.main.transform.position;
        HomeCam.SetScreen(ecurMonsterSlot.middle);
        homeUI.TrainButtonActive(false);
        ChangePlayerGold(+50);

        var c = new AndroidNotificationChannel()
        {
            Id = "normal",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Timer notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public void SendNotification(DateTime FireTime, string msg, string title, int monsterID = 3)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = msg;
        notification.FireTime = FireTime;
        if (monsterID < 3)
            PetNotifs[monsterID] = notification;
        else
            DungeonNotif = notification;

        AndroidNotificationCenter.SendNotification(notification, "normal");
        print("Firing notif: " + msg);
    }

    public void CancelNotification(int monsterID)
    {
        //AndroidNotification notif = PetNotifs[monsterID];
        //var identifier = AndroidNotificationCenter.SendNotification(notif, "normal");
        //AndroidNotificationCenter.CancelScheduledNotification(identifier);
        //print("Canceling pet notif for no" + monsterID);
    }

    public void CancelAllNotifs()
    {
        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        //foreach(AndroidNotification notif in PetNotifs)
        //{
        //    var identifier = AndroidNotificationCenter.SendNotification(notif, "normal");
        //    AndroidNotificationCenter.CancelNotification(identifier);
        //}

        //var dungeonidenti = AndroidNotificationCenter.SendNotification(DungeonNotif, "normal");
        //AndroidNotificationCenter.CancelNotification(dungeonidenti);
        //print("Canceling all notifs");
    }

    public void RestartNotifs()
    {
        //print("Restarting all notifs");

        //for (int i = 0; i < 3; i++)
        //{
        //    if (CurMonsters[i].PetTimerEnd > System.DateTime.Now)
        //    {
        //        SendNotification(CurMonsters[i].PetTimerEnd, "A monster is sad and needs affection! :(", "Time for hugs!");
        //    }
        //}

        //if (DungeonLordWaitTimeEnd > System.DateTime.Now)
        //{
        //    SendNotification(DungeonLordWaitTimeEnd, "The dungeonlord wants to buy some new monsters!", "Sell monsters!");           
        //}
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
        CurMonsters[0].UnlockPrice = 1350;
        CurMonsters[2].UnlockPrice = 2700;
    }

    public void SetDLTimer()
    {
        DungeonLordWaitTimeEnd = DateTime.Now.AddMinutes(DLWaitInMinutes);
        //SendNotification(DungeonLordWaitTimeEnd, "The dungeonlord wants to buy some new monsters!", "Sell monsters!");
    }

    /// <summary>
    /// Sets the pet timer to now-time plus waiting time
    /// </summary>
    /// <param name="slotID"></param>
    public void SetPetTimer(int slotID)
    {
        CurMonsters[slotID].PetTimerEnd = DateTime.Now.AddMinutes(petWaitInMinutes);
        //SendNotification(CurMonsters[slotID].PetTimerEnd, "A monster is sad and needs affection! :(", "Time for hugs!");
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
