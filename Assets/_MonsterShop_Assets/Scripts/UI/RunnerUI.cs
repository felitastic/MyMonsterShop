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
        StartText,
        MonsterLevel
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

        // set lvl bar    
        float curXP = GM.CurMonsters[(int)GM.curMonsterSlot].MonsterXP;
        float LevelUpAt = GM.runnerMonsterManager.LevelUpXpValue();
        xpBar.fillAmount = curXP / LevelUpAt;        

        // set result scene menus and values
        DisableMenu((int)eMenus.InGameStuff);
        GM.runnerMonsterManager.SpawnCurrentMonster(GM.runnerMonsterManager.ResultMonsterSpawn);
        SetText((int)eTextfields.CollectedNo, GM.runnerController.CollectedCount + " x");
        SetText((int)eTextfields.XPValue, GM.runnerController.CollectedXP + " XP");
        EnableMenu((int)eMenus.ResultScreen);
        // move camera on result scene
        Camera.main.transform.position = GM.runnerController.ResultCamPos;

        yield return new WaitForSeconds(1f);

        // gain xp and update level bar
        GM.runnerMonsterManager.SetMonsterXP(GM.runnerController.CollectedXP);
        curXP = GM.CurMonsters[(int)GM.curMonsterSlot].MonsterXP;
        xpBar.fillAmount = curXP / LevelUpAt;

        // if xpbar is full, check for level up
        if (xpBar.fillAmount >= 1.0f)
        {
            if (GM.runnerMonsterManager.CheckForMonsterLevelUp())
            {
                //update level text
                
                if (GM.runnerMonsterManager.CheckForStageChange())
                {
                    StartCoroutine(GM.runnerMonsterManager.cLevelUpMonster(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage, GM.runnerMonsterManager.ResultMonsterSpawn));
                }
            }    
        }

        yield return new WaitForSeconds(0.5f);

        // Enable EndResultButton for player to tap and go back to home scene
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
