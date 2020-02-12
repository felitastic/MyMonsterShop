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
    public Camera Cam;

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
    public Animator CollectedAnimation;
         
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

    public void OnCollectFeedback()
    {
        CollectedFeedbackText.text = "+" + curCollectableValue + "XP";
        CollectedAnimation.SetTrigger("gain");
    }

    public IEnumerator cShake(float duration, float magnitude)
    {
        print("start camshake");
        Vector3 originalPos = Cam.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Cam.transform.position = new Vector3(x + originalPos.x, y + originalPos.y, originalPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;      //waits for the next frame before continuing while loop
        }

        print("end camshake");
        Cam.transform.position = originalPos;
    }

    public IEnumerator cGameEnd(GameObject monster)
    {
        yield return new WaitForSeconds(0.2f);

        if (GameManager.Instance.runnerController.win)
        {
            UI.SetGameEndText("SUCCESS!");
            yield return new WaitForSeconds(0.5f);
            Destroy(monster, 0.52f);
        }
        else
        {
            UI.SetGameEndText("GAME OVER");
            yield return new WaitForSeconds(0.3f);
            Destroy(monster, 0.2f);
        }
        StartCoroutine(UI.cShowResult());
    }

    public void InstantiateNextTile(int whichTile)
    {
        float newYPos = 19 * whichTile;

        if (whichTile < LevelTiles.Length)
        {
            GameObject newTile = Instantiate(LevelTiles[whichTile]);
            newTile.transform.position = new Vector3(0f, newYPos, 0f);
        }
        //print("last tile spawned");
    }

}
