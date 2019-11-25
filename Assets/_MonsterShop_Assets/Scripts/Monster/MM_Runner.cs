using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Runner : MonsterManager
{
    public Transform IngameMonsterSpawn;
    public Transform ResultMonsterSpawn;
    //Rigidbody Monster { get { return GetComponentInChildren<Rigidbody>(GM.runnerMonsterManager.monsterBody[(int)GM.curMonsterSlot]); } }

    public void Start()
    {
        GetGameManager();
        GM.runnerMonsterManager = this;
        SpawnMonsterinRunner();
    }

    public void SpawnMonsterinRunner()
    {
        SpawnCurrentMonster(IngameMonsterSpawn);
        //change local scale to fit
        //change rotation
        //monsterBody[(int)GM.curMonsterSlot].transform.localScale = 
        GM.runnerController.playerControls.Monster = GetComponentInChildren<Rigidbody>(GM.runnerMonsterManager.monsterBody[(int)GM.curMonsterSlot]);
        //enable start button after setup is done
    }
}
