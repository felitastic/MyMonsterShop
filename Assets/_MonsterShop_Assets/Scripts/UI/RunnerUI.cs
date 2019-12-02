using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RunnerUI : UIController
{
    //public Text CollectedText;
    //public Text GameEndText;
    //public Text CollectedFeedbackText;

    public Image xpBar;

    private enum eMenus
    {
        StartButton,
        InGameStuff,
        ResultScreen,
        EndResultButton
    }

    private enum eButtons
    {
        LeftButton,
        RightButton,
        StartButton
    }

    private enum eTextfields
    {
        CollectedCount,
        OnCollectFeedback,
        GameOverFeedback,
        CollectedNo,
        XPValue,
        StartText
    }

    private void Awake()
    {
        DisableButton((int)eButtons.LeftButton);
        DisableButton((int)eButtons.RightButton);
        DisableButton((int)eButtons.StartButton);
        SetText((int)eTextfields.StartText, "");
    }
    
    private void Start()
    {
        GetGameManager();
        GM.runnerUI = this;
    }

    // Called by StartButton
    public void StartPressed()
    {
        DisableMenu((int)eMenus.StartButton);
        EnableButton((int)eButtons.LeftButton);
        EnableButton((int)eButtons.RightButton);
        GM.runnerController.IsRunning = true;
    }

    // Called after monster hits an obstacle
    public void GameOver()
    {
        if (GM.runnerController.win)
        {
            GM.runnerUI.SetText((int)eTextfields.GameOverFeedback, "YOU WIN");
        }
        else
        {
            GM.runnerUI.SetText((int)eTextfields.GameOverFeedback, "GAME OVER");
        }
        StartCoroutine(cShowResult());
    }

    // called after game over or win 
    public IEnumerator cShowResult()
    {
        yield return new WaitForSeconds(0.5f);
        xpBar.fillAmount = GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP / GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel];

        float curXP = GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP;
        float LevelUpAt = GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel];

        xpBar.fillAmount = curXP / LevelUpAt;

        DisableMenu((int)eMenus.InGameStuff);
        Camera.main.transform.position = GM.runnerController.ResultCamPos;

        GM.runnerMonsterManager.SpawnCurrentMonster(GM.runnerMonsterManager.ResultMonsterSpawn);

        SetText((int)eTextfields.CollectedNo, GM.runnerController.CollectedCount + " x");
        SetText((int)eTextfields.XPValue, GM.runnerController.CollectedXP + " XP");
        EnableMenu((int)eMenus.ResultScreen);
        yield return new WaitForSeconds(1f);

        GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP += GM.runnerController.CollectedXP;
        curXP = GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP;
        xpBar.fillAmount = curXP / LevelUpAt;
        

        // check if level up (change to do from the back?, highest first, then go down)
        if (GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP > GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel])
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel += 1;

            // check for level up, which will happen at 3, 6 and 9
            if (GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel % 3 == 0)
            {
                StartCoroutine(GM.runnerMonsterManager.cLevelUpMonster(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + 1, GM.runnerMonsterManager.ResultMonsterSpawn));
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        EnableMenu((int)eMenus.EndResultButton);
    }

    // EndResultButtons function
    public void TapResult()
    {
        StartCoroutine(GM.cLoadHomeScene());
    }

    // Endabling Startbutton after a short wait to make sure everything has loaded
    public void EnableStartButton()
    {
        //print("enabling start button");
        SetText((int)eTextfields.StartText, "Tap to start");
        EnableButton((int)eButtons.StartButton);
    }
}
