using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerController : MonoBehaviour
{
    [Header("Draag n Drop")]
    public EndlessRunner_Values RunnerValues;
    public PlayerControls playerControls;
    public RunnerUI UI;

    public GameManager GM;

    //[HideInInspector]
    public float curCollectableValue;
    public float curSpeedModifier;
    public float CollectedCount;
    public float CollectedXP;
    public float CollectedResult;
    public Vector3 ResultCamPos;

    //public float TileDistance = 19.0f;

    //kommt in die UI
    public Text CollectedText;
    public Text GameEndText;
    public Text CollectedFeedbackText;
         
    //level spawn stuff, maybe second controller dafür
    public int curTile = 0;
    [Tooltip("Prefabs of the tiles of this level")]
    public GameObject[] LevelTiles;
    public bool IsRunning;
    public bool win;

    private void Awake()
    {
        IsRunning = false;
        ResultCamPos = new Vector3(-25.0f, 0.0f, -10.0f);
        curSpeedModifier = 1.00f;
        curCollectableValue = RunnerValues.CollectableValue;
        InstantiateNextTile(curTile);
        curTile += 1;
        InstantiateNextTile(curTile);
    }

    private void Start()
    {
        GM = GameManager.Instance;
        GM.runnerController = this;
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

    public IEnumerator cGameEnd()
    {
        //show fancy anims oder so
        GM.runnerController.IsRunning = false;
        yield return new WaitForSeconds(0.01f);
        UI.GameOver();
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
    }

    public void InstantiateNextTile(int whichTile)
    {
        float newYPos = 19 * whichTile;

        if (whichTile < LevelTiles.Length)
        {
            GameObject newTile = Instantiate(LevelTiles[whichTile]);
            newTile.transform.position = new Vector3(0f, newYPos, 0f);
        }
        print("last tile spawned");
    }

}
