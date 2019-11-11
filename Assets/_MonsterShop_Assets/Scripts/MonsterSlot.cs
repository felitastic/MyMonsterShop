using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlot : MonoBehaviour
{
    public Transform MonsterSpawn;

    //Scriptable Objekt der Creature einlesen
    public Monster monster;

    public bool Unlocked;

    public int SlotID;
    public eMonsterStage MonsterStage;
    public eRarity Rarity;

    //current values, starting with zero/base
    public int CreatureLevel = 0;
    public float CreatureXP = 0;
    public float CreatureValue = 0;

    public void GetXP()
    {

    }

    public void Grow()
    {

    }

    public void Feed()
    {

    }
}
