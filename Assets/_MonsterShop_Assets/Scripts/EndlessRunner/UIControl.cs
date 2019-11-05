using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [Header("Do no touch unless you are The Programmer")]
    public EndlessRunnerVars vars;
    public float CollectedCount;
    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;

    public void Start()
    {
        vars.CollectedText = CollectedText;
        vars.CollectedCount = CollectedCount;
        vars.GameEndText = GameEndText;
        vars.CollectedFeedbackText = CollectedFeedbackText;
        vars.InstantiateNextTile(vars.curTile);
        vars.curTile += 1;
        vars.InstantiateNextTile(vars.curTile);
    }
}
