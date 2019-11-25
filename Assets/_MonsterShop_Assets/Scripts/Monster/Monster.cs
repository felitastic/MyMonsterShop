using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Unchanging base values of the Monsters
/// </summary>
[CreateAssetMenu(fileName = "newMonster", menuName = "MonsterShop/Monster", order = 1)]
public class Monster : SerializedScriptableObject
{
    /*
 * Monster level (steigert stage & value)
 * Monster XP (steigert level)
 * Monster value (steigt durch XP)
 * Monster stage enum (alle 3 lvl next stage) == level / 3; max level = 9
 * Rarity enum 
 * Animator, Rigidbody, Material
 */

    [Header("--- VALUES FOR GD TO TWEAK ---")]
    [Tooltip("XP threshold for normal creature")]
    public float[] LevelThreshold_normal = new float[9];

    [Tooltip("Multiplicator for rare Levelthresholds ")]
    public float MultiplicatorRare;
    [Tooltip("Multiplicator for legendary Levelthresholds ")]
    public float MultiplicatorLegendary;

    [Tooltip("How much the egg BaseCosts in shop")]
    public int BaseCost;
    [Tooltip("Value modificator: gold = modificator * xp + base value")]
    public float GoldModificator;
    [Tooltip("gold = modificator * xp + base value")]
    public int BaseValue;

    [Header("--- FOR PROGRAMMERS GENTLE TOUCH ONLY ---")]
    public eMonsterType MonsterType;
    [Tooltip("Name of the creature")]
    public string CreatureName;
    [Tooltip("Sprite to use in Shop?")]
    public Sprite EggSprite;
    public GameObject EggPrefab;
    [Tooltip("Creature model prefabs by Rarity and Age")]
    [SerializeField]
    private GameObject[,] m_CreaturePrefabs = new GameObject[3, 3];
    public GameObject[,] CreaturePrefabs { get { return m_CreaturePrefabs; } }
    //drag n drop ? not sure if works cause prefab
    private Animator anim;
    private Rigidbody rigid;
}
