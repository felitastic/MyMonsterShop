using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot holding the current monster
/// </summary>
public class MonsterManager : MonoBehaviour
{
    public GameManager GM;
    public MonsterSlot CurMonster { get { return GM.CurMonsters[SlotID]; } }
    public int CreatureValue { get { return Mathf.RoundToInt(CurMonster.CreatureValue); } }
    public int SlotID { get { return (int)GM.curMonsterSlot; } }

    //currently spawned monster model prefabs
    public GameObject[] monsterBody = new GameObject[3];

    //Pos to spawn creature 
    public Transform[] MonsterSpawn = new Transform[3];
    

    //public Transform EggSpawn;
    //private float WaitForHatch = 0.01f;
    //public float LevelUpWait = 0.25f;

    //put this in UI manager!

    //private float _CreatureValue = 0;
       
    //Scriptable Objekt der Creature einlesen
    //has player unlocked this slot?
    //public bool Unlocked;

    //public float[] LevelThreshold_current = new float[9];

    //public eMonsterStage MonsterStage;
    //[Tooltip("Rarity rank of the creature")]
    //public eRarity Rarity;

    //current values, starting with zero/base
    //public int CreatureLevel = 0;
    //public float CreatureXP = 0;
    
    public void GetGameManager()
    {
        GM = GameManager.Instance;
    }

    public void CalculateMonsterValue()
    {
        CurMonster.CreatureValue = (CurMonster.GoldModificator * CurMonster.CreatureXP) + (float)CurMonster.BaseValue;
    }


    public void SpawnCurrentMonster(Transform monsterSpawn)
    {
        monsterBody[SlotID] = Instantiate(CurMonster.Monster.CreaturePrefabs[(int)CurMonster.Rarity, (int)CurMonster.MonsterStage], monsterSpawn);
        monsterBody[SlotID].transform.SetParent(monsterSpawn);
    }

    public void SpawnAnyMonster(GameObject monster, Transform monsterSpawn)
    {
        monsterBody[SlotID] = Instantiate(monster, monsterSpawn);
        monsterBody[SlotID].transform.SetParent(monsterSpawn);
    }

    public IEnumerator cLevelUpMonster(eMonsterStage newStage, Transform monsterSpawn)
    {
        CurMonster.MonsterStage = newStage;
        print(newStage);

        if (CurMonster.MonsterStage != eMonsterStage.none)
        {
            //play effect for level up            
            Destroy(monsterBody[SlotID], 0.25f);
            yield return new WaitForSeconds(0.25f);
            SpawnCurrentMonster(monsterSpawn);            
            yield return null;
        }
        else
        {
            yield return null;
            //empty slot, monster sold? 
        }
    }



    //private void Start()
    //{
    //        //GM.CurMonsters[SlotID] = this;

    //    if ((int)GM.thisMonster.thisSlot == SlotID)
    //    {
    //        GM.WriteCurrentMonsters();
    //        CalculateValue();
    //        GM.SetGold(0);
    //        if (CurMonster != null)
    //        {
    //            SpawnCurrentMonster(CurMonster.CreaturePrefabs[(int)Rarity, (int)MonsterStage],MonsterSpawn);
    //            GM.homeUI.SetMonsterTexts(SlotID);
    //        }
    //    }
    //    SetSymbol();
    //    //DontDestroyOnLoad(this);
    //}

    //public IEnumerator C_SetStage(eMonsterStage newStage, Transform monsterSpawn)
    //{
    //    MonsterStage = newStage;
    //    print(newStage);
    //    if(MonsterStage == eMonsterStage.Egg)
    //    {
    //        //play effect for egg spawn
    //        yield return new WaitForSeconds(WaitForHatch);
    //        //CurMonster.CreaturePrefabs[(int)MonsterStage].GetComponentInChildren<Material>() = CurMonster.materials[(int)Rarity];
    //        monsterBody = Instantiate(CurMonster.EggPrefab, monsterSpawn);
    //    }
    //    else if (MonsterStage != eMonsterStage.none)
    //    {
    //        //play effect for level up
    //        Destroy(monsterBody, 0.25f);
    //        yield return new WaitForSeconds(WaitforStageChange);
    //        monsterBody = Instantiate(CurMonster.CreaturePrefabs[(int)Rarity, (int)MonsterStage], monsterSpawn);
    //    }
    //    else
    //    {
    //        //empty slot
    //    }
    //    CalculateValue();
    //}
}
