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

    [Header("Values for GD to tweak")]
    [Tooltip("XP threshold for each level of the creature")]
    public float[] LevelThreshold = new float[9];
    [Tooltip("How much the egg costs in shop")]
    public int Cost;

    [Header("--- FOR PROGRAMMERS GENTLE TOUCH ONLY ---")]

    [Tooltip("Name of the creature")]
    public string CreatureName;
    public GameObject MonsterEgg;
    [Tooltip("Creature model prefabs by Rarity and Age")]
    [SerializeField]
    private GameObject[,] m_CreaturePrefabs = new GameObject[3,3];
    public GameObject[,] CreaturePrefabs { get { return m_CreaturePrefabs; } }
    //drag n drop ? not sure if works cause prefab
    private Animator anim;
    private Rigidbody rigid;


    /* ~~~ Code von Monsterlinker ~~~ */

    //public List<Material> ToriiColor;
    //public List<Material> EnemySkin;
    //public List<GameObject> LinkerChar;
    //public List<GameObject> EnemyScript;

    //public eToriiColor curColor;
    //public eEnemySkin curSkin;

    //public void ChangeMaterial(eToriiColor toriiColor, eEnemySkin enemySkin)
    //{
    //    curColor = toriiColor;
    //    curSkin = enemySkin;
    //    RightTorii.material = ToriiColor[(int)toriiColor];
    //    Enemy.material = EnemySkin[(int)enemySkin];
    //    GameObject EnemyLinker = GameObject.Instantiate(LinkerChar[(int)toriiColor]);
    //    if ((int)enemySkin > 0)
    //    {
    //        EnemyScript[(int)enemySkin - 1].SetActive(false);
    //    }
    //    EnemyScript[(int)enemySkin].SetActive(true);
    //}

}
