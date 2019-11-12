using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot holding the current monster
/// </summary>
public class MonsterSlot : MonoBehaviour
{
    [HideInInspector]
    public int SlotID;

    //position to spawn the creature at
    public Transform MonsterSpawn;

    //Scriptable Objekt der Creature einlesen
    public Monster CurMonster;

    //has player unlocked this slot?
    public bool Unlocked;

    public eMonsterStage MonsterStage;
    [Tooltip("Rarity rank of the creature")]
    public eRarity Rarity;

    //current values, starting with zero/base
    public int CreatureLevel = 0;
    public float CreatureXP = 0;
    public float CreatureValue = 0;
    
    public void SpawnEgg()
    {
        GameObject egg = Instantiate(CurMonster.CreaturePrefabs[0], MonsterSpawn);
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
