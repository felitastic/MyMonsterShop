using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIController
{
    [SerializeField]
    private Monster curEgg;
    private int freeSlot;
    public enum eMenus
    {
        Hatchery,
        Shop,
        H_SwipeButtons,
        H_BottomButtons,
        S_BottomButtons,
        S_PurchaseConfirm,
        S_EggMenu
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
        DisableMenu(Menus[(int)eMenus.Hatchery]);
        EnableMenu(Menus[(int)eMenus.Shop]);
        EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        EnableMenu(Menus[(int)eMenus.S_BottomButtons]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "What can I do for you?");
    }

    public void ExitShopButtonPressed()
    {
        EnableMenu(Menus[(int)eMenus.Hatchery]);
        DisableMenu(Menus[(int)eMenus.Shop]);
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
                // Y/N popup    
                DisableMenu(Menus[(int)eMenus.S_EggMenu]);
                EnableMenu(Menus[(int)eMenus.S_PurchaseConfirm]);
                DisableMenu(Menus[(int)eMenus.S_BottomButtons]);
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
        GM.HomeCam.SetScreen((eCurHomeScreen)freeSlot);
        yield return new WaitForSeconds(0.5f);
        DisableMenu(Menus[(int)eMenus.Shop]);
        EnableMenu(Menus[(int)eMenus.Hatchery]);
        GM.CurMonsters[freeSlot].SpawnEgg();
    }

    public IEnumerator CancelEggPurchas()
    {
        EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }
}
