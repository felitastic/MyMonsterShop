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
    
    [SerializeField] private bool pointerdown;
    [SerializeField] private float horizontalDirection;

    public void Start()
    {
        //newPosX = Monster.transform.position.x;
    }
    private void Update()
    {
        Cam.transform.position = new Vector3(0f, Monster.transform.position.y + 5.0f, -10f);

        if (!ointerdown)
        {
            Monster.velocity = new Vector2(0f, verticalSpeed);
        }
        else
        {
            Monster.velocity = new Vector2(horizontalSpeed * horizontalDirection, verticalSpeed);
        }
    }

    public void OnPointerDown(bool left)
    {
        pointerdown = true;

        if (left)
            horizontalDirection = -1;
        else
            horizontalDirection = +1;
    }

    public void OnPointerUp()
    {
        pointerdown = false;
    }

    public void UpdateCollectedCount(int value)
    {
        CollectedCount += value;
        CollectedText.text = "" + CollectedCount;
    }
}
