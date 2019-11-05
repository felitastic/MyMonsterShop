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
    public float VerticalSpeed = 1f;
    [Tooltip("Speed of the monster left and right")]
    public float HorizontalSpeed = 1f;
    [Tooltip("Speed multiplier for every checkpoint")]
    public float SpeedModifier = 1f;
    [Tooltip("Added value for the powerup per Checkpoint")]
    public float ValueModifier = 1f;
    [Tooltip("Multiplier for reward after reaching the goal")]
    public float GoalReward = 1f;

    [Header("ONLY for programmer")]
    public float CollectedCount;
    [Tooltip("Prefabs of the tiles of this level")]
    public GameObject[] LevelTiles;
    public int curTile;
    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;

    //public Text GameEndText;
    //[Tooltip("Temporary feedback text for XP gain")]
    //public Text CollectedFeedbackText;
    //public GameObject LevelSpawn;
    public void ResetValues()
    {
        CollectedCount = 0;
        curTile = 0;
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
