using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public EndlessRunnerVars vars;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vars.GameEndText.text = "GAME OVER";
            other.gameObject.SetActive(false);
            StartCoroutine(vars.GameEnd());
        }
    }
}
