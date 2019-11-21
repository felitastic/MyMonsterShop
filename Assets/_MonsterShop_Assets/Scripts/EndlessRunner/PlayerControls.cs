using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Drag n Drop")]
    public Camera Cam;
    public Rigidbody Monster;

    private GameManager GM;
    private float verticalSpeed { get { return GM.runnerController.RunnerValues.VerticalSpeed * GM.runnerController.curSpeedModifier; } }
    private float horizontalSpeed { get { return GM.runnerController.RunnerValues.HorizontalSpeed; } }

    //[SerializeField] 
    private bool pointerdown;
    //[SerializeField] 
    private float horizontalDirection;

    public void Start()
    {
        GM = GameManager.Instance;
    }
    private void Update()
    {
        if (GM.runnerController.IsRunning)
        {
            Cam.transform.position = new Vector3(0f, Monster.transform.position.y + 6.0f, -10f);

            if (!pointerdown)
            {
                Monster.velocity = new Vector2(0f, verticalSpeed);
            }
            else
            {
                Monster.velocity = new Vector2(horizontalSpeed * horizontalDirection, verticalSpeed);
            }
        }
    }

    public void PointerEnter(bool left)
    {
        pointerdown = true;

        if (left)
            horizontalDirection = -1;
        else
            horizontalDirection = +1;
    }

    public void PointerExit()
    {
        pointerdown = false;
    }

}
