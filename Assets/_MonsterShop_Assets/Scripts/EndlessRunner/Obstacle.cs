using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RunnerController.inst.GameEndText.text = "GAME OVER";
            other.gameObject.SetActive(false);
            StartCoroutine(RunnerController.inst.GameEnd());
        }
    }
}
