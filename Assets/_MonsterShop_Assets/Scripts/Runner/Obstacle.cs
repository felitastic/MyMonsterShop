using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.runnerController.IsRunning = false;
            StartCoroutine(GameManager.Instance.runnerController.cShake(0.3f, 0.4f));
            GameManager.Instance.vfx_runner.SpawnEffektAtPosition(VFX_Runner.VFX.Runner_Death, other.gameObject.transform.position);
            GameManager.Instance.runnerController.win = false;
            //other.gameObject.SetActive(false);
            StartCoroutine(GameManager.Instance.runnerController.cGameEnd(other.gameObject));
        }
    }
}
