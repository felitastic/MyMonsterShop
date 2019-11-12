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


    private void Start()
    {
        GM = GameManager.inst;
        GM.HomeCam = this;
        SetScreen(eCurHomeScreen.middle);
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
        if (GM.curHomeScreen != eCurHomeScreen.left && !lerping)
        {
            SetScreen(GM.curHomeScreen -= 1);
        }
    }

    public void PressRightButton()
    {
        if (GM.curHomeScreen != eCurHomeScreen.right && !lerping)
        {
            SetScreen(GM.curHomeScreen += 1);            
        }
    }

    public void SetScreen(eCurHomeScreen curScreen)
    {
        GM.curHomeScreen = curScreen;
        StartPos = Camera.main.transform.position;

        switch (GM.curHomeScreen)
        {
            case eCurHomeScreen.left:
                EndPos = new Vector3(-CamXMovement, 0, -10);

                break;
            case eCurHomeScreen.middle:
                EndPos = new Vector3(0, 0, -10);

                break;
            case eCurHomeScreen.right:
                EndPos = new Vector3(CamXMovement, 0, -10);

                break;
            default:
                break;
        }
        StartLerp(lerpTime);
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