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

    GameManager GM;

    //[HideInInspector]
    public float curValueModifier;
    public float curSpeedModifier = 1.01f;
    public float CollectedCount;
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
    
    private void Start()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.runnerController = this;
        }
        else
        {
            Debug.LogWarning("Could not find Game Manager!");
        }


        InstantiateNextTile(curTile);
        curTile += 1;
        InstantiateNextTile(curTile);
    }


    public IEnumerator cOnCollectFeedback()
    {
        CollectedFeedbackText.text = "+" + _CollectableValue + "XP";
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
