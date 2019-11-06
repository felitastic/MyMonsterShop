using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    float valueModifier { get { return RunnerController.inst.ValueModifier; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateCollectedCount();
            StartCoroutine(RunnerController.inst.cOnCollectFeedback());
            Destroy(this.gameObject, 0.25f);
        }
    }

    public void UpdateCollectedCount()
    {
        RunnerController.inst.CollectedCount += valueModifier;
        RunnerController.inst.CollectedText.text = "" + Mathf.RoundToInt(RunnerController.inst.CollectedCount);
    }
}
