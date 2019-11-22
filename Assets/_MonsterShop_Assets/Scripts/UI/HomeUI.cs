using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : UIController
{
    [SerializeField]
    private Monster curEgg;
    private int hatchTaps;
    private Vector3 camShopPos = new Vector3(0.0f,20.0f, -10.001f);
    private Vector3 camDungeonPos = new Vector3(-10.801f,20.0f, -10.001f);
    private Vector3 camHomePos;

    private enum eMenus
    {
        Home,
        Shop,
        H_SwipeButtons,
        H_BottomButtons,
        S_BottomButtons,
        S_PurchaseConfirm,
        S_EggMenu,
        HomeBG,
        ShopBG,
        S_TappEggButton,
        PlayerInfo,
        MonsterStats,
        MiniGames,
        S_RarityText,
        DungeonBG,
        Dungeon,
        D_BottomButtons,
    }

    private enum  eButtons
    {
        H_Train,
        H_Feed,
        H_Shop,
        H_Left,
        H_Right,
        S_ExitShop,
        S_PurchaseYes,
        S_PurchaseNo
    }

    private enum eTextfields
    {
        ShopDialogue,
        GoldCount,
        MonsterTypeandStage,
        MonsterValue,
        Hatch_Rarity,
        DungeonDialogue,
    }

    private enum eScene
    {
        Home,
        Eggshop,
        Minigame,
        Dungeonlord
    }

    private void Awake()
    {
        GetGameManager();
        GM.homeUI = this;
    }
    
    public void SetMonsterValue()
    {
        GM.homeUI.SetText((int)eTextfields.MonsterValue, "Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].CreatureValue);
        print("Value: " + GM.CurMonsters[(int)GM.curMonsterSlot].CreatureValue);
    }

    public void DungeonButtonPressed()
    {
        camHomePos = Camera.main.transform.position;
        Camera.main.transform.position = camDungeonPos;
        DisableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.DungeonBG);
        EnableMenu((int)eMenus.Dungeon);
        //Show creature for sale
        EnableMenu((int)eMenus.D_BottomButtons);
        SetDungeonDialogue();
    }

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

    public void SellMonster()
    {

    }

    public void ExitDungeonMenu()
    {
        Camera.main.transform.position = camHomePos;
        EnableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Dungeon);
        DisableMenu((int)eMenus.DungeonBG);
    }

    public void ShopButtonPressed()
    {
        camHomePos = Camera.main.transform.position;
        Camera.main.transform.position = camShopPos;
        DisableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.ShopBG);
        EnableMenu((int)eMenus.Shop);
        EnableMenu((int)eMenus.S_EggMenu);
        EnableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, "What can I do for you?");
    }

    public void TrainButtonPressed()
    {
        DisableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.MiniGames);
    }

    public void ChooseMinigame(int scene)
    {
        GM.SaveCurrentMonsters();
        SceneManager.LoadScene(scene);
    }

    public void ExitTrainMenu()
    {
        DisableMenu((int)eMenus.MiniGames);
        EnableMenu((int)eMenus.Home);
    }

    public void FeedButtonPressed()
    {
        StartCoroutine(GM.CurMonsters[(int)GM.curMonsterSlot].C_SetStage(eMonsterStage.Teen,GM.CurMonsters[(int)GM.curMonsterSlot].MonsterSpawn));
        print("Feeding time");
    }

    public void ExitShopButtonPressed()
    {
        Camera.main.transform.position = camHomePos;
        EnableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Shop);
        DisableMenu((int)eMenus.ShopBG);
    }

    public void ChooseEgg(Monster EggPrefab)
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster != null && GM.PlayerMoney >= EggPrefab.BaseCost)
        {
            SetText((int)eTextfields.ShopDialogue, "There is already a monster in this spot!");
        }
        else if (GM.PlayerMoney < EggPrefab.BaseCost)
        {
            SetText((int)eTextfields.ShopDialogue, "It seems you cannot afford this egg!");
        }
        else
        {
            if (GM.CurMonsters[(int)GM.curMonsterSlot].Unlocked)
            {
                curEgg = EggPrefab;
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
            GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster = curEgg;
            StartCoroutine(ConfirmEggPurchase());
        }
        else
        {
            StartCoroutine(CancelEggPurchas());
        }

        DisableMenu((int)eMenus.S_PurchaseConfirm);
    }

    public IEnumerator ConfirmEggPurchase()
    {
        SetText((int)eTextfields.ShopDialogue, "Alright, one egg to go!");
        GM.SetGold(-GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster.BaseCost);
        GM.CurMonsters[(int)GM.curMonsterSlot].SetSymbol();
        //GM.HomeCam.SetScreen((ecurMonsterSlot)freeSlot);
        yield return new WaitForSeconds(0.3f);
        DisableMenu((int)eMenus.S_EggMenu);
        StartCoroutine(GM.CurMonsters[(int)GM.curMonsterSlot].C_SetStage(eMonsterStage.Egg, GM.CurMonsters[(int)GM.curMonsterSlot].EggSpawn));
        DisableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, "Tap egg to hatch it!");
        EnableMenu((int)eMenus.S_TappEggButton);
        //EnableMenu(Menus[(int)eMenus.Home]);
    }

    public IEnumerator CancelEggPurchas()
    {
        //make button deselected
        //EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        EnableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }

    public void TapEgg()
    {
        hatchTaps += 1;
        //print("tapped " + hatchTaps);
        if (hatchTaps == 5)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)Random.Range(0, 3);

            switch (GM.CurMonsters[(int)GM.curMonsterSlot].Rarity)
            {
                case eRarity.normal:
                    GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster.LevelThreshold_normal;
                    SetText((int)eTextfields.Hatch_Rarity, "Normal");
                    break;
                case eRarity.rare:
                    GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster.LevelThreshold_rare;
                    SetText((int)eTextfields.Hatch_Rarity, "Rare!");

                    break;
                case eRarity.legendary:
                    GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].CurMonster.LevelThreshold_legendary;
                    SetText((int)eTextfields.Hatch_Rarity, "Legendary!");

                    break;
                default:
                    break;
            }

            print(GM.CurMonsters[(int)GM.curMonsterSlot].Rarity);
            StartCoroutine(WaitForEggToHatch());
            DisableMenu((int)eMenus.S_TappEggButton);
        }
    }

    public IEnumerator WaitForEggToHatch()
    {
        DisableMenu((int)eMenus.ShopBG);
        DisableMenu((int)eMenus.Shop);
        EnableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.HomeBG);
        StartCoroutine(GM.CurMonsters[(int)GM.curMonsterSlot].C_SetStage(eMonsterStage.Baby, GM.CurMonsters[(int)GM.curMonsterSlot].MonsterSpawn));
        yield return new WaitForSeconds(0.15f);
        Camera.main.transform.position = camHomePos;
        EnableMenu((int)eMenus.S_RarityText);
        SetMonsterTexts((int)GM.curMonsterSlot);
        yield return new WaitForSeconds(1f);
        DisableMenu((int)eMenus.S_RarityText);
    }

    public void SetMonsterTexts(int thisSlot)
    {
        if (GM.CurMonsters[thisSlot].CurMonster == null)
        {
            SetText((int)eTextfields.MonsterTypeandStage, "");
            SetMonsterValue();
        }
        else
        {
            SetText((int)eTextfields.MonsterTypeandStage, GM.CurMonsters[thisSlot].MonsterStage + " " + GM.CurMonsters[thisSlot].CurMonster.CreatureName);
            SetMonsterValue(); 
        }
    }

    public void SetGoldCounter()
    {
        SetText((int)eTextfields.GoldCount, "" + GM.PlayerMoney);
    }
}
