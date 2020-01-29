using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameManager GM;

    public float CamXMovement = 10.501f;

    //Time for the whole movement, the higher, the slower the object moves
    public float lerpTime = 0.35f;
    public float zoomTime = 0.10f;
    //Is set to the current time as long as the lerp is running
    float curLerpTime;
    float curZoomTime;
    //To start the lerp
    public bool lerping;
    public bool zooming;
    //Quaternion StartQuaternion;
    Vector3 StartPos;
    //Quaternion EndQuaternion;
    Vector3 EndPos;
    //public List<GameObject> CamTransforms;
    //public GameObject CamHolder;

    float StartZoomPos;
    float EndZoomPos;

    public void Awake()
    {
        GM = GameManager.Instance;
        GM.HomeCam = this;
    }

    private void Start()
    {
        SetCameraPosition(GM.CurCamHomePos);
    }

    private void Update()
    {
        if (lerping)
        {
            curLerpTime += Time.deltaTime;

            if (curLerpTime > lerpTime)
                curLerpTime = lerpTime;

            float percentage = curLerpTime / lerpTime;
            Camera.main.transform.position = Vector3.Lerp(StartPos, EndPos, percentage);
            //CamHolder.transform.rotation = Quaternion.Lerp(StartQuaternion, EndQuaternion, percentage);

            if (Mathf.Approximately(percentage, 1.0f))
            {
                EndLerp();
            }
        }

        if (zooming)
        {
            curZoomTime += Time.deltaTime;
            if (curZoomTime > zoomTime)
                curZoomTime = zoomTime;

            float percentage = curZoomTime / zoomTime;
            Camera.main.orthographicSize = Mathf.Lerp(StartZoomPos, EndZoomPos, percentage);
            Camera.main.transform.position = Vector3.Lerp(StartPos, EndPos, percentage);

            if (Mathf.Approximately(percentage, 1.0f))
            {
                EndZoom();
            }
        }
    }

    /// <summary>
    /// Changes home screen UI values of the monster on swipe
    /// </summary>
    /// <param name="PlusMinus"></param>
    private void SetNewScreenHome(int PlusMinus)
    {
        SetScreen(GM.curMonsterSlot += 1 * PlusMinus);
        //GM.homeUI.SetMonsterTexts();
        //GM.homeUI.SetMonsterValue();
        //GM.homeUI.SetMonsterXPBarUndLevel();
        GM.homeUI.SetSlotSymbol();

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null)
        {
            GM.homeUI.TrainButtonActive(false);
            GM.homeUI.SetPlayTimer(false);
            GM.homeUI.ShowMonsterStats(false);
        }
        else
        {
            GM.monsterTimer.CheckDateTimes();
            GM.homeUI.ShowMonsterStats(true);
            //if (GM.CurMonsters[GM.curMonsterID].IsTired)
            //{
            //    GM.homeUI.SetPlayTimer(true);
            //}
            //else
            //{
            //    GM.homeUI.TrainButtonActive(true);
            //    GM.homeUI.SetPlayTimer(false);
            //}
        }
        GM.homeUI.SetSwipeButtonStatus();
    }

    /// <summary>
    /// Changes dungeon screen UI values of the monster on swipe
    /// </summary>
    /// <param name="PlusMinus"></param>
    private void SetNewScreenDungeon(int PlusMinus)
    {
        SetScreen(GM.curMonsterSlot += 1 * PlusMinus);
        GM.homeUI.SetMonsterTexts();
        GM.homeUI.SetMonsterValue();
        GM.homeUI.SetMonsterlevel_Dungeon();
        GM.homeUI.SetDungeonDialogue();

        if (GM.CurMonsters[(int)GM.curMonsterSlot].Monster == null || GM.CurMonsters[(int)GM.curMonsterSlot].Sold)
        {
            GM.homeUI.SellButtonActive(false);
        }
        else
        {
            GM.homeUI.SellButtonActive(true);
        }

        GM.homeUI.SetSwipeButtonStatus();
    }

    public void PressLeftButton()
    {
        if (GM.homeUI.curScene == HomeUI.eHomeUIScene.Dungeonlord)
        {
            if (GM.curMonsterSlot != ecurMonsterSlot.left && !lerping)
            {
                SetNewScreenDungeon(-1);
            }
        }
        else
        {
            if (GM.curMonsterSlot != ecurMonsterSlot.left && !lerping)
            {
                SetNewScreenHome(-1);
            }
        }
    }

    public void PressRightButton()
    {
        if (GM.homeUI.curScene == HomeUI.eHomeUIScene.Dungeonlord)
        {
            if (GM.curMonsterSlot != ecurMonsterSlot.right && !lerping)
            {
                SetNewScreenDungeon(+1);
            }
        }
        else
        {
            if (GM.curMonsterSlot != ecurMonsterSlot.right && !lerping)
            {
                SetNewScreenHome(+1);
            }
        }
    }
    
    public void SetScreen(ecurMonsterSlot curScreen)
    {
        GM.curMonsterSlot = curScreen;
        StartPos = Camera.main.transform.position;

        switch (GM.curMonsterSlot)
        {
            case ecurMonsterSlot.left:
                EndPos = new Vector3(-CamXMovement, 0, -20);

                break;
            case ecurMonsterSlot.middle:
                EndPos = new Vector3(0, 0, -20);

                break;
            case ecurMonsterSlot.right:
                EndPos = new Vector3(CamXMovement, 0, -20);

                break;
            default:
                break;
        }

        GM.CurCamHomePos = EndPos;
        StartLerp(lerpTime);
    }

    public void Zoom(bool zoomIn)
    {
        StartPos = Camera.main.transform.position;
        StartZoomPos = Camera.main.orthographicSize;

        if (zoomIn)
        {
            EndPos = new Vector3(Camera.main.transform.position.x, -1, Camera.main.transform.position.z);
            EndZoomPos = 7.5f;
        }
        else
        {
            EndPos = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
            EndZoomPos = 9.5f;
        }
        StartZoom(zoomTime);
    }

    public void SetCameraPosition(Vector3 CamPos)
    {
        Camera.main.transform.position = CamPos;
    }

    public void StartZoom(float curSpeed)
    {
        zoomTime = curSpeed;
        lerpTime = curSpeed;
        zooming = true;        
    }

    public void EndZoom()
    {
        curZoomTime = 0.0f;
        zooming = false;
    }

    public void StartLerp(float curSpeed)
    {
        //StartPos = Camera.GetComponent<Transform>();
        ////set endposition depending on gamestate
        //EndPos = CamTransforms[(int)camEndPos];
        lerpTime = curSpeed;
        lerping = true;
    }

    public void EndLerp()
    {
        //setting curtime back to zero
        curLerpTime = 0.0f;
        //ending lerp
        lerping = false;
    }

    public IEnumerator cShake(float duration, float magnitude)
    {
        print("start camshake");
        Vector3 originalPos = Camera.main.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.position = new Vector3(x+ originalPos.x, y+ originalPos.y, originalPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;      //waits for the next frame before continuing while loop
        }

        print("end camshake");
        Camera.main.transform.position = originalPos;
    }
}