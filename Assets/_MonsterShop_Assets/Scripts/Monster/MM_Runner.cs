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
        //StartCoroutine(cSpawnMonsterinRunner());
    }

    public IEnumerator cSpawnMonsterinRunner()
    {        
        SpawnCurrentMonster(IngameMonsterSpawn);
        GM.runnerController.playerControls.Monster = GM.runnerMonsterManager.monsterRigid[SlotID];
        GM.runnerController.Cam.transform.position =
            new Vector3(0f, GM.runnerMonsterManager.IngameMonsterSpawn.transform.position.y + 6.0f, -10f);
        yield return new WaitForSeconds(0.75f);
        GM.vfx_runner.SpawnEffektAtObject(VFX_Runner.VFX.Runner_Run, monsterBody[SlotID]);
        GM.runnerController.IsRunning = true;        
    }       

}
