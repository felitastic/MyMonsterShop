using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class _testabstractclass : MonoBehaviour
{
    //for dis-enabling?
    public GameObject[] Menus;
    //for fadeout or interaction (buttons?)
    public CanvasGroup[] Canvasgroups;
    public Text[] Textfields;
    public abstract void DisableMenu(GameObject menu);
    public abstract void SetText(string newText);
    public abstract void FadeOut(string newText);
}
