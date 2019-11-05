using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public EndlessRunnerVars vars;
    //private int curTile { get { return vars.curTile; } }
    //private GameObject[] LevelTiles { get { return controller.LevelTiles; } }
    public float TimeTilDestroy = 0.5f;
    //public GameObject LevelSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("InstantiateTile"))
            {
                vars.curTile += 1;
                vars.InstantiateNextTile(vars.curTile +1);

            }
            else if (this.gameObject.CompareTag("DestroyTile"))
            {
                Destroy(this.gameObject, TimeTilDestroy);
            }
        }
    }

}
