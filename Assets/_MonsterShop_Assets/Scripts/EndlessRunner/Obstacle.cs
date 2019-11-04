using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public RunnerController controller;
    public GameObject GameOver;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameOver.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }

}
