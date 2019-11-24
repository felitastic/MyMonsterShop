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
    //public UIController CurUI;

    //UIs
    public HomeUI homeUI;
    public RunnerUI runnerUI;

    //MonsterManager
    public MM_Home homeMonsterManager;
    public MM_GameResult gameResultMonsterManager;


    [Header("Monster Values")]
    public CameraMovement HomeCam;

    public int CurPlayerID;
    public int PlayerMoney = 0;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;

    //left, middle, right -> used as (int) to get the right monsterslot ID
    public ecurMonsterSlot curMonsterSlot;

    //momentane unlocked slots and creatures, die der Spieler hat
    //public CurrentMonster[] CurMonsters = new CurrentMonster[3];
    public MonsterSlot[] CurMonsters;

    //public CurrentMonster thisMonster = new CurrentMonster();

    //Lautstärke vom Spieler eingestellt
    public float BGMVolume;
    public float SFXVolume;

    //private bool isLoading;
    //private AsyncOperation asyncOperation;

    //private GameObject monsterBody;

    //Scripte für die Minigames
    public RunnerController runnerController;

    //private bool isLoading = false;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        Application.targetFrameRate = 60;        
    }
    private void Start()
    {
        WriteEmptySlots();
        ChangePlayerGold(+100);
        homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
        homeMonsterManager.SetSlotSymbol();
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
    }

    public void ChangePlayerGold(int value)
    {
        PlayerMoney += value;
        print("player gold: " + PlayerMoney);
        homeUI.SetGoldCounter();
    }

    //private void Update()
    //{
    //    //if (isLoading)
    //    //{
    //    //    //show loading screen

    //    //    if (asyncOperation.isDone)
    //    //    {
    //    //        print("scene loaded");
    //    //        isLoading = false;
    //    //        homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
    //    //        SpawnAllCurrentMonsters();
    //    //        homeUI.SetGoldCounter();
    //    //        homeMonsterManager.CalculateMonsterValue();
    //    //    }
    //    //}

    //    //if (isLoading)
    //    //{
    //    //    if (asyncOperation.progress == 0.9f)
    //    //    {
    //    //        asyncOperation.allowSceneActivation = true;
    //    //        //SceneManager.LoadScene(0);
    //    //        homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
    //    //        SpawnAllCurrentMonsters();
    //    //        homeUI.SetGoldCounter();
    //    //        homeMonsterManager.CalculateMonsterValue();
    //    //        isLoading = false;
    //    //    }
    //    //}
    //}




    public IEnumerator TestLoad()
    {
        asyncOperation = SceneManager.LoadSceneAsync(0);
        
        //yield return new WaitUntil(asyncOperation.isDone);

        while(!asyncOperation.isDone)
        {
            print("loading in progress");
            //if (homeUI != null)
            //{
            //    print("home ui loaded");
            //}            
            
            //if (homeMonsterManager != null)
            //{
            //    print("home mm loaded");
            //}
            yield return StartCoroutine(WaitForThing());
        }

        print("got em all");
        //yield return null;
    }    

    public IEnumerator WaitForThing()
    {
        print("waiting for thing");
        if (asyncOperation.isDone)
        {
            print("asyn has finished");
            SpawnAllCurrentMonsters();
            homeMonsterManager.CalculateMonsterValue();
            homeUI.SetGoldCounter();
        }
        yield return null;
    }

    public IEnumerator cLoadHomeScene()
    {
        print("loading new scene");
        SceneManager.LoadScene(0);

        yield return new WaitForSeconds(2.0f);

        //homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);


        //asyncOperation = SceneManager.LoadSceneAsync(0);
        //isLoading = true;

        //while(asyncOperation.progress < 1f && isLoading)
        //{
        //    print("loading scene");

        //    if (asyncOperation.progress >= 0.9f)
        //    {
        //        isLoading = false;

        //    }
        //}

    }





    public void SpawnAllCurrentMonsters()
    {
        print("spawning monsters");
        foreach(MonsterSlot slot in CurMonsters)
        {
            if (slot.Monster != null)
            {
                homeMonsterManager.SpawnAnyMonster(slot.Monster.CreaturePrefabs[(int)homeMonsterManager.CurMonster.Rarity, (int)homeMonsterManager.CurMonster.MonsterStage], homeMonsterManager.MonsterSpawn[slot.SlotID]);
                print("slot " + slot.SlotID + " contains monster: " + slot.Monster.MonsterType);
            }
            else
                print("slot " + slot.SlotID + " is empty");
        }
    }
}
