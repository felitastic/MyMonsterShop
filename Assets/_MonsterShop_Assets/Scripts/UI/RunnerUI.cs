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
        RightButton
    }

    private enum eTextfields
    {
        CollectedCount,
        OnCollectFeedback,
        GameOverFeedback,
        CollectedNo,
        XPValue
    }

    private void Awake()
    {
        GetGameManager();
        GM.runnerUI = this;
        DisableButton((int)eButtons.LeftButton);
        DisableButton((int)eButtons.RightButton);
    }

    public void TapResult()
    {
        StartCoroutine(GM.cLoadHomeScene());
    }

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

    public IEnumerator cShowResult()
    {
        yield return new WaitForSeconds(0.2f);
        //xpBar.fillAmount = GM.CurMonsters[(int)GM.curMonsterSlot].CreatureXP / GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[GM.CurMonsters[(int)GM.curMonsterSlot].CreatureLevel];

        //float curXP = GM.thisMonster.CreatureXP;
        //float LevelUpAt = GM.thisMonster.LevelThreshold_current[GM.thisMonster.CreatureLevel];

        //xpBar.fillAmount = curXP / LevelUpAt;

        DisableMenu((int)eMenus.InGameStuff);
        Camera.main.transform.position = GM.runnerController.ResultCamPos;
        //GM.CurMonsters[(int)GM.curMonsterSlot].SpawnCurrentMonster(GM.runnerController.monsterSpawn);

        //GM.CurMonsters[(int)GM.curMonsterSlot].SpawnCurrentMonster(GM.thisMonster.CurMonster.CreaturePrefabs[(int)GM.thisMonster.Rarity, (int)GM.thisMonster.MonsterStage], GM.runnerController.monsterSpawn);

        SetText((int)eTextfields.CollectedNo, GM.runnerController.CollectedCount + " x");
        SetText((int)eTextfields.XPValue, GM.runnerController.CollectedXP + " XP");
        EnableMenu((int)eMenus.ResultScreen);
        yield return new WaitForSeconds(1f);

        //GM.thisMonster.CreatureXP += GM.runnerController.CollectedXP;
        //curXP = GM.thisMonster.CreatureXP; 
        //xpBar.fillAmount = curXP / LevelUpAt;

        //check if level up

        yield return new WaitForSeconds(0.5f);

        //GM.thisMonster.MonsterStage += 1;
        //StartCoroutine(GM.CurMonsters[(int)GM.curMonsterSlot].C_SetStage(GM.thisMonster.MonsterStage, GM.runnerController.monsterSpawn));

        //StartCoroutine(GM.CurMonsters[(int)GM.curMonsterSlot].C_SetStage(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage + 1, GM.runnerController.monsterSpawn));      
        yield return new WaitForSeconds(0.5f);
        EnableMenu((int)eMenus.EndResultButton);
    }

    public void StartGame()
    {
        DisableMenu((int)eMenus.StartButton);
        EnableButton((int)eButtons.LeftButton);
        EnableButton((int)eButtons.RightButton);
    }
}
