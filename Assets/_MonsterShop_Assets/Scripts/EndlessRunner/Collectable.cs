using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public RunnerController controller;

    private float value { get { return controller.CollectableValue; } }

    //If player touches Collectable
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.UpdateCollectedCount(value);
            StartCoroutine(controller.cOnCollectFeedback());
            Destroy(this.gameObject, 0.25f);
        }
    }
}
