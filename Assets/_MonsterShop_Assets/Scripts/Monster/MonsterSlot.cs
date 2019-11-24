using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlot
{
    public int SlotID = 4;
    public Monster Monster = null;
    public bool Unlocked = false;
    public float[] LevelThreshold_current = new float[9];
    public eMonsterStage MonsterStage = eMonsterStage.none;
    public eRarity Rarity;
    public int CreatureLevel = 0;
    public float CreatureXP = 0;
    public float CreatureValue = 0;
    public float GoldModificator;
    public int BaseValue;
}
