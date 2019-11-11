using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UIController
{
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
    
    public void ChooseEgg()
    {
        //check if there are creatures in every slot already
        //if (GameManager.inst.)
        //if not, save empty slot position

        SetText(Textfields[(int)eTexts.ShopDialogue], "You really wanna buy this egg?");
        // Y/N popup    
        DisableMenu(Menus[(int)eMenus.S_EggMenu]);
        EnableMenu(Menus[(int)eMenus.S_PurchaseConfirm]);
        DisableMenu(Menus[(int)eMenus.S_BottomButtons]);
    }

    public void ConfirmEggPurchase(bool isTrue)
    {
        if (isTrue)
        {
            //put egg in empty slot position
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
        yield return new WaitForSeconds(0.5f);
        DisableMenu(Menus[(int)eMenus.Shop]);
        EnableMenu(Menus[(int)eMenus.Hatchery]);
        //place egg n shit?! 
    }

    public IEnumerator CancelEggPurchas()
    {
        EnableMenu(Menus[(int)eMenus.S_EggMenu]);
        SetText(Textfields[(int)eTexts.ShopDialogue], "Can I get you another one?");
        yield return new WaitForSeconds(0.5f);
    }
}
