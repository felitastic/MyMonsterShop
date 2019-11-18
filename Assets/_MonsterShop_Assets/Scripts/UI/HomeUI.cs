using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIController
{
    [SerializeField]
    private Monster curEgg;
    private int freeSlot;
    private int hatchTaps;

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
        PlayerInfo
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
        GoldCount
    }

    private void Start()
    {
        SetUIinManager();
    }

    public void ShopButtonPressed()
    {
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
        print("Training");
    }

    public void FeedButtonPressed()
    {
        print("Feeding time");
    }

    public void ExitShopButtonPressed()
    {
        EnableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.HomeBG);
        DisableMenu((int)eMenus.Shop);
        DisableMenu((int)eMenus.ShopBG);
    }

    public void ChooseEgg(Monster monsteregg)
    {
        if (GM.CurMonsters[(int)GM.curHomeScreen].CurMonster != null)
        {
            SetText((int)eTextfields.ShopDialogue, "There is already a monster in this spot!");
        }
        else
        {
            if (GM.CurMonsters[(int)GM.curHomeScreen].Unlocked)
            {
                freeSlot = (int)GM.curHomeScreen;
                print("free slot is no " + freeSlot);
                curEgg = monsteregg;
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
            GM.CurMonsters[freeSlot].CurMonster = curEgg;
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
        SetGold(-GM.CurMonsters[freeSlot].CurMonster.Cost);
        //GM.HomeCam.SetScreen((eCurHomeScreen)freeSlot);
        yield return new WaitForSeconds(0.3f);
        DisableMenu((int)eMenus.S_EggMenu);
        StartCoroutine(GM.CurMonsters[freeSlot].C_SetStage(eMonsterStage.egg));
        DisableMenu((int)eMenus.S_BottomButtons);
        SetText((int)eTextfields.ShopDialogue, "Tap egg to hatch it!");
        EnableMenu((int)eMenus.S_TappEggButton);
        //EnableMenu(Menus[(int)eMenus.Home]);
    }

    public IEnumerator CancelEggPurchas()
    {
        //make button deselected
        //EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        SetText((int)eTextfields.ShopDialogue, "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }

    public void TapEgg()
    {
        hatchTaps += 1;
        //print("tapped " + hatchTaps);
        if (hatchTaps == 5)
        {
            GM.CurMonsters[freeSlot].Rarity = (eRarity)Random.Range(0, 3);
            print(GM.CurMonsters[freeSlot].Rarity);
            StartCoroutine(WaitForEggToHatch());
            DisableMenu((int)eMenus.S_TappEggButton);
        }
    }

    public IEnumerator WaitForEggToHatch()
    {
        StartCoroutine(GM.CurMonsters[freeSlot].C_SetStage(eMonsterStage.baby));
        yield return new WaitForSeconds(0.5f);
        DisableMenu((int)eMenus.ShopBG);
        DisableMenu((int)eMenus.Shop);
        EnableMenu((int)eMenus.Home);
        EnableMenu((int)eMenus.HomeBG);
    }

    public void SetGold(int value)
    {
        GM.PlayerMoney += value;
        SetText((int)eTextfields.GoldCount, "" + GM.PlayerMoney);
    }    
}
