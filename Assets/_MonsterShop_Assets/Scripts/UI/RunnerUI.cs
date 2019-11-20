using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerUI : UIController
{
    //public Text CollectedText;
    //public Text GameEndText;
    //public Text CollectedFeedbackText;
    private enum eMenus
    {

    }

    private enum eButtons
    {
    }

    private enum eTextfields
    {
        CollectedCount,
        OnCollectFeedback,
        GameOverFeedback,
    }

    private void Start()
    {
        SetUIinManager();
    }

    public void GameOver(bool win)
    {
        if (win)
        {
            GameManager.Instance.CurUI.SetText((int)eTextfields.GameOverFeedback, "YOU WIN");
        }
        else
        {
            GameManager.Instance.CurUI.SetText((int)eTextfields.GameOverFeedback, "GAME OVER");
        }
    }
}
