using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.runnerController.GameEndText.text = "GAME OVER";
            other.gameObject.SetActive(false);
            StartCoroutine(GameManager.Instance.runnerController.GameEnd());
        }
    }
}
