using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public float TimeTilDestroy = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.runnerController.InstantiateNextTile(GameManager.Instance.runnerController.curTile +1);
            //if (RunnerController.inst.curTile == RunnerController.inst.LevelTiles.Length-2)
            //    RunnerController.inst.curTile = 1;
            //else
            GameManager.Instance.runnerController.curTile += 1;
        }
    }
}
