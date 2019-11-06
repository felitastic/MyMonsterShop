using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    float speedModifier { get { return RunnerController.inst.SpeedModifier; } }
    float valueModifier { get { return RunnerController.inst.ValueModifier; } }
    float goalReward { get { return RunnerController.inst.GoalReward; } }

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
                CollectedCountWinModifier();
                RunnerController.inst.GameEndText.text = "YOU WIN";                
            }
            else
            {
                Debug.LogError("No tag set to checkpoint: " + this.gameObject.name);
            }
        }
    }

    public void ModifySpeed()
    {
        RunnerController.inst.VerticalSpeed *= speedModifier;
    }

    public void AddCollectableValue()
    {
        RunnerController.inst.CollectableValue += valueModifier;
    }

    public void CollectedCountWinModifier()
    {
        RunnerController.inst.CollectableValue *= goalReward;
    }
}
