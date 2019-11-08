using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
    //Menus in the Scene
    public GameObject[] Menus;
    //CanvasGroups in the Scene for Fadeout
    public CanvasGroup[] Canvasgroups;
    //Text fields for changing them
    public Text[] Textfields;
    //Buttons in the Scene for en/disable etc
    public Button[] Buttons;

    public void SetUIinManager()
    {
        GameManager.inst.curUI = this;
    }

    public void DisableMenu(GameObject menu)
    { }
    public void EnableMenu(GameObject menu)
    { }
    public void SetText(string newText)
    { }
    public void FadeOut(Text thisText)
    { }

    public void EnableButton(Button thisButton)
    { }
    public void DisableButton(Button thisButton)
    { }

    public void InfoPopup()
    { }
}


