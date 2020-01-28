using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtons : MonoBehaviour
{
    private HomeUI homeUI;
    private GameManager GM;
    public GameObject CheatMenu;

    void Start()
    {
        GM = GameManager.Instance;
        homeUI = GM.homeUI;
    }

    public void OpenCheatMenu()
    {
        CheatMenu.SetActive(true);
        homeUI.DisableSwiping(true);
        //disable other menus? 
    }

    public void CloseCheatMenu()
    {
        CheatMenu.SetActive(false);
        homeUI.DisableSwiping(false);
        //close cheat menu
        //enable swiping
        //enable other menus?
    }


}
