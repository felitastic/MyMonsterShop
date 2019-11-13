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

    public enum eMenus
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
        S_TappEggButton
    }

    public enum eButtons
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

    public enum eTexts
    {
        ShopDialogue
    }

    public void Start()
    {
        SetUIinManager();
    }

    public void ShopButtonPressed()
    {
        DisableMenu(Menus[(int)eMenus.HomeBG]);
        DisableMenu(Menus[(int)eMenus.Home]);
        EnableMenu(Menus[(int)eMenus.ShopBG]);
        EnableMenu(Menus[(int)eMenus.Shop]);
        EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        EnableMenu(Menus[(int)eMenus.S_BottomButtons]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "What can I do for you?");
    }

    public void ExitShopButtonPressed()
    {
        EnableMenu(Menus[(int)eMenus.Home]);
        EnableMenu(Menus[(int)eMenus.HomeBG]);
        DisableMenu(Menus[(int)eMenus.Shop]);
        DisableMenu(Menus[(int)eMenus.ShopBG]);
    }

    public void ChooseEgg(Monster monsteregg)
    {
        if (GM.CurMonsters[(int)GM.curHomeScreen].CurMonster != null)
        {
            SetText(Textfields[(int)eTexts.ShopDialogue], "There is already a monster in this spot!");
        }
        else
        {
            if (GM.CurMonsters[(int)GM.curHomeScreen].Unlocked)
            {
                freeSlot = (int)GM.curHomeScreen;
                print("free slot is no " + freeSlot);
                curEgg = monsteregg;
                SetText(Textfields[(int)eTexts.ShopDialogue], "You really wanna buy this egg?");
                // Make Button "selected"
                //DisableMenu(Menus[(int)eMenus.S_EggMenu]);
                DisableMenu(Menus[(int)eMenus.S_BottomButtons]);
                EnableMenu(Menus[(int)eMenus.S_PurchaseConfirm]);
            }
            else
            {
                SetText(Textfields[(int)eTexts.ShopDialogue], "You haven't unlocked this spot yet!");
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

        DisableMenu(Menus[(int)eMenus.S_PurchaseConfirm]);
    }

    public IEnumerator ConfirmEggPurchase()
    {
        SetText(Textfields[(int)eTexts.ShopDialogue], "Alright, one egg to go!");
        //GM.HomeCam.SetScreen((eCurHomeScreen)freeSlot);
        yield return new WaitForSeconds(0.3f);
        DisableMenu(Menus[(int)eMenus.S_EggMenu]);
        StartCoroutine(GM.CurMonsters[freeSlot].C_SetStage(eMonsterStage.egg));
        DisableMenu(Menus[(int)eMenus.S_BottomButtons]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "Tap egg to hatch it!");
        EnableMenu(Menus[(int)eMenus.S_TappEggButton]);
        //EnableMenu(Menus[(int)eMenus.Home]);
    }

    public IEnumerator CancelEggPurchas()
    {
        //make button deselected
        //EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }

    public void TapEgg()
    {
        hatchTaps += 1;
        print("tapped " + hatchTaps);
        if (hatchTaps == 5)
        {
            GM.CurMonsters[freeSlot].Rarity = (eRarity)Random.Range(0, 4);
            StartCoroutine(WaitForEggToHatch());
            DisableMenu(Menus[(int)eMenus.S_TappEggButton]);
        }
    }

    public IEnumerator WaitForEggToHatch()
    {
        StartCoroutine(GM.CurMonsters[freeSlot].C_SetStage(eMonsterStage.baby));
        yield return new WaitForSeconds(0.5f);
        DisableMenu(Menus[(int)eMenus.ShopBG]);
        EnableMenu(Menus[(int)eMenus.Home]);
        EnableMenu(Menus[(int)eMenus.HomeBG]);
    }
}
