using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot holding the current monster
/// </summary>
public class MonsterManager : MonoBehaviour
{
    public int SlotID;
    public GameManager GM;
    //position to spawn the creature at
    public Transform EggSpawn;
    public Transform MonsterSpawn;
    private float WaitForHatch = 0.01f;
    private float WaitforStageChange = 0.25f;

    //put this in UI manager!
    public GameObject Lock;
    public GameObject Plus;
    public int CreatureValue { get { return Mathf.RoundToInt(CurMonster.CreatureValue); } }
    //private float _CreatureValue = 0;
       
    //Scriptable Objekt der Creature einlesen
    public MonsterSlot CurMonster { get { return GM.CurMonsters[SlotID]; } }
    private GameObject monsterBody;
    //has player unlocked this slot?
    public bool Unlocked;

    public float[] LevelThreshold_current = new float[9];

    public eMonsterStage MonsterStage;
    [Tooltip("Rarity rank of the creature")]
    //public eRarity Rarity;

    //current values, starting with zero/base
    public int CreatureLevel = 0;
    public float CreatureXP = 0;

    private void Start()
    {
        GM = GameManager.Instance;
            //GM.CurMonsters[SlotID] = this;

        if ((int)GM.thisMonster.thisSlot == SlotID)
        {
            GM.WriteCurrentMonsters();
            CalculateValue();
            GM.SetGold(0);
            if (CurMonster != null)
            {
                SpawnCurrentMonster(CurMonster.CreaturePrefabs[(int)Rarity, (int)MonsterStage],MonsterSpawn);
                GM.homeUI.SetMonsterTexts(SlotID);
            }
        }
        SetSymbol();
        //DontDestroyOnLoad(this);
    }

    public void CalculateValue()
    {
        _CreatureValue = (CurMonster.Modificator * CreatureXP) + (float)CurMonster.BaseValue;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetSymbol()
    {        
        if (null == CurMonster)
        {
            if (!Unlocked)
            {
                Plus.SetActive(false);
                Lock.SetActive(true);
            }
            else
            {
                Lock.SetActive(false);
                Plus.SetActive(true);

            }
        }
        else
        {
            Lock.SetActive(false);
            Plus.SetActive(false);
        }
    }

    public IEnumerator C_SetStage(eMonsterStage newStage, Transform monsterSpawn)
    {
        MonsterStage = newStage;
        print(newStage);
        if(MonsterStage == eMonsterStage.Egg)
        {
            //play effect for egg spawn
            yield return new WaitForSeconds(WaitForHatch);
            //CurMonster.CreaturePrefabs[(int)MonsterStage].GetComponentInChildren<Material>() = CurMonster.materials[(int)Rarity];
            monsterBody = Instantiate(CurMonster.EggPrefab, monsterSpawn);
        }
        else if (MonsterStage != eMonsterStage.none)
        {
            //play effect for level up
            Destroy(monsterBody, 0.25f);
            yield return new WaitForSeconds(WaitforStageChange);
            monsterBody = Instantiate(CurMonster.CreaturePrefabs[(int)Rarity, (int)MonsterStage], monsterSpawn);
        }
        else
        {
            //empty slot
        }
        CalculateValue();
    }

    public void SpawnCurrentMonster(GameObject monster, Transform monsterSpawn)
    {
        monsterBody = Instantiate(monster, monsterSpawn);
    }

    public void GetXP()
    {

    }

    public void Grow()
    {

    }

    public void Feed()
    {

    }

    //Resets all values, add cur value to player gold
    public void Sell()
    {

    }
}
