using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unchanging base values of the Monsters
/// </summary>
[CreateAssetMenu(fileName = "newMonster", menuName = "MonsterShop/Monster", order = 1)]
public class Monster : ScriptableObject
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

    [Header("--- FOR PROGRAMMERS GENTLE TOUCH ONLY ---")]
    [Tooltip("Creature model prefabs")]
    public GameObject[] CreaturePrefabs;
    [Tooltip("Name of the creature")]
    public string CreatureName;
    [Tooltip("List of materials for different rarities: normal, epic, legendary")]
    public Material[] materials = new Material[3];

    //drag n drop ? not sure if works cause prefab
    public Animator anim;
    public Rigidbody rigid;


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
