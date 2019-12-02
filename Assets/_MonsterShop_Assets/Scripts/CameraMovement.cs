using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameManager GM;

    public float CamXMovement = 10.501f;

    //Time for the whole movement, the higher, the slower the object moves
    public float lerpTime = 0.35f;
    //Is set to the current time as long as the lerp is running
    float curLerpTime;
    //To start the lerp
    public bool lerping;
    //Quaternion StartQuaternion;
    Vector3 StartPos;
    //Quaternion EndQuaternion;
    Vector3 EndPos;
    //public List<GameObject> CamTransforms;
    //public GameObject CamHolder;

    public void Awake()
    {
        GM = GameManager.Instance;
        GM.HomeCam = this;                
    }

    private void Start()
    {
        SetCameraPosition(GM.CurCamHomePos);
    }

    public void Update()
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
    }

    public void PressLeftButton()
    {
        if (GM.curMonsterSlot != ecurMonsterSlot.left && !lerping)
        {
            SetScreen(GM.curMonsterSlot -= 1);
            GM.homeUI.SetMonsterTexts();
            GM.homeUI.SetMonsterValue();
            GM.homeUI.SetXPBarUndLevel();
        }
    }

    public void PressRightButton()
    {
        if (GM.curMonsterSlot != ecurMonsterSlot.right && !lerping)
        {
            SetScreen(GM.curMonsterSlot += 1);
            GM.homeUI.SetMonsterTexts();
            GM.homeUI.SetMonsterValue();
            GM.homeUI.SetXPBarUndLevel();
        }
    }

    public void SetScreen(ecurMonsterSlot curScreen)
    {
        GM.curMonsterSlot = curScreen;
        StartPos = Camera.main.transform.position;

        switch (GM.curMonsterSlot)
        {
            case ecurMonsterSlot.left:
                EndPos = new Vector3(-CamXMovement, 0, -10);

                break;
            case ecurMonsterSlot.middle:
                EndPos = new Vector3(0, 0, -10);

                break;
            case ecurMonsterSlot.right:
                EndPos = new Vector3(CamXMovement, 0, -10);

                break;
            default:
                break;
        }

        GM.CurCamHomePos = EndPos;
        StartLerp(lerpTime);
    }

    public void SetCameraPosition(Vector3 CamPos)
    {
        Camera.main.transform.position = CamPos;
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

    //public void SetCamPosition(eCamPosition camEndPos)
    //{
    //    CamHolder.transform.position = CamTransforms[(int)camEndPos].transform.position;
    //    CamHolder.transform.rotation = CamTransforms[(int)camEndPos].transform.rotation;
    //}

    //public void SetPositions(eCamPosition camEndPos)
    //{
    //    StartPos = CamHolder.transform.position;
    //    StartQuaternion = CamHolder.transform.rotation;
    //    EndPos = CamTransforms[(int)camEndPos].transform.position;
    //    EndQuaternion = CamTransforms[(int)camEndPos].transform.rotation;
    //}


}