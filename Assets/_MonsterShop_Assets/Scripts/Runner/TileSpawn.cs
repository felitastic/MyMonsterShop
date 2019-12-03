using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public float TimeTilDestroy = 0.75f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.runnerController.InstantiateNextTile(GameManager.Instance.runnerController.curTile + 1);
            GameManager.Instance.runnerController.curTile += 1;
        }
    }
}
