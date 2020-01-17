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

    private bool swipeDisabled;

    // called via (int)GM.curMonsterSlot
    public SpriteRenderer[] HomeBGs;

    public Sprite[] BGsprites;

    public Animator[] CageTop;
    public GameObject[] RarityStars = new GameObject[3];
    public GameObject AllCages;
    public Animator D_Signature;
    public CameraMovement camMovement;
    public Animator PlayerGold;

    private int totalValue;
    private bool signed = false;

    private enum BGsprite
    {
        homeLeft,
        homeMiddle,
        homeRight,
        dungeonLeft,
        dungeonMiddle,
        dungeonRight
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
        S_ShopUI,
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
        D_SaleConfirm,
        SwipeLeftInput,
        SwipeRightInput,
        D_SaleConfirmButton,
        D_LeaveConfirmButton,
        D_SalesContract,
        Kompendium,
        Petting,
        PetMeSymbol,
        PettingInfo,
        PettingXPBar,
        DungeonSellButton
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
        DungeonSellButton,
        K_PaageLeft,
        K_PageRight
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
        D_MonsterLevel,
        D_SoldMonster1,
        D_SoldMonster2,
        D_SoldMonster3,
        K_MonsterDetail,
        K_MonsterFluff
    }

    public enum eHomeUIScene
    {
        Home,
        Eggshop,
        Minigame,
        Dungeonlord,
        Kompendium,
        Petting,
        none
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
        ShowMonsterStats(GM.CurMonsters[(int)GM.curMonsterSlot].Monster != null);
        SetMonsterXPBarUndLevel();
        SetSlotSymbol();
        SetMonsterValue();
        DisableSwiping(false);
        //SetGoldCounter();
    }

    public void SetPettingSymbol(bool show)
    {
        if (show)
        {
            //show symbol
            EnableMenu((int)eMenus.PetMeSymbol);
            //TODO change to sad idle
        }
        else
        {
            //hide symbol
            DisableMenu((int)eMenus.PetMeSymbol);
        }
    }

    /// <summary>
    /// Called by CameraMovement: Controls enable/disable status of swipe buttons and controls
    /// </summary>
    public void SetSwipeButtonStatus()
    {
        if (GM.curMonsterSlot == ecurMonsterSlot.left)
        {
            DisableButton((int)eButtons.SwipeLeft);
            DisableMenu((int)eMenus.SwipeLeftInput);
        }
        else if (GM.curMonsterSlot == ecurMonsterSlot.right)
        {
            DisableButton((int)eButtons.SwipeRight);
            DisableMenu((int)eMenus.SwipeRightInput);
        }
        else
        {
            EnableButton((int)eButtons.SwipeLeft);
            EnableMenu((int)eMenus.SwipeLeftInput);

            EnableButton((int)eButtons.SwipeRight);
            EnableMenu((int)eMenus.SwipeRightInput);
        }
    }

    /// <summary>
    /// Disables/Enables all swiping controls and buttons using bool swipeEnabled
    /// </summary>
    public void DisableSwiping(bool disable)
    {
        swipeDisabled = disable;

        if (swipeDisabled)
        {
            DisableButton((int)eButtons.SwipeRight);
            DisableMenu((int)eMenus.SwipeRightInput);
            DisableButton((int)eButtons.SwipeLeft);
            DisableMenu((int)eMenus.SwipeLeftInput);
        }
        else
        {
            EnableButton((int)eButtons.SwipeRight);
            EnableMenu((int)eMenus.SwipeRightInput);
            EnableButton((int)eButtons.SwipeLeft);
            EnableMenu((int)eMenus.SwipeLeftInput);
        }
    }

    // Changes UI menus according to scene 
    // Also camera position
    // Later including animations
    //TODO transfer to game manager when home ui screens are split up
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

            case eHomeUIScene.Kompendium:
                UI_Kompendium();

                break;
            case eHomeUIScene.Petting:
                UI_Petting();

                break;
            case eHomeUIScene.none:
                NoUI();

                break;
            default:
                Debug.LogError("No UI scene set!");
                break;
        }
    }

    private void NoUI()
    {
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.Petting);
        DisableMenu((int)eMenus.Kompendium);
        DisableMenu((int)eMenus.Dungeon);
        DisableMenu((int)eMenus.Shop);
        DisableMenu((int)eMenus.S_EggMenu);
        DisableMenu((int)eMenus.S_BottomButtons);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.D_MonsterStats);
        DisableMenu((int)eMenus.D_BottomButtons);
        DisableMenu((int)eMenus.MiniGameWindow);
        DisableMenu((int)eMenus.PlayerInfo);
        DisableMenu((int)eMenus.SwipeButtons);
        DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.SwipeButtons);
    }

    private void UI_Petting()
    {
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.SwipeButtons);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.PlayerInfo);
        DisableSwiping(true);

        EnableMenu((int)eMenus.Petting);
        EnableMenu((int)eMenus.PettingInfo);
        DisableMenu((int)eMenus.PettingXPBar);
    }

    private void UI_Kompendium()
    {
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.SwipeButtons);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.PlayerInfo);
        DisableSwiping(true);

        EnableMenu((int)eMenus.Kompendium);
    }

    private void UI_MonsterView()
    {
        for(int i = 0; i < 3; i++)
        {
            HomeBGs[i].sprite = BGsprites[i];
        }

        //scale monsters
        GM.homeMonsterManager.ScaleMonsterBody(0.20f);

        SetCageVisibility(false);
        EnableMenu((int)eMenus.H_MonsterStats);
        EnableMenu((int)eMenus.H_BottomButtons);
        EnableMenu((int)eMenus.SwipeButtons);
        EnableMenu((int)eMenus.PlayerInfo);
        EnableMenu((int)eMenus.Home);

        DisableMenu((int)eMenus.Petting);
        DisableMenu((int)eMenus.Kompendium);
        DisableMenu((int)eMenus.Dungeon);
        //DisableMenu((int)eMenus.DungeonBG);
        DisableMenu((int)eMenus.D_BottomButtons);
        //DisableMenu((int)eMenus.ShopBG);
        DisableMenu((int)eMenus.Shop);
        DisableMenu((int)eMenus.S_EggMenu);
        DisableMenu((int)eMenus.S_BottomButtons);
        DisableMenu((int)eMenus.MiniGameWindow);
        DisableMenu((int)eMenus.D_MonsterStats);

        //EnableMenu((int)eMenus.HomeBG);
        //EnableMenu((int)eMenus.H_SwipeButtons);
        //EnableMenu((int)eMenus.XPBar);
        DisableSwiping(false);

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
        EnableMenu((int)eMenus.S_ShopUI);
        EnableMenu((int)eMenus.S_EggMenu);
        EnableMenu((int)eMenus.S_BottomButtons);
    }

    private void UI_DungeonLord()
    {
        for (int i = 0; i < 3; i++)
        {
            HomeBGs[i].sprite = BGsprites[i+3];
        }

        SetCageVisibility(true);
        //scale monsters
        GM.homeMonsterManager.ScaleMonsterBody(0.150f);
        
        //DisableMenu((int)eMenus.HomeBG);
        //DisableMenu((int)eMenus.XPBar);
        DisableMenu((int)eMenus.Home);
        DisableMenu((int)eMenus.H_MonsterStats);
        DisableMenu((int)eMenus.D_SalesContract);        
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

    public void ShowPetSessionResult()
    { 
        DisableMenu((int)eMenus.PettingInfo);
        EnableMenu((int)eMenus.PettingXPBar);
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

    public void ShowMonsterStats(bool show)
    {
        SetMonsterTexts();
        SetMonsterValue();
        SetMonsterXPBarUndLevel();
        SetMonsterRarityStars();

        if (show)
        {
            EnableMenu((int)eMenus.H_MonsterStats);
            if (!GM.CurMonsters[(int)GM.curMonsterSlot].IsHappy)
                EnableMenu((int)eMenus.PetMeSymbol);
            else
                DisableMenu((int)eMenus.PetMeSymbol);
        }
        else
        {
            DisableMenu((int)eMenus.H_MonsterStats);
            DisableMenu((int)eMenus.PetMeSymbol);
        }
    }

    public void AddMonster()
    {
        GoToEggShop();
    }
    public void SetPopInfoWindowStatus(bool open, string message = "")
    {
        if (open)
        {
            EnableMenu((int)eMenus.PopupInfoWindow);
            SetText((int)eTextfields.PopupInfoText, message);
        }
        else
        {
            DisableMenu((int)eMenus.PopupInfoWindow);
        }
    }

    // opens the lock-info-window if player pressed on padlock symbol
    public void PressedPadlockSymbol()
    {
        DisableSwiping(true);
        DisableMenu((int)eMenus.H_BottomButtons);
        //EnableMenu((int)eMenus.PopupInfoWindow);
        if (GM.PlayerMoney >= GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice)
        {
            SetPopInfoWindowStatus(true, 
                "This slot is locked." +
                "\nUnlock it for " + GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice + " Money?");
            EnableMenu((int)eMenus.UnlockYNButtons);
        }
        else
        {
            SetPopInfoWindowStatus(true, 
                "This slot is locked." +
                "\nUnlocking costs" + GM.CurMonsters[(int)GM.curMonsterSlot].UnlockPrice + " Money.");
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
        DisableSwiping(false);
    }

    // Updating Monster and Player Values 
    public void SetMonsterValue()
    {     
        int curValue = GM.homeMonsterManager.CalculateMonsterValue(); 

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {            
            GM.homeUI.SetText((int)eTextfields.H_MonsterValue, "");
            GM.homeUI.SetText((int)eTextfields.D_MonsterValue, "");
        }
        else if (GM.CurMonsters[(int)GM.curMonsterSlot].Sold)
        {
            GM.homeUI.SetText((int)eTextfields.D_MonsterValue, "Sold for " + curValue);
        }
        else
        {
            GM.homeUI.SetText((int)eTextfields.H_MonsterValue, "Value: " + curValue);
            GM.homeUI.SetText((int)eTextfields.D_MonsterValue, "Value: " + curValue);
            //GM.homeUI.SetText((int)eTextfields.MonsterLevel, "" + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            //print("Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
        }
    }

    public void SetMonsterTexts()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {          
            GM.homeUI.SetText((int)eTextfields.H_MonsterTypeandStage, "");
            GM.homeUI.SetText((int)eTextfields.D_MonsterTypeandStage, "No monster \navailable");
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
        else if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster != null && GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage != eMonsterStage.Adult)
        {
            EnableMenu((int)eMenus.XPBar);
            SetText((int)eTextfields.H_MonsterLevel, "Lvl " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
            SetXPBars();           
        }
        else
        {
            DisableMenu((int)eMenus.XPBar);
            SetText((int)eTextfields.H_MonsterLevel, "Lvl " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel);
        }
    }    

    public void SetMonsterRarityStars()
    {
        switch (GM.CurMonsters[(int)GM.curMonsterSlot].Rarity)
        {
            case eRarity.normal:
                RarityStars[0].SetActive(true);
                RarityStars[1].SetActive(false);
                RarityStars[2].SetActive(false);
                break;
            case eRarity.rare:
                RarityStars[0].SetActive(true);
                RarityStars[1].SetActive(true);
                RarityStars[2].SetActive(false);

                break;
            case eRarity.legendary:
                RarityStars[0].SetActive(true);
                RarityStars[1].SetActive(true);
                RarityStars[2].SetActive(true);

                break;
            default:
                RarityStars[0].SetActive(false);
                RarityStars[1].SetActive(false);
                RarityStars[2].SetActive(false);

                break;
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


    /// <summary>
    /// Sets the dungeonlords dialogue depending on the offered monsters stage
    /// </summary>
    public void SetDungeonDialogue()
    {
        string textline;

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Sold)
        {
            textline =
            "Can't wait to put this one to work.";
        }
        else
        {
            switch (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage)
            {
                case eMonsterStage.Baby:
                    textline =
                        "Eh, canon fodder.";
                    break;
                case eMonsterStage.Teen:
                    textline =
                        "Moody lil guy.";
                    break;
                case eMonsterStage.Adult:
                    textline =
                        "Ah, a useful one!";
                    break;
                case eMonsterStage.Egg:
                    textline =
                        "I'm not buying this!";
                    break;
                case eMonsterStage.none:
                    textline =
                        "Got anything for me?";
                    break;
                default:
                    textline =
                        "Eh?!";
                    break;
            }
        }
        SetText((int)eTextfields.DungeonDialogue, textline);
    }

    //Button Inputs for menu changes

    public void GoToKompendium()
    {
        GM.monsterKompendium.SetButtons();
        GM.monsterKompendium.ShowEntry(0);
        SetUIStage(eHomeUIScene.Kompendium);
        //TODO set info to be the fist monster in list
    }

    public void GoToPetSession()
    {
        StartCoroutine(cWaitForZoom(false));
    }

    public void ExitPetSession()
    {
        StartCoroutine(cWaitForZoom(true));
    }

    /// <summary>
    /// Waiting for the zoom from Petting session
    /// </summary>
    /// <param name="zoomedIn"></param>
    /// <returns></returns>
    private IEnumerator cWaitForZoom(bool zoomedIn)
    {
        SetUIStage(eHomeUIScene.none);            

        yield return new WaitForSeconds(0.15f);
        if (zoomedIn)
        {
            camMovement.Zoom(false);
            SetUIStage(eHomeUIScene.Home);
        }
        else
        {
            SetUIStage(eHomeUIScene.Petting);
        }
    }

    public void GoToEggShop()
    {
        SetUIStage(eHomeUIScene.Eggshop);
        SetText((int)eTextfields.ShopDialogue, 
            "Click the egg you want to buy!");
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

    public void ExitKompendium()
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
            SetText((int)eTextfields.ShopDialogue, 
                "There is already a monster in this spot!");
        }
        else if (GM.PlayerMoney < thisMonster.BaseCost)
        {
            SetText((int)eTextfields.ShopDialogue, 
                "It seems you cannot afford this egg!");
        }
        else
        {
            if (GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked)
            {
                curEgg = thisMonster;
                SetText((int)eTextfields.ShopDialogue, 
                    "You really wanna buy this egg?");
                // Make Button "selected"
                //DisableMenu(Menus[(int)eMenus.S_EggMenu]);
                DisableMenu((int)eMenus.S_BottomButtons);
                EnableMenu((int)eMenus.S_PurchaseConfirm);
            }
            else
            {
                SetText((int)eTextfields.ShopDialogue, 
                    "You haven't unlocked this spot yet!");
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
                       
            SetText((int)eTextfields.H_MonsterTypeandStage, 
                GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + 
                GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);
            SetText((int)eTextfields.D_MonsterTypeandStage, 
                GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + " " + 
                GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName);

            GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
            GM.CurMonsters[(int)GM.curMonsterSlot].XPCap = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.XP_Cap;
            SetMonsterTexts();
            SetMonsterValue();
            GM.homeUI.SetSlotSymbol();
            DisableMenu((int)eMenus.S_EggMenu);
            DisableMenu((int)eMenus.S_BottomButtons);
            SetText((int)eTextfields.ShopDialogue, 
                "Alright, one egg to go!");
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
        //SetText((int)eTextfields.ShopDialogue, 
        //    "Tap egg to hatch it!");
        DisableMenu((int)eMenus.S_ShopUI);
        DisableMenu((int)eMenus.S_EggMenu);
        DisableMenu((int)eMenus.S_BottomButtons);
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
        //tapeffect

        if (hatchTaps == EggHatchCount)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel = 1;            
            GM.homeMonsterManager.SetEggRarity();
            StartCoroutine(GM.homeMonsterManager.cHatchEgg(GM.homeMonsterManager.EggSpawn));
            DisableMenu((int)eMenus.S_TappEggButton);
            GM.monsterKompendium.MonsterEntry[CurKompendiumEntrySlot()].MonsterHatchCount += 1;
            SetMonsterTexts();
            SetMonsterValue();
            SetMonsterXPBarUndLevel();
            SetSlotSymbol();
            hatchTaps = 0;
        }
    }

    public IEnumerator cCancelEggPurchas()
    {
        EnableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, 
            "Can I get you another one?");
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

    /// <summary>
    /// Set Sell button active if there is a monster to sell in the current slot
    /// </summary>
    /// <param name="isActive"></param>
    public void SellButtonActive(bool isActive)
    {
        if (isActive)
        {
            EnableMenu((int)eMenus.DungeonSellButton);
        }
        else
        {
            DisableMenu((int)eMenus.DungeonSellButton);
        }
    }

    //Button Inputs Dungeonlord    

   /// <summary>
   /// Player pressed Sell monster Button in Dungeon screen
   /// </summary>
    public void SellMonsterButton()
    {
        //disable swipe buttons
        DisableSwiping(true);
        DisableMenu((int)eMenus.SwipeButtons);
        DisableMenu((int)eMenus.D_BottomButtons);
        //enable y/n menu        
        SetPopInfoWindowStatus(true, 
            "Are you sure you want to sell this monster?");
        EnableMenu((int)eMenus.D_SaleConfirm);
        SellButtonActive(false);
    }

    /// <summary>
    /// If an adult monster is sold, the entry in the kompendium is unlocked/updated
    /// </summary>
    private void WriteValuesToKompendium()
    {
        int entrySlot = CurKompendiumEntrySlot();
        GM.UnlockedLogEntries[entrySlot] = true;
        GM.monsterKompendium.MonsterEntry[entrySlot].MonsterHighestPrice =
            "" + Mathf.RoundToInt(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
    }

    public int CurKompendiumEntrySlot()
    {
        int entrySlot = (int)GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterType;

        switch (GM.CurMonsters[(int)GM.curMonsterSlot].Rarity)
        {
            case eRarity.normal:

                break;
            case eRarity.rare:
                entrySlot += 1;
                break;
            case eRarity.legendary:
                entrySlot += 2;
                break;
            default:
                break;
        }

        return entrySlot;
    }

    /// <summary>
    /// Player confirms/cancels sale of monster in dungeon screen
    /// </summary>
    public void ConfirmMonsterSale(bool yes)
    {
        if (yes)
        {
            GM.DungeonlordWaiting = false;
            GM.CurMonsters[(int)GM.curMonsterSlot].Sold = true;
            if (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage == eMonsterStage.Adult)
            {
                WriteValuesToKompendium();
            }
            SetText((int)eTextfields.DungeonDialogue, "Sign and it's mine!");
            ShowContract();
        }
        else        
        {
            //enable swipe buttons
            DisableSwiping(false);
            EnableMenu((int)eMenus.SwipeButtons);
            EnableMenu((int)eMenus.D_BottomButtons);
            EnableMenu((int)eMenus.DungeonSellButton);
            //close dis y/n shit
        }

        DisableMenu((int)eMenus.D_SaleConfirm);
        SetPopInfoWindowStatus(false);
    }
    
    /// <summary>
    /// Cage is put over monsters to show they are sold
    /// </summary>
    private IEnumerator cMonsterCage()
    {
        SellButtonActive(false);
        SetText((int)eTextfields.DungeonDialogue, "Can't wait to put this one to work!");

        SetText((int)eTextfields.D_MonsterValue,
            "Sold for " + Mathf.RoundToInt(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue));

        DropCage(GM.CurMonsters[(int)GM.curMonsterSlot].SlotID);
        yield return new WaitForSeconds(0.5f);
        PlayerGold.SetTrigger("gain");
        yield return new WaitForSeconds(0.2f);
        GM.ChangePlayerGold(+totalValue);
        yield return new WaitForSeconds(0.5f);
        DisableMenu((int)eMenus.D_SaleConfirm);
        EnableMenu((int)eMenus.D_BottomButtons);
        EnableMenu((int)eMenus.SwipeButtons);
        DisableSwiping(false);        
    }

    private void ShowContract()
    {        
        string msg = GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage +" "+ GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MonsterName + "\n"
            + "Selling Price: " + Mathf.RoundToInt(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);

        totalValue = Mathf.RoundToInt(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterValue);
        SetText((int)eTextfields.D_SoldMonster1, msg);
        EnableMenu((int)eMenus.D_SalesContract);
    }

    private void SetCageVisibility(bool isVisible)
    {
        AllCages.SetActive(isVisible);        
    }

    private void DropCage(int slotID)
    {
        CageTop[slotID].SetTrigger("falling");
        CageTop[slotID].SetBool("down", true);
    }

    /// <summary>
    /// Player pressed Go Home button in Dungeonlord menu
    /// </summary>
    public void ExitDungeonMenu()
    {
        // nothing has been sold to the dungeon lord
        if (GM.DungeonlordWaiting)
        {
            SetUIStage(eHomeUIScene.Home);
        }
        else
        {
            // Y/N popup
            //string msg = 
            //    "If you leave now, you have to wait x hours before the dungeonlord needs new monsters." +
            //    "\nReally leave and sell?";
            //SetPopInfoWindowStatus(true, msg);
            //DisableMenu((int)eMenus.D_BottomButtons);
            //EnableMenu((int)eMenus.D_LeaveConfirmButton);

            SellMonsterAndGoHome(true);
            //TODO remove this when dungeon lord timer is implemented!!
            GM.DungeonlordWaiting = true;
            StartCoroutine(cExitDungeon());
        }
    }

    private IEnumerator cExitDungeon()
    {
        SetText((int)eTextfields.DungeonDialogue, "You can come back in 8 hours");
        yield return new WaitForSeconds(2f);
        foreach (MonsterSlot slot in GM.CurMonsters)
        {
            if (slot.Sold)
            {
                GM.homeMonsterManager.DeleteMonsterBody(slot.SlotID);
                slot.ResetValues();
            }
        }
        SetSlotSymbol();
        SetUIStage(eHomeUIScene.Home);
    }

    /// <summary>
    /// Player pressed a button of the Y/N choice when leaving the dungeonlords menu after marking monsters as sold
    /// </summary>
    /// <param name="goHome"></param>
    public void SellMonsterAndGoHome(bool goHome)
    {
        //if (goHome)
        //{
        //    DisableSwiping(true);
        //    DisableMenu((int)eMenus.SwipeButtons);

        //    string msg = "";

        //    foreach (MonsterSlot slot in GM.CurMonsters)
        //    {
        //        if (slot.Sold)
        //        {
        //            msg += slot.Monster.MonsterName + "\n" +
        //                "Selling Price" + Mathf.RoundToInt(slot.MonsterValue) + "\n";

        //            totalValue += Mathf.RoundToInt(slot.MonsterValue);
        //            GM.homeMonsterManager.DeleteMonsterBody(slot.SlotID);
        //            slot.ResetValues();
        //        }
        //    }
        //    SetText((int)eTextfields.D_SoldMonster1, msg);
        //    EnableMenu((int)eMenus.D_SalesContract);          

        //}
        //else
        //{
        //    EnableMenu((int)eMenus.D_BottomButtons);
        //}
        //DisableMenu((int)eMenus.D_LeaveConfirmButton);
        //SetPopInfoWindowStatus(false);
    }

    /// <summary>
    /// Player taps the contract to "sign"
    /// </summary>
    public void SignContract()
    {
        if (!signed)
        {
            StartCoroutine(cSignMonsterSaleContract());
        }
    }

    /// <summary>
    /// Called by SignContract()
    /// </summary>
    /// <returns></returns>
    public IEnumerator cSignMonsterSaleContract()
    {
        signed = true;
        //TODO Dungeon Lord sale: animated signature

        D_Signature.SetTrigger("sign");
        
        yield return new WaitForSeconds(1f);
        //totalValue = 0;
        //DisableMenu((int)eMenus.D_SalesContract);
        //D_Signature.color = new Color(D_Signature.color.r, D_Signature.color.g, D_Signature.color.b, 0.0f);
        signed = false;
        DisableMenu((int)eMenus.D_SalesContract);
        StartCoroutine(cMonsterCage());
    }

    public void WatchAdButton()
    {
        print("watch an ad and get money");
    }

    /// <summary>
    /// This is for testing bullshit; spawns a monster atm
    /// </summary>
    /// <param name="monster"></param>
    public void TestButton(Monster monster)
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked && GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].Monster = monster;
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel = 1;
            GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[(int)GM.CurMonsters[(int)GM.curMonsterSlot].Rarity];
            GM.homeMonsterManager.SetEggRarity();
            GM.CurMonsters[(int)GM.curMonsterSlot].BaseValue = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.BaseValue;
            GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage = eMonsterStage.Baby;
            SetMonsterTexts();
            SetMonsterValue();
            SetMonsterXPBarUndLevel();
            SetSlotSymbol();
            GM.homeMonsterManager.SpawnCurrentMonster(GM.homeMonsterManager.MonsterSpawn[(int)GM.curMonsterSlot]);
            TrainButtonActive(true);
        }
    }
    public void AnotherCheat()
    {
        GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage = eMonsterStage.Adult;
        GM.homeMonsterManager.DeleteMonsterBody(GM.CurMonsters[(int)GM.curMonsterSlot].SlotID);
        GM.homeMonsterManager.SpawnCurrentMonster(GM.homeMonsterManager.MonsterSpawn[(int)GM.curMonsterSlot]);
    }
}
