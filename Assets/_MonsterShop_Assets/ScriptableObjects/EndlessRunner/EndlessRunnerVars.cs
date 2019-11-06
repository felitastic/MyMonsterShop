using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "RunnerVars", menuName = "MonsterShop/EndlessRunner")]
public class EndlessRunnerVars : ScriptableObject
{
    [Header("BALANCING values for GD")]
    [Tooltip("Base XP value of one powerup")]
    public float CollectableValue;
    [Tooltip("Speed of the monster forwards")]
    public float VerticalSpeed;
    [Tooltip("Speed of the monster left and right")]
    public float HorizontalSpeed;
    [Tooltip("Speed multiplier for every checkpoint")]
    public float SpeedModifier;
    [Tooltip("Added value for the powerup per Checkpoint")]
    public float ValueModifier;
    [Tooltip("Multiplier for reward after reaching the goal")]
    public float GoalReward;

    [Header("ONLY for programmer")]
    public float CollectedCount;
    public int curTile;
    [Tooltip("Prefabs of the tiles of this level")]
    public GameObject[] LevelTiles;
    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;

    [Header("Copies for changing values")]
    //[HideInInspector]
    public float curCollectableValue;
    //[HideInInspector]
    public float curVerticalSpeed;
    //[HideInInspector]
    public float curHorizontalSpeed;
    //[HideInInspector]
    public float curCollectedCount;
    //[HideInInspector]
    public int StartTile;

    //public Text GameEndText;
    //[Tooltip("Temporary feedback text for XP gain")]
    //public Text CollectedFeedbackText;
    //public GameObject LevelSpawn;
    public void ResetToStartValues()
    {
        curCollectableValue = CollectableValue;
        curVerticalSpeed = VerticalSpeed;
        curHorizontalSpeed = HorizontalSpeed;
        curCollectedCount = CollectedCount;
        StartTile = curTile;
    }

    public void ModifySpeed()
    {
        curVerticalSpeed *= SpeedModifier;
    }

    public void AddCollectableValue()
    {
        curCollectableValue += ValueModifier;
    }

    public void CollectedCountWinModifier()
    {
        curCollectableValue *= GoalReward;
    }

    public void UpdateCollectedCount(float value)
    {
        curCollectedCount += value;
        CollectedText.text = "" + Mathf.RoundToInt(curCollectedCount);
    }


    public IEnumerator cOnCollectFeedback()
    {
        CollectedFeedbackText.text = "+" + curCollectableValue + "XP";
        yield return new WaitForSeconds(0.1f);
        CollectedFeedbackText.text = "";

        //GameObject feedback = Instantiate(FeedbackPrefab, FeedbackSpawm);
        //feedback.transform.SetParent(FeedbackSpawm, true);
        //Destroy(feedback, 0.3f);
    }

    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(0.5f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void InstantiateNextTile(int whichTile)
    {
        Instantiate(LevelTiles[whichTile]);
        //Instantiate(vars.LevelTiles[curTile+1], LevelSpawn.transform);
    }

}
