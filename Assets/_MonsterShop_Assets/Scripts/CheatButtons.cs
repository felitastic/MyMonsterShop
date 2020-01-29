using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtons : MonoBehaviour
{
    private HomeUI homeUI;
    private GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;
        homeUI = GM.homeUI;
    }

    public void OpenCheatMenu()
    {
        homeUI.SetUIStage(HomeUI.eHomeUIScene.Cheats);
    }

    public void CloseCheatMenu()
    {
        homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
    }

    /// <summary>
    /// Next hatched monster will be epic
    /// </summary>
    public void GetEpic()
    {
        GM.getEpic = true;
        homeUI.SetPopInfoWindowStatus(true, "Your next hatch will be Epic Rarity!");
        homeUI.EnablePopupInfoCloseButton();
    }

    /// <summary>
    /// Next hatched monster will be legendary
    /// Unless epic is turned on, then it will be the one after
    /// </summary>
    public void GetLegendary()
    {
        GM.getLegendary = true;

        if (GM.getEpic)
            homeUI.SetPopInfoWindowStatus(true, "Your 2nd next hatch will be Legendary Rarity!");
        else
            homeUI.SetPopInfoWindowStatus(true, "Your next hatch will be Legendary Rarity!");
        homeUI.EnablePopupInfoCloseButton();
    }

    public void GiveMoney(int value)
    {
        GM.ChangePlayerGold(+value);
    }

    public void ToggleDungeonLord()
    {
        if (GM.DLIsGone)
        {
            //change dungeonlord endtime GM
            GM.DLIsGone = false;
            GM.DungeonLordWaitTimeEnd = System.DateTime.Now;
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
        else
        {
            //change dungeonlord endtime GM
            GM.DLIsGone = true;
            GM.SetDLTimer();
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
    }

    /// <summary>
    /// Current Monster gains one level, can also lead to growth
    /// </summary>
    public void OneLevelUp()
    {
        //steal logic from petting scene
        //calculate difference between cur xp and next level threshold
        //give enough xp to reach next threshold
        //do the levelup and growth
    }
}
