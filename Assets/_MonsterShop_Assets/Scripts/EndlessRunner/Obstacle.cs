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
            StartCoroutine(ReloadScene(other.gameObject));
        }
    }

    public IEnumerator ReloadScene(GameObject monster)
    {
        yield return new WaitForSeconds(0.5f);
        monster.transform.position = new Vector2(0f, 0f);
        GameOver.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        monster.SetActive(true);
    }
}
