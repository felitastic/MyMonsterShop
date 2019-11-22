using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlot
{
    public Monster CurMonster;
    public bool Unlocked;
    public float[] LevelThreshold_current = new float[9];
    public eMonsterStage MonsterStage;
    public eRarity Rarity;
    public int CreatureLevel;
    public float CreatureXP;
    public float CreatureValue;
    public int thisSlot = 4;
}
