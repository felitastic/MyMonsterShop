using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unchanging base values of the Monsters
/// </summary>
public class CurrentMonster
{
    public Monster CurMonster;
    public bool Unlocked;
    public float[] LevelThreshold_current = new float[9];
    public eMonsterStage MonsterStage;
    public eRarity Rarity;
    public int CreatureLevel;
    public float CreatureXP;
    public int thisSlot;
}
