using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateCollectedCount();
            StartCoroutine(GameManager.Instance.runnerController.cOnCollectFeedback());
            Destroy(this.gameObject, 0.25f);
        }
    }

    public void UpdateCollectedCount()
    {
        GameManager.Instance.runnerController.CollectedCount += 1;
        GameManager.Instance.runnerController.CollectedXP += GameManager.Instance.runnerController.curCollectableValue;
        GameManager.Instance.runnerController.CollectedText.text = "" + Mathf.RoundToInt(GameManager.Instance.runnerController.CollectedCount);
        print("cur XP: " + GameManager.Instance.runnerController.CollectedXP);
    }
}
