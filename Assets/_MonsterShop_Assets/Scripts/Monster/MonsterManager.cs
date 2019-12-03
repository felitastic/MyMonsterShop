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
    public int SlotID { get { return (int)GM.curMonsterSlot; } }
    public int MonsterValue { get { return Mathf.RoundToInt(CurMonster.MonsterValue); } }

    //currently spawned monster model prefabs
    public GameObject[] monsterBody = new GameObject[3];
    public Rigidbody[] monsterRigid = new Rigidbody[3];
    public Animator[] monsterAnim = new Animator[3];

    //Pos to spawn creature 
    public Transform[] MonsterSpawn = new Transform[3];

    //public Transform EggSpawn;
    //private float WaitForHatch = 0.01f;
    //public float LevelUpWait = 0.25f;

    //put this in UI manager!

    //private float _MonsterValue = 0;
       
    //Scriptable Objekt der Creature einlesen
    //has player unlocked this slot?
    //public bool Unlocked;

    //public float[] LevelThreshold_current = new float[9];

    //public eMonsterStage MonsterStage;
    //[Tooltip("Rarity rank of the creature")]
    //public eRarity Rarity;

    //current values, starting with zero/base
    //public int MonsterLevel = 0;
    //public float MonsterXP = 0;
    
    public void GetGameManager()
    {
        GM = GameManager.Instance;
    }

    // calculates the monsters selling value
    public void CalculateMonsterValue()
    {
        CurMonster.MonsterValue = (CurMonster.GoldModificator * CurMonster.MonsterXP) + (float)CurMonster.BaseValue;
    }

    // calculates the monsters level on xp gain
    public bool CheckForMonsterLevelUp()
    {
        if (CurMonster.MonsterLevel > 9)
        {
            // if xp is same or higher as required for levelup
            if (CurMonster.MonsterXP > LevelUpXpValue() || Mathf.Approximately(CurMonster.MonsterXP, LevelUpXpValue()))
            {

                CurMonster.MonsterLevel += 1;
                return true;
            }
        }
        return false;        
    }

    public bool CheckForStageChange()
    {
        if (CurMonster.MonsterStage != eMonsterStage.Adult)
        {
            if (CurMonster.MonsterLevel % 3 == 0)
            {
                CurMonster.MonsterStage += 1;
                return true;
            }
        }
        return false;
    }

    // Changes the monsters xp
    public void SetMonsterXP(float gainValue)
    {
        CurMonster.MonsterXP += gainValue;
        print("xp: " + CurMonster.MonsterXP);
    }

    // at which XP a level up occurs
    public float LevelUpXpValue()
    {
        return (CurMonster.LevelThreshold_current[GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel]);
    }

    public int RoundedXPValue()
    {
        return Mathf.RoundToInt(CurMonster.MonsterXP);
    }

    public void SpawnCurrentMonster(Transform monsterSpawn)
    {
        monsterBody[SlotID] = Instantiate(CurMonster.Monster.MonsterPrefabs[(int)CurMonster.Rarity, (int)CurMonster.MonsterStage], monsterSpawn);
        monsterBody[SlotID].transform.SetParent(monsterSpawn);
        monsterAnim[SlotID] = monsterBody[SlotID].GetComponentInChildren<Animator>();
        monsterRigid[SlotID] = monsterBody[SlotID].GetComponentInChildren<Rigidbody>();
    }

    public void SpawnAnyMonster(GameObject monster, Transform monsterSpawn)
    {
        monsterBody[SlotID] = Instantiate(monster, monsterSpawn);
        monsterBody[SlotID].transform.SetParent(monsterSpawn);
        monsterAnim[SlotID] = monsterBody[SlotID].GetComponentInChildren<Animator>();
        monsterRigid[SlotID] = monsterBody[SlotID].GetComponentInChildren<Rigidbody>();
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
}
