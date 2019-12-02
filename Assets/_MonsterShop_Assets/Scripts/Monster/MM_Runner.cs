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
        StartCoroutine(cSpawnMonsterinRunner());
    }

    public IEnumerator cSpawnMonsterinRunner()
    {        
        //change local scale and rotation to fit
        SpawnCurrentMonster(IngameMonsterSpawn);
               
        //while ( == null)
        //    yield return null;

        GM.runnerController.playerControls.Monster = GM.runnerMonsterManager.monsterRigid[SlotID];
        //enable start button after setup is done
        yield return new WaitForSeconds(0.5f);
        GM.runnerUI.EnableStartButton();
    }
}
