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

    public void TogglePetTimer()
    {
        if (GM.CurMonsters[GM.curMonsterID].IsHappy)
        {
            //change dungeonlord endtime GM
            GM.CurMonsters[GM.curMonsterID].IsHappy = false;
            GM.CurMonsters[GM.curMonsterID].PetTimerEnd = System.DateTime.Now;
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
        else
        {
            //change dungeonlord endtime GM
            GM.CurMonsters[GM.curMonsterID].IsHappy = true;
            GM.SetPetTimer(GM.curMonsterID);
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
    }
    public void TogglePlayTimer()
    {
        if (GM.CurMonsters[GM.curMonsterID].IsTired)
        {
            //change dungeonlord endtime GM
            GM.CurMonsters[GM.curMonsterID].IsTired = false;
            GM.CurMonsters[GM.curMonsterID].PlayTimerEnd = System.DateTime.Now;
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
        else
        {
            //change dungeonlord endtime GM
            GM.CurMonsters[GM.curMonsterID].IsTired = true;
            GM.SetPlayTimer(GM.curMonsterID);
            //check if timer should be running Timer
            GM.monsterTimer.CheckDateTimes();
        }
    }

    public void UnlockAllKompendiumEntries()
    {
        for (int i = 0; i < GM.UnlockedLogEntries.Length; ++i)
        {
            GM.UnlockedLogEntries[i] = true;
        }
    }

    public void UnlockMonsterSlots()
    {
        foreach (MonsterSlot slot in GM.CurMonsters)
        {
            if (!slot.Unlocked)
                slot.Unlocked = true;
        }
        GM.homeUI.SetSlotSymbol();
    }
    /// <summary>
    /// Current Monster gains one level, can also lead to growth
    /// </summary>
    public void OneLevelUp()
    {
        if (GM.CurMonsters[GM.curMonsterID].Monster != null)
        {
            float curXP = GM.CurMonsters[GM.curMonsterID].MonsterXP;
            float totalXPforStageUp;
            float XPdiff;

            if (GM.CurMonsters[GM.curMonsterID].MonsterLevel < 7)
            {
                if (GM.CurMonsters[GM.curMonsterID].MonsterLevel < 4)
                {
                    //lvl 1-3
                    totalXPforStageUp = GM.CurMonsters[GM.curMonsterID].LevelThreshold_current[2];
                    XPdiff = totalXPforStageUp - curXP;

                }
                else
                {
                    //lvl 4-6
                    totalXPforStageUp = GM.CurMonsters[GM.curMonsterID].LevelThreshold_current[5];
                    XPdiff = totalXPforStageUp - curXP;
                }

                GM.homeUI.SetUIStage(HomeUI.eHomeUIScene.none);
                GM.homeUI.DisableSwiping(true);
                GM.homeMonsterManager.SetMonsterXP(XPdiff);
                StartCoroutine(cSetNewStage());
            }
            else
            {
                print("monster is already an adult");
            }
        }
    }

    private IEnumerator cSetNewStage()
    {
        while (GM.homeMonsterManager.CheckForMonsterLevelUp())
        {
            GM.homeUI.SetMonsterXPBarUndLevel();
            //TODO vfx effekt level up
            if (GM.homeMonsterManager.CheckForStageChange())
            {
                StartCoroutine(GM.homeMonsterManager.cLevelUpMonster(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage, GM.homeMonsterManager.MonsterSpawn[(int)GM.curMonsterSlot]));
                yield return new WaitForSeconds(1.0f);
            }
            yield return new WaitForSeconds(0.5f);
            GM.homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
            GM.homeUI.DisableSwiping(false);
        }
    }
}
