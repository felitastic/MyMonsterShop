using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerController : MonoBehaviour
{
    [Header("BALANCING values for GD")]
    [Tooltip("Base XP value of one powerup")]
    public float CollectableValue;
    [Tooltip("Speed of the monster forwards")]
    public float VerticalSpeed = 1f;
    [Tooltip("Speed of the monster left and right")]
    public float HorizontalSpeed = 1f;
    [Tooltip("Speed multiplier for every checkpoint")]
    public float SpeedModifier = 1f;    
    [Tooltip("Added value for the powerup per Checkpoint")]
    public float ValueModifier = 1f;    
    [Tooltip("Multiplier for reward after reaching the goal")]
    public float GoalReward = 1f;

    [Header("Do no touch unless you are The Programmer")]
    public float CollectedCount;
    public Text CollectedText;
    public Text CollectedFeedbackText;

    //Spawned when player collects item
    //public GameObject FeedbackPrefab;
    //public RectTransform FeedbackSpawm;

    void Start()
    {
        CollectedCount = 0;
    }

    public void ModifySpeed()
    {
        VerticalSpeed *= SpeedModifier;
    }

    public void AddCollectableValue()
    {
        CollectableValue += ValueModifier;
    }

    public void CollectedCountWinModifier()
    {
        CollectedCount *= GoalReward;
    }

    public void UpdateCollectedCount(float value)
    {
        CollectedCount += value;
        CollectedText.text = "" + Mathf.RoundToInt(CollectedCount);
    }

    public IEnumerator cOnCollectFeedback()
    {
        CollectedFeedbackText.text = "+" + CollectableValue + "XP";
        yield return new WaitForSeconds(0.25f);
        CollectedFeedbackText.text = "";

        //GameObject feedback = Instantiate(FeedbackPrefab, FeedbackSpawm);
        //feedback.transform.SetParent(FeedbackSpawm, true);
        //Destroy(feedback, 0.3f);
    }
}
