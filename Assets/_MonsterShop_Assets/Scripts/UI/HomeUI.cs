using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : UIController
{
    [HideInInspector]
    public eHomeUIScene curScene;
    private Monster curEgg;
    private int hatchTaps;
    //private Vector3 camHomePos { get { return GM.CurCamHomePos; } }
    private Vector3 camShopPos;
    //private Vector3 camDungeonPos;
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
        H_MonsterStats,
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
        LoadingScreen,
        LockedButton,
        AddButton,
        PopupInfoWindow,
        UnlockYNButtons,
        ClosePopupButton,
        D_MonsterStats,
        D_SaleConfirm
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
        SwipeRight,
        DungeonSellButton
    }

    private enum eTextfields
    {
        GoldCount,
        H_MonsterTypeandStage,
        H_MonsterValue,        
        H_MonsterLevel,
        ShopDialogue,
        DungeonDialogue,
        PopupInfoText,
        D_MonsterTypeandStage,
        D_MonsterValue,
        D_MonsterLevel
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
        ShowMonsterStats();
        SetMonsterXPBarUndLevel();
        SetSlotSymbol();
        SetMonsterValue();
        //SetGoldCounter();
    }

    public void DisableSwipeButtun(bool IsLeft)
    {
        if (IsLeft)
        {
            DisableButton((int)eButtons.SwipeLeft);
        }
        else
        {
            DisableButton((int)eButtons.SwipeRight);
        }
    }

    public void EnableSwipeButton(bool IsLeft)
    {
        if (IsLeft)
        {
            EnableButton((int)eButtons.SwipeLeft);
        }
        else
        {
            EnableButton((int)eButtons.SwipeRight);
        }
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
        DisableMenu((int)eMenus.D_MonsterStats);
        EnableMenu((int)eMenus.Home);

        //EnableMenu((int)eMenus.HomeBG);
        //EnableMenu((int)eMenus.H_SwipeButtons);
        //EnableMenu((int)eMenus.XPBar);
        EnableMenu((int)eMenus.H_MonsterStats);
        EnableMenu((int)eMenus.H_BottomButtons);
        EnableMenu((int)eMenus.SwipeButtons);

        SetMonsterTexts();
        SetMonsterValue();
        SetGoldCounter();
    }

    private void UI_EggShop()
    {
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.SwipeButtons);
        DisableMenu((int)eMenus.H_MonsterStats);
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

        DisableMenu((int)eMenus.H_MonsterStats);
        EnableMenu((int)eMenus.D_MonsterStats);

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

// sets monster slot symbols if slot is locked or empty
    public void SetSlotSymbol()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null && !GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked)
        {
            EnableMenu((int)eMenus.LockedButton);
            DisableMenu((int)eMenus.AddButton);
        }
        else if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null && GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked)
        {
            EnableMenu((int)eMenus.AddButton);
            DisableMenu((int)eMenus.LockedButton);
        }
        else
        {
            DisableMenu((int)eMenus.LockedButton);
            DisableMenu((int)eMenus.AddButton);
        }
    }

    public void ShowMonsterStats()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster != null)
        {
            EnableMenu((int)eMenus.H_MonsterStats);
        }
        else
        {
            DisableMenu((int)eMenus.H_MonsterStats);
        }
    }

    public void AddMonster()
    {
        GoToEggShop();
    }

    // opens the lock-info-window if player pressed on padlock symbol
    public void PressedPadlockSymbol()
    {
        DisableMenu((int)eMenus.H_BottomButtons);
        EnableMenu((int)eMenus.PopupInfoWindow);
        if (GM.PlayerMoney >= GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice)
        {
            SetText((int)eTextfields.PopupInfoText, "This slot is locked.\nUnlock it for " + GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice + " Money?");
            EnableMenu((int)eMenus.UnlockYNButtons);
        }
        else
        {
            SetText((int)eTextfields.PopupInfoText, "This slot is locked.\nUnlocking costs" + GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice + " Money.");
            EnableMenu((int)eMenus.ClosePopupButton);            
        }
    }

    // Unlocks a slot after player pressed yes
    public void UnlockSlot()
    {
        GM.ChangePlayerGold(-GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice);
        GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked = true;
        SetSlotSymbol();
        CloseUnlockInfo();
    }

    // If player doesnt want or cant unlock the slot
    public void CloseUnlockInfo()
    {
        DisableMenu((int)eMenus.ClosePopupButton);
        DisableMenu((int)eMenus.UnlockYNButtons);
        EnableMenu((int)eMenus.H_BottomButtons);
        DisableMenu((int)eMenus.PopupInfoWindow);
    }

    // Updating Monster and Player Values 
    public void SetMonsterValue()
    {
        GM.homeMonsterManager.CalculateMonsterValue();

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {            
            GM.homeUI.SetText((int)eTextfields.H_MonsterValue, "");
            GM.homeUI.SetText((int)eTextfields.D_MonsterValue, "");
        }
        else
        {
            GM.homeUI.SetText((int)eTextfields.H_MonsterValue, "Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
            GM.homeUI.SetText((int)eTextfields.D_MonsterValue, "Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
            //GM.homeUI.SetText((int)eTextfields.MonsterLevel, "" + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            //print("Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
        }
    }

    public void SetMonsterTexts()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {          
            GM.homeUI.SetText((int)eTextfields.H_MonsterTypeandStage, "");
            GM.homeUI.SetText((int)eTextfields.D_MonsterTypeandStage, "");
        }
        else
        {           
            SetText((int)eTextfields.H_MonsterTypeandStage, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            SetText((int)eTextfields.D_MonsterTypeandStage, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            SetMonsterValue();
            SetMonsterXPBarUndLevel();
        }
    }

    public void SetMonsterXPBarUndLevel()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null || GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage >= eMonsterStage.Egg)
        {
            DisableMenu((int)eMenus.XPBar);
            GM.homeUI.SetText((int)eTextfields.H_MonsterLevel, "");
        }
        else
        {
            EnableMenu((int)eMenus.XPBar);
            SetText((int)eTextfields.H_MonsterLevel, "Lvl " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            SetXPBars();           
        }
    }    

    public void SetMonsterlevel_Dungeon()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.homeUI.SetText((int)eTextfields.D_MonsterLevel, "");
        }
        else
        {
            SetText((int)eTextfields.D_MonsterLevel, "Lvl " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
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
                textline = "What've you got for me?";
                break;
            default:
                textline = "Eh?!";
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
      
        GM.homeUI.SetMonsterTexts();
        GM.homeUI.SetMonsterValue();
        GM.homeUI.SetMonsterlevel_Dungeon();

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.homeUI.SellButtonActive(false);
        }
        else
        {
            GM.homeUI.SellButtonActive(true);
        }
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
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage = eMonsterStage.Egg;
            SetText((int)eTextfields.H_MonsterTypeandStage, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            SetText((int)eTextfields.D_MonsterTypeandStage, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
            SetMonsterTexts();
            SetMonsterValue();
            GM.homeUI.SetSlotSymbol();
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

    public void TrainButtonActive(bool isActive)
    {
        if (isActive)
            EnableButton((int)eButtons.H_Train);
        else
            DisableButton((int)eButtons.H_Train);
    }

    public void TapEgg()
    {
        hatchTaps += 1;

        if (hatchTaps == EggHatchCount)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel = 1;
            GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator;
            GM.homeMonsterManager.SetEggRarity();
            StartCoroutine(GM.homeMonsterManager.cHatchEgg(GM.homeMonsterManager.EggSpawn));
            DisableMenu((int)eMenus.S_TappEggButton);
            GM.homeUI.SetMonsterTexts();
            GM.homeUI.SetMonsterValue();
            GM.homeUI.SetMonsterXPBarUndLevel();
            GM.homeUI.SetSlotSymbol();
            hatchTaps = 0;
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

    public void SellButtonActive(bool isActive)
    {
        if (isActive)
        {
            EnableButton((int)eButtons.DungeonSellButton);
        }
        else
        {
            DisableButton((int)eButtons.DungeonSellButton);
        }
    }

    //Button Inputs Dungeonlord
    
   /// <summary>
   /// Player pressed Sell monster Button in Dungeon screen
   /// </summary>
    public void SellMonsterButton()
    {
        //disable swipe buttons
        DisableButton((int)eButtons.SwipeLeft);
        DisableButton((int)eButtons.SwipeRight);
        DisableMenu((int)eMenus.D_BottomButtons);
        //enable y/n menu        
        EnableMenu((int)eMenus.D_SaleConfirm);
    }

    /// <summary>
    /// Player confirms/cancels sale of monster in dungeon screen
    /// </summary>
    public void ConfirmMonsterSale(bool yes)
    {
        if (yes)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].Sold = true;
            StartCoroutine(cMonsterCage());
        }
        else
        {
            //enable swipe buttons
            EnableButton((int)eButtons.SwipeLeft);
            EnableButton((int)eButtons.SwipeRight);
            EnableMenu((int)eMenus.D_BottomButtons);
            //close dis y/n shit
            DisableMenu((int)eMenus.D_SaleConfirm);
        }
    }
    
    /// <summary>
    /// Cage is put over monsters to show they are sold
    /// </summary>
    private IEnumerator cMonsterCage()
    {
        SetText((int)eTextfields.DungeonDialogue, "Very busy progger hasn't implemented that feature yet");
        yield return new WaitForSeconds(0.5f);
        DisableMenu((int)eMenus.D_SaleConfirm);
        EnableButton((int)eButtons.SwipeLeft);
        EnableButton((int)eButtons.SwipeRight);
        EnableMenu((int)eMenus.D_BottomButtons);
    }

    /// <summary>
    /// This is for testing bullshit; spawns a monster atm
    /// </summary>
    /// <param name="monster"></param>
    public void TestButton(Monster monster)
    {
        GM.CurMonsters[(int)GM.curMonsterSlot].Monster = monster;
        GM.homeMonsterManager.SetEggRarity();
        GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
        GM.homeUI.SetSlotSymbol();
        GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage = eMonsterStage.Baby;
        SetMonsterTexts();
        SetMonsterValue();
        SetMonsterXPBarUndLevel();
        GM.homeMonsterManager.SpawnCurrentMonster(GM.homeMonsterManager.MonsterSpawn[(int)GM.curMonsterSlot]);
    }
}
