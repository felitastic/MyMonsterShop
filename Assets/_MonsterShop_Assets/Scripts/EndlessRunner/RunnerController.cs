using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
    [Header("Draag n Drop")]
    public EndlessRunner_Values RunnerValues;
    public PlayerControls playerControls;
    public RunnerUI UI;

    GameManager GM;

    //[HideInInspector]
    public float curCollectableValue;
    public float curSpeedModifier;
    public float CollectedCount;
    public float CollectedXP;
    public float CollectedResult;

    //public float TileDistance = 19.0f;

    //kommt in die UI
    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;
         
    //level spawn stuff, maybe second controller dafür
    public int curTile;
    [Tooltip("Prefabs of the tiles of this level")]
    public GameObject[] LevelTiles;
    public bool IsRunning;

    private void Start()
    {
        IsRunning = false;

        if (GameManager.Instance)
        {
            GameManager.Instance.runnerController = this;
        }
        else
        {
            Debug.LogWarning("Could not find Game Manager!");
        }

        curSpeedModifier = 1.00f;
        curCollectableValue = RunnerValues.CollectableValue;

        InstantiateNextTile(curTile);
        curTile += 1;
        InstantiateNextTile(curTile);
    }

    public void StartPressed()
    {
        IsRunning = true;
    }

    public IEnumerator cOnCollectFeedback()
    {
        CollectedFeedbackText.text = "+" + curCollectableValue + "XP";
        yield return new WaitForSeconds(0.2f);
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
