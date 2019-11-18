using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot holding the current monster
/// </summary>
public class MonsterSlot : MonoBehaviour
{
    public int SlotID;

    //position to spawn the creature at
    public Transform EggSpawn;
    public Transform MonsterSpawn;

    //Scriptable Objekt der Creature einlesen
    public Monster CurMonster;
    private GameObject monsterBody;
    //has player unlocked this slot?
    public bool Unlocked;

    public eMonsterStage MonsterStage;
    [Tooltip("Rarity rank of the creature")]
    public eRarity Rarity;

    //current values, starting with zero/base
    public int CreatureLevel = 0;
    public float CreatureXP = 0;
    public float CreatureValue = 0;

    private float WaitForHatch = 0.01f;
    private float WaitforStageChange = 0.25f;

    private void Start()
    {
        GameManager.Instance.CurMonsters[SlotID] = this;
    }

    public IEnumerator C_SetStage(eMonsterStage newStage)
    {
        MonsterStage = newStage;
        if(MonsterStage == eMonsterStage.egg)
        {
            //play effect for egg spawn
            yield return new WaitForSeconds(WaitForHatch);
            //CurMonster.CreaturePrefabs[(int)MonsterStage].GetComponentInChildren<Material>() = CurMonster.materials[(int)Rarity];
            monsterBody = Instantiate(CurMonster.MonsterEgg, EggSpawn);
        }
        else if (MonsterStage != eMonsterStage.none)
        {
            //play effect for level up
            Destroy(monsterBody, 0.25f);
            yield return new WaitForSeconds(WaitforStageChange);
            monsterBody = Instantiate(CurMonster.CreaturePrefabs[(int)Rarity, (int)MonsterStage], MonsterSpawn);
        }
        else
        {
            //empty slot
        }        
    }

    public void GetXP()
    {

    }

    public void Grow()
    {

    }

    public void Feed()
    {

    }

    //Resets all values, add cur value to player gold
    public void Sell()
    {

    }
}
