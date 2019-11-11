using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class _Test : MonoBehaviour
    {
        [SerializeField]
        enum curScreen { left, middle, right };
        [SerializeField]
        curScreen screen;
        public float CamXMovement = 10.501f;

        //Time for the whole movement, the higher, the slower the object moves
        public float lerpTime = 3f;
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
            screen = curScreen.middle;
            SetScreen();
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
            if (screen != curScreen.left && !lerping)
            {
                screen -= 1;
                SetScreen();
            }
        }

        public void PressRightButton()
        {
            if (screen != curScreen.right && !lerping)
            {
                screen += 1;
                SetScreen();
            }
        }

        public void SetScreen()
        {
            StartPos = Camera.main.transform.position;

            switch (screen)
            {
                case curScreen.left:
                    EndPos = new Vector3(-CamXMovement, 0, -10);

                    break;
                case curScreen.middle:
                    EndPos = new Vector3(0, 0, -10);

                    break;
                case curScreen.right:
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
}

