using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : UIController
{
    private Monster curEgg;
    private int hatchTaps;
    //private Vector3 camHomePos { get { return GM.CurCamHomePos; } }
    private Vector3 camShopPos;
    //private Vector3 camDungeonPos;
    private eHomeUIScene curScene;
    private int EggHatchCount;

    // called via (int)GM.curMonsterSlot
    public SpriteRenderer[] HomeBGs;

    public Sprite[] BGsprites;

    private enum BGsprite
    {
        homeLeft,
        homeMiddle,
        homeRight,
        dungeonLeft,
        dungeonMiddle,
        dungeonRight,
    }

    private enum eMenus
    {
        PlayerInfo,
        MonsterStats,
        XPBar,
        Home,
        Shop,
        Dungeon,
        MiniGameWindow,
        H_BottomButtons,
        S_BottomButtons,
        S_PurchaseConfirm,
        S_EggMenu,
        S_TappEggButton,
        D_BottomButtons,
        SwipeButtons,
        LoadingScreen
    }

    private enum  eButtons
    {
        H_Train,
        H_Feed,
        H_Shop,
        S_ExitShop,
        S_PurchaseYes,
        S_PurchaseNo,
        SwipeLeft,
        SwipeRight
    }

    private enum eTextfields
    {
        GoldCount,
        MonsterTypeandStage,
        MonsterValue,        
        MonsterLevel,
        ShopDialogue,
        DungeonDialogue        
    }

    public enum eHomeUIScene
    {
        Home,
        Eggshop,
        Minigame,
        Dungeonlord
    }


    public void Awake()
    {
        GetGameManager();
        GM.homeUI = this;
        camShopPos = new Vector3(0.0f, 20.0f, -10.001f);
        //camDungeonPos = new Vector3(-10.801f, 20.0f, -10.001f);
        EggHatchCount = 1;
        //print("home ui has been started");
    }

    private void Start()
    {
        SetUIStage(eHomeUIScene.Home);
        SetMonsterXPBarUndLevel();
        //SetGoldCounter();
    }

    // Changes UI menus according to scene 
    // Also camera position
    // Later including animations
    public void SetUIStage(eHomeUIScene newScene)
    {
        curScene = newScene;

        switch (curScene)
        {
            case eHomeUIScene.Home:
                UI_MonsterView();
                Camera.main.transform.position = GM.CurCamHomePos;

                break;
            case eHomeUIScene.Eggshop:
                UI_EggShop();
                GM.CurCamHomePos = Camera.main.transform.position;
                Camera.main.transform.position = camShopPos;

                break;
            case eHomeUIScene.Minigame:
                UI_Minigame();

                break;
            case eHomeUIScene.Dungeonlord:
                GM.CurCamHomePos = Camera.main.transform.position;
                //Camera.main.transform.position = camDungeonPos;
                UI_DungeonLord();

                break;
            default:
                break;
        }
    }

    private void UI_MonsterView()
    {
        for(int i = 0; i < 3; i++)
        {
            HomeBGs[i].sprite = BGsprites[i];
        }
        
        DisableMenu((int)eMenus.Dungeon);
        //DisableMenu((int)eMenus.DungeonBG);
        DisableMenu((int)eMenus.D_BottomButtons);
        //DisableMenu((int)eMenus.ShopBG);
        DisableMenu((int)eMenus.Shop);
        DisableMenu((int)eMenus.S_EggMenu);
        DisableMenu((int)eMenus.S_BottomButtons);
        DisableMenu((int)eMenus.MiniGameWindow);

        EnableMenu((int)eMenus.Home);        

        //EnableMenu((int)eMenus.HomeBG);
        //EnableMenu((int)eMenus.H_SwipeButtons);
        EnableMenu((int)eMenus.H_BottomButtons);
        EnableMenu((int)eMenus.SwipeButtons);

        SetMonsterTexts();
        SetMonsterValue();
        SetGoldCounter();
    }

    private void UI_EggShop()
    {
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.MonsterStats);
        DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.SwipeButtons);
        //DisableMenu((int)eMenus.HomeBG);

        //EnableMenu((int)eMenus.ShopBG);
        EnableMenu((int)eMenus.Shop);
        EnableMenu((int)eMenus.S_EggMenu);
        EnableMenu((int)eMenus.S_BottomButtons);
    }

    private void UI_DungeonLord()
    {
        for (int i = 0; i < 3; i++)
        {
            HomeBGs[i].sprite = BGsprites[i+3];
        }

        //TODO: do not show lock/plus symbol in this scene

        //DisableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Home);

        //EnableMenu((int)eMenus.DungeonBG);
        EnableMenu((int)eMenus.Dungeon);
        EnableMenu((int)eMenus.D_BottomButtons);
        EnableMenu((int)eMenus.SwipeButtons);
    }

    private void UI_Minigame()
    {
        DisableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.MiniGameWindow);
    }

// Updating Monster and Player Values 
    public void SetMonsterValue()
    {
        GM.homeMonsterManager.CalculateMonsterValue();

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.homeUI.SetText((int)eTextfields.MonsterValue, "");
        }
        else
        {
            GM.homeUI.SetText((int)eTextfields.MonsterValue, "Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
            //GM.homeUI.SetText((int)eTextfields.MonsterLevel, "" + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            print("Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
        }
    }

    public void SetMonsterTexts()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.homeUI.SetText((int)eTextfields.MonsterTypeandStage, "");
        }
        else
        {
            SetText((int)eTextfields.MonsterTypeandStage, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            SetMonsterValue();
            SetMonsterXPBarUndLevel();
        }
    }

    public void SetMonsterXPBarUndLevel()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null || GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage >= eMonsterStage.Egg)
        {
            DisableMenu((int)eMenus.XPBar);
            GM.homeUI.SetText((int)eTextfields.MonsterLevel, "");
        }
        else
        {
            EnableMenu((int)eMenus.XPBar);
            SetText((int)eTextfields.MonsterLevel, "Lvl " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            SetXPBars();           
        }
    }    

    public void SetGoldCounter()
    {
        SetText((int)eTextfields.GoldCount, "" + GM.PlayerMoney);
    }


    //Dialogue of the Dungeonlord
    public void SetDungeonDialogue()
    {
        string textline = "";

        switch (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage)
        {
            case eMonsterStage.Baby:
                textline = "Eh, canon fodder.";
                break;
            case eMonsterStage.Teen:
                textline = "Moody lil guy.";
                break;
            case eMonsterStage.Adult:
                textline = "Ah, a useful one!";
                break;
            case eMonsterStage.Egg:
                textline = "I'm not buying this!";
                break;
            case eMonsterStage.none:
                textline = "Eh?!";
                break;
            default:
                textline = "What've you got for me?";
                break;
        }
        SetText((int)eTextfields.DungeonDialogue, textline);
    }

    //Button Inputs for menu changes

    public void GoToEggShop()
    {
        SetUIStage(eHomeUIScene.Eggshop);
        SetText((int)eTextfields.ShopDialogue, "What can I do for you?");
    }

    public void GoToMinigames()
    {
        SetUIStage(eHomeUIScene.Minigame);
    }

    public void GoToDungeonlord()
    {
        SetUIStage(eHomeUIScene.Dungeonlord);
        SetDungeonDialogue();
    }

    public void ExitDungeonMenu()
    {
        SetUIStage(eHomeUIScene.Home);
    }

    public void ExitMinigameMenu()
    {
        SetUIStage(eHomeUIScene.Home);
    }

    public void ExitEggShop()
    {
        SetUIStage(eHomeUIScene.Home);
    }

    //Button Inputs EggShop
    public void ChooseEgg(Monster thisMonster)
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster != null && GM.PlayerMoney >= thisMonster.BaseCost)
        {
            SetText((int)eTextfields.ShopDialogue, "There is already a monster in this spot!");
        }
        else if (GM.PlayerMoney < thisMonster.BaseCost)
        {
            SetText((int)eTextfields.ShopDialogue, "It seems you cannot afford this egg!");
        }
        else
        {
            if (GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked)
            {
                curEgg = thisMonster;
                SetText((int)eTextfields.ShopDialogue, "You really wanna buy this egg?");
                // Make Button "selected"
                //DisableMenu(Menus[(int)eMenus.S_EggMenu]);
                DisableMenu((int)eMenus.S_BottomButtons);
                EnableMenu((int)eMenus.S_PurchaseConfirm);
            }
            else
            {
                SetText((int)eTextfields.ShopDialogue, "You haven't unlocked this spot yet!");
            }
        }
    }

    public void ConfirmEggPurchase(bool isTrue)
    {
        if (isTrue)
        {
            //put egg in empty slot position
            GM.CurMonsters[(int)GM.curMonsterSlot].Monster = curEgg;
            SetText((int)eTextfields.MonsterTypeandStage, "Egg");
            GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
            SetMonsterTexts();
            SetMonsterValue();
            GM.homeMonsterManager.SetSlotSymbol();
            DisableMenu((int)eMenus.S_EggMenu);
            DisableMenu((int)eMenus.S_BottomButtons);
            SetText((int)eTextfields.ShopDialogue, "Alright, one egg to go!");
            GM.ChangePlayerGold(-GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseCost);

            StartCoroutine(GM.homeMonsterManager.cSpawnEgg());
            StartCoroutine(cStartEggHatchMinigame());

            //StartCoroutine(ConfirmEggPurchase());
        }
        else
        {
            StartCoroutine(cCancelEggPurchas());
        }

        DisableMenu((int)eMenus.S_PurchaseConfirm);
    }

    public IEnumerator cStartEggHatchMinigame()
    {
        //wait until egg is spawned
        yield return new WaitForSeconds(0.35f);
        SetText((int)eTextfields.ShopDialogue, "Tap egg to hatch it!");
        EnableMenu((int)eMenus.S_TappEggButton);
    }

    public void TapEgg()
    {
        hatchTaps += 1;
        //print("tapped " + hatchTaps);
        if (hatchTaps == EggHatchCount)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel = 1;
            GM.homeMonsterManager.SetEggRarity();
            StartCoroutine(GM.homeMonsterManager.cHatchEgg(GM.homeMonsterManager.EggSpawn));
            DisableMenu((int)eMenus.S_TappEggButton);
        }
    }

    public IEnumerator cCancelEggPurchas()
    {
        EnableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }

    //Button Inputs extra menus in home (Minigame, Pet, Feed?)
    public void ChooseMinigame(int scene)
    {
        GM.curScreen = (eScene)scene;
        SceneManager.LoadScene(scene);
    }

    public void FeedButtonPressed()
    {
        print("Feeding time");
    }


    //Button Inputs Dungeonlord

    public void SellMonster()
    {

    }

    //Testing
    public void TestButton(Monster monster)
    {
        GM.CurMonsters[(int)GM.curMonsterSlot].Monster = monster;
        GM.homeMonsterManager.SetEggRarity();
        GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
        GM.homeMonsterManager.SetSlotSymbol();
        GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage = eMonsterStage.Baby;
        SetMonsterTexts();
        SetMonsterValue();
        SetMonsterXPBarUndLevel();
        GM.homeMonsterManager.SpawnCurrentMonster(GM.homeMonsterManager.MonsterSpawn[(int)GM.curMonsterSlot]);
    }
}
