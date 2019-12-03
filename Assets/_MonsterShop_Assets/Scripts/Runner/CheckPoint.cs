using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //float speedModifier { get { return RunnerController.inst.SpeedModifier; } }
    //float valueModifier { get { return RunnerController.inst.ValueModifier; } }
    //float goalReward { get { return RunnerController.inst.GoalReward; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("Checkpoint"))
            {
                ModifySpeed();
                AddCollectableValue();
            }
            else if (this.CompareTag("Goal"))
            {
                //CollectedCountWinModifier();
                GameManager.Instance.runnerController.curSpeedModifier = 0.5f;
                GameManager.Instance.runnerController.win = true;
                StartCoroutine(GameManager.Instance.runnerController.cGameEnd());
            }
            else
            {
                Debug.LogError("No tag set to checkpoint: " + this.gameObject.name);
            }
        }
    }

    public void ModifySpeed()
    {
        GameManager.Instance.runnerController.curSpeedModifier *= GameManager.Instance.runnerController.RunnerValues.SpeedModifier;
    }

    public void AddCollectableValue()
    {
        GameManager.Instance.runnerController.curCollectableValue += GameManager.Instance.runnerController.RunnerValues.ValueModifier;
    }

    //public void CollectedCountWinModifier()
    //{
    //    GameManager.Instance.runnerController.CollectedXP *= GameManager.Instance.runnerController.RunnerValues.GoalReward;
    //}
}
