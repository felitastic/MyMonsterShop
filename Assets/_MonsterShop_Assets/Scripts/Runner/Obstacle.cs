using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.vfx_runner.SpawnEffektAtPosition(VFX_Runner.VFX.Runner_Death, other.gameObject.transform.position);
            GameManager.Instance.runnerController.win = false;
            //other.gameObject.SetActive(false);
            StartCoroutine(GameManager.Instance.runnerController.cGameEnd(other.gameObject));
        }
    }
}
