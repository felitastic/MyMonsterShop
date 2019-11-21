using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
    //Game Manager shortcut
    public GameManager GM;

    //Menus in the Scene
    [Tooltip("Menu Objects to en/disable")]
    public GameObject[] Menus;
    //CanvasGroups in the Scene for Fadeout
    [Tooltip("Canvas Groups for Fadeout/in")]
    public CanvasGroup[] Canvasgroups;
    //Text fields for changing them
    public Text[] Textfields;
    //Buttons in the Scene for en/disable etc
    public Button[] Buttons;

    public virtual void SetUIinManager()
    {
        GM = GameManager.Instance;

        GM.CurUI = this;

        if (GM.CurUI != null)
            print("current UI: " + GM.CurUI.name);
        else
            print("current UI controller script not set");

    }

    public virtual void DisableMenu(int menu)
    {
        Menus[menu].SetActive(false);
    }
    public virtual void EnableMenu(int menu)
    { 
        Menus[menu].SetActive(true);
    }
    public virtual void SetText(int textfield, string newText)
    {
        Textfields[textfield].text = newText;
    }
    public virtual void EnableButton(int thisButton)
    {
        Buttons[thisButton].interactable = true;
    }
    public virtual void DisableButton(int thisButton)
    { 
        Buttons[thisButton].interactable = false;
    }
    public virtual void InfoPopup()
    { }
}


