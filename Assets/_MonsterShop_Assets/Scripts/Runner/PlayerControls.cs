using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Drag n Drop")]
    public Camera Cam;
    public Rigidbody Monster;
    //{ get { return GetComponentInChildren<Rigidbody>(GM.runnerMonsterManager.monsterBody[(int)GM.curMonsterSlot]); } }

    private GameManager GM;
    private float verticalSpeed { get { return GM.runnerController.RunnerValues.VerticalSpeed * GM.runnerController.curSpeedModifier; } }
    private float horizontalSpeed { get { return GM.runnerController.RunnerValues.HorizontalSpeed; } }

    private Vector2 monsterSpeed;

    //[SerializeField] 
    private bool pointerdown;
    //[SerializeField] 
    private float horizontalDirection;

    public void Start()
    {
        GM = GameManager.Instance;
        monsterSpeed = new Vector2(0f, 0f);
    }
    
    private void Update()
    {
        if (GM.runnerController.IsRunning)
        {
            Cam.transform.position = new Vector3(0f, Monster.transform.position.y + 6.0f, -10f);

            if (!pointerdown)
            {
                monsterSpeed = new Vector2(0f, verticalSpeed);
            }
            else
            {
                monsterSpeed = new Vector2(horizontalSpeed * horizontalDirection, verticalSpeed);
            }
        }
        else
        {
            pointerdown = false;
            //monsterSpeed = new Vector2(0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (Monster != null)
            Monster.velocity = monsterSpeed;
    }

    public void PointerEnter(bool left)
    {
        if (GM.runnerController.IsRunning)
        {
            pointerdown = true;

        if (left)
            horizontalDirection = -1;
        else
            horizontalDirection = +1;
        }
    }

    public void PointerExit()
    {
        pointerdown = false;
    }
}
