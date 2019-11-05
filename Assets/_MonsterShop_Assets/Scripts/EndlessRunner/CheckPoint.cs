using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public EndlessRunnerVars vars;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("Checkpoint"))
            {
                vars.ModifySpeed();
                vars.AddCollectableValue();
            }
            else if (this.CompareTag("Goal"))
            {
                vars.CollectedCountWinModifier();
                vars.GameEndText.text = "YOU WIN";                
            }
            else
            {
                Debug.LogError("No tag set to checkpoint: " + this.gameObject.name);
            }
        }
    }
}
