using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterSlot
{
    public int SlotID;
    public Monster Monster = null;
    public bool Unlocked = false;
    public bool Sold = false;
    public int UnlockPrice;
    public float[] LevelThreshold_current = new float[6];
    public float XPCap;
    public eMonsterStage MonsterStage = eMonsterStage.none;
    public eRarity Rarity;
    public int MonsterLevel = 0;
    public float MonsterXP = 0;
    public float MonsterValue = 0;
    public float GoldModificator;
    public int BaseValue;
    [Tooltip("If true, pet counter is running, no pet button")]
    public bool IsHappy;
    public DateTime PetTimerEnd;
    [Tooltip("If true, minigame counter is running, no playing available")]
    public bool IsTired;
    public DateTime PlayTimerEnd;
    [Tooltip("How many times the monster has been stroked")]
    public int StrokeTimes;
    
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
        XPCap = 0;
        IsHappy = false;
        IsTired = false;
        StrokeTimes = 0;
        PetTimerEnd = System.DateTime.Now;
        PlayTimerEnd = System.DateTime.Now;
    }
}
