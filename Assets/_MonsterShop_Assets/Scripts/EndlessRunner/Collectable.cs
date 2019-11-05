using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public EndlessRunnerVars vars;

    private float value { get { return vars.CollectableValue; } }

    //If player touches Collectable
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vars.UpdateCollectedCount(value);
            StartCoroutine(vars.cOnCollectFeedback());
            Destroy(this.gameObject, 0.25f);
        }
    }
}
