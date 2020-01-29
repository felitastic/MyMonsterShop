using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Home : MonsterManager
{
    public GameObject[] Lock;
    public GameObject[] Plus;
    public Transform EggSpawn;
    public Transform BabySpawn;

    public double RareDrop = 5.0f;
    public double EpicDrop = 1.0f;

    public void Awake()
    {
        GetGameManager();
        GM.homeMonsterManager = this;
        //print("home mm has been started");
    }
    public void Start()
    {
        //SetSlotSymbol();
        SpawnAllCurrentMonsters();
        //CalculateMonsterValue();
    }
    
    // Spawns all currently owned monsters in home scene each start
    public void SpawnAllCurrentMonsters()
    {
        print("spawning monsters");

        for (int i = 0; i< GM.CurMonsters.Length;i++)
        {
            if (GM.CurMonsters[i].Monster == null)
            {
                //print("slot " + i + " is empty");
            }
            else
            {
                //print("slot " + i + " is NOT empty");
                SpawnAnyMonster(GM.CurMonsters[i].Monster.MonsterPrefabs[(int)GM.CurMonsters[i].Rarity, (int)GM.CurMonsters[i].MonsterStage], MonsterSpawn[i], i);
                //print("rarity: " + (int)GM.CurMonsters[i].Rarity);
                //print("stage: " + (int)GM.CurMonsters[i].MonsterStage);
                //print("spawn: " + MonsterSpawn[i].name);
            }
        }
    }

    /// <summary>
    /// Sets rarity of the hatching monster via chance
    /// </summary>
    public void SetEggRarity()
    {
        if (GM.getEpic)
        {
            GM.getEpic = false;
            GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)1;
            GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[1];
            GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current =
                newLevelThreshold(GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MultiplicatorRare);
        }
        else if (GM.getLegendary)
        {
            GM.getLegendary = false;
            GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)2;
            GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[2];
            GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current =
                newLevelThreshold(GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MultiplicatorLegendary);
        }
        else
        {
            float rand = Random.Range(0.0f, 1.01f);
            double percentage = System.Math.Round((double)rand, 2);
            double epic = System.Math.Round((EpicDrop / 100.0), 2);
            double rare = System.Math.Round((RareDrop / 100.0), 2);
            //Debug.Log("random chance: " + percentage+"epic: " + epic+ "rare: " + rare); 

            if (percentage <= epic)
            {
                GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)2;
                GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[2];
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = 
                    newLevelThreshold(GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MultiplicatorLegendary);
            }
            else if (percentage <= rare)
            {
                GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)1;
                GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[1];
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = 
                    newLevelThreshold(GM.CurMonsters[(int)GM.curMonsterSlot].Monster.MultiplicatorRare);
            }
            else
            {
                GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)0;
                GM.CurMonsters[(int)GM.curMonsterSlot].GoldModificator = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.GoldModificator[0];
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = newLevelThreshold();
            }
        }
        //Debug.Log("Rarity: "+GM.CurMonsters[(int)GM.curMonsterSlot].Rarity);
    }

    // calculates level threshold of the monster according to its rarity (with rarity multiplier)
    public float[] newLevelThreshold(float multiplier = 1.00f)
    {
        for (int i = 0; i < GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current.Length; i++)
        {
            GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[i] = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.LevelThreshold_normal[i] * multiplier;
            //print(i+" = "+GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current[i]);
        }
        return GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current;
    }

    // Spawns the egg model in egg hatching scene
    public IEnumerator cSpawnEgg()
    {
        //effect for spawning
        //yield return new WaitForSeconds(0.5f);        
        CurMonster.MonsterStage = eMonsterStage.Egg;
        SpawnAnyMonster(CurMonster.Monster.EggPrefab, EggSpawn, CurMonster.SlotID);
        yield return null;
    }

    // Hatching egg animation
    public IEnumerator cHatchEgg(Transform monsterSpawn)
    {
        yield return new WaitForSeconds(0.05f);
        CurMonster.MonsterStage = eMonsterStage.Baby;
        Destroy(monsterBody[SlotID]);
        GM.vfx_home.SpawnEffect(VFX_Home.VFX.EggShells, VFX_Home.Position.EggHatching);
        StartCoroutine(GM.HomeCam.cShake(0.1f, 0.25f));
        GM.vfx_home.SpawnEffect(VFX_Home.VFX.EggGlow, VFX_Home.Position.EggGlow);
        GM.homeMonsterManager.SpawnCurrentMonster(monsterSpawn);
        yield return new WaitForSeconds(3f);
        Destroy(monsterBody[SlotID], 0.15f);
        GM.homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
        GM.homeMonsterManager.SpawnCurrentMonster(MonsterSpawn[SlotID]);
        GM.homeUI.ShowMonsterStats(true);
        GM.homeUI.TrainButtonActive(true);
        GM.homeMonsterManager.monsterAnim[GM.homeMonsterManager.CurMonster.SlotID].SetBool("isSad", true);
        yield return null;
    }
}
