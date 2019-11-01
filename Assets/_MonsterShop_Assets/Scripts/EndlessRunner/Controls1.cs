using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls1 : MonoBehaviour
{
    public int CollectedCount;
    public Text CollectedText;
    public Camera Cam;
    public Rigidbody Monster;
    public float verticalSpeed = 1f;
    public float horizontalSpeed = 1f;


    public Collider2D leftButton;
    public Collider2D rightButton;

    //private Touch touch;
    //private Vector2 touchPos;

    public void Start()
    {
        //newPosX = Monster.transform.position.x;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Cam.ScreenToWorldPoint(touch.position);
            CheckForTouch(touchPos);
            //switch (touch.phase)
            //{
            //    case TouchPhase.Began:
            //        touchPos = Cam.ScreenToWorldPoint(touch.position);
            //        break;
            //    case TouchPhase.Moved:
            //        Vector2 direction = touch.position - touchPos;
            //        break;
            //    case TouchPhase.Stationary:
            //        if it touches one of the arrow colliders
            //        break;
            //    case TouchPhase.Ended:

            //        break;
            //    case TouchPhase.Canceled:
            //        break;
            //    default:
            //        break;
            //}
        }
        else
        {
            Monster.velocity = new Vector2(0f, verticalSpeed);
        }
        Cam.transform.position = new Vector3(0f, Monster.transform.position.y + 5.0f, -10f);
    }

    void CheckForTouch(Vector2 TouchPos)
    {
        if (leftButton == Physics2D.OverlapPoint(TouchPos))
        {
            Monster.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
        }
        else if (rightButton == Physics2D.OverlapPoint(TouchPos))
        {
            Monster.velocity = new Vector2(horizontalSpeed, verticalSpeed);
        }
        else
        {
            Monster.velocity = new Vector2(0f, verticalSpeed);
        }
    }

    public void UpdateCollectedCount(int value)
    {
        CollectedCount += value;
        CollectedText.text = "" + CollectedCount;
    }
}
