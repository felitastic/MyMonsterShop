using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlot
{
    public int SlotID;
    public Monster Monster = null;
    public bool Unlocked = false;
    public bool Sold = false;
    public int UnlockPrice;
    public float[] LevelThreshold_current = new float[6];
    public eMonsterStage MonsterStage = eMonsterStage.none;
    public eRarity Rarity;
    public int MonsterLevel = 0;
    public float MonsterXP = 0;
    public float MonsterValue = 0;
    public float GoldModificator;
    public int BaseValue;

    public void ResetValues()
    {
        Monster = null;
        Sold = false;
        LevelThreshold_current = new float[6];
        MonsterStage = eMonsterStage.none;
        Rarity = eRarity.normal;
        MonsterLevel = 0;
        MonsterXP = 0;
        MonsterValue = 0;
        GoldModificator = 0;
        BaseValue = 0;
    }
}
