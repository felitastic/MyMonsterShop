using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
    public static RunnerController inst;

    [Header("BALANCING values for GD")]
    [Tooltip("Speed of the monster forwards")]
    public float VerticalSpeed;
    [Tooltip("Speed of the monster left and right")]
    public float HorizontalSpeed;
    [Tooltip("Speed multiplier for every checkpoint")]
    public float SpeedModifier;
    [Tooltip("Base XP value of one powerup")]
    public float CollectableValue;
    [Tooltip("Added value for the powerup per Checkpoint")]
    public float ValueModifier;
    [Tooltip("Multiplier for reward after reaching the goal")]
    public float GoalReward;

    [Header("ONLY for programmer")]
    public float CollectedCount;
    [HideInInspector]
    public int curTile;
    [Tooltip("Prefabs of the tiles of this level")]
    public GameObject[] LevelTiles;

    //public float TileDistance = 19.0f;

    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;


    private void Start()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);

        InstantiateNextTile(curTile);
        curTile += 1;
        InstantiateNextTile(curTile);
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
