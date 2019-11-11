using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
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
        GameManager.inst.CurUI = this;

        if (GameManager.inst.CurUI != null)
            print("current UI: " + GameManager.inst.CurUI.name);
        else
            print("current UI controller script not set");
    }

    public virtual void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
    public virtual void EnableMenu(GameObject menu)
    { 
        menu.SetActive(true);
    }
    public virtual void SetText(Text textfield, string newText)
    { }
    public virtual void FadeOut(Text thisText)
    { }

    public virtual void EnableButton(Button thisButton)
    { }
    public virtual void DisableButton(Button thisButton)
    { }

    public virtual void InfoPopup()
    { }
}


