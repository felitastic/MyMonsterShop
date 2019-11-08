using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    /*
     * Monster level (steigert stage & value)
     * Monster XP (steigert level)
     * Monster value (steigt durch XP)
     * Monster stage enum (alle 3 lvl next stage) == level / 3; max level = 9
     * Rarity enum 
     * Animator, Rigidbody, Material
     */

    //Does not change
    public float[] LevelThreshold = new float[9];
    public Animator anim;
    public Rigidbody rigid;
    public Material material;

    //Does change
    public int CreatureLevel;
    public float CreatureXP;
    public float CreatureValue;
    public eMonsterStage MonsterStage;
    public eRarity Rarity;

    
    public void SetGrowthStage()
    {
        
    }

    public void SetRarity()
    {

    }
}
