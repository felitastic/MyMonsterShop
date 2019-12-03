using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlot
{
    public int SlotID;
    public Monster Monster = null;
    public bool Unlocked = false;
    public float[] LevelThreshold_current = new float[9];
    public eMonsterStage MonsterStage = eMonsterStage.none;
    public eRarity Rarity;
    public int MonsterLevel = 0;
    public float MonsterXP = 0;
    public float MonsterValue = 0;
    public float GoldModificator;
    public int BaseValue;    
}
