using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Home : MonsterManager
{
    public Material[] PumpkinEgg;
    public Material[] MimikEgg;

    public GameObject[] Lock;
    public GameObject[] Plus;
    public Transform EggSpawn;
    public Transform BabySpawn;

    private Renderer eggRenderer;


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

    //Egg cracks with each tap
    public void CrackEgg(int taps)
    {
        eggRenderer = monsterBody[CurMonster.SlotID].GetComponentInChildren<Renderer>();

        switch (GM.CurMonsters[CurMonster.SlotID].Monster.MonsterType)
        {
            case eMonsterType.pumpkin:
                eggRenderer.material = PumpkinEgg[taps - 1];
                break;
            case eMonsterType.mimik:
                eggRenderer.material = MimikEgg[taps - 1];
                break;
            case eMonsterType.sphere:
                break;
            case eMonsterType.cube:
                break;
            case eMonsterType.capsule:
                break;
            default:
                break;
        }
    }

     // Hatching egg animation
public IEnumerator cHatchEgg(Transform monsterSpawn)
    {
        yield return new WaitForSeconds(0.05f);
        CurMonster.MonsterStage = eMonsterStage.Baby;
        Destroy(monsterBody[SlotID]);
        StartCoroutine(GM.HomeCam.cShake(0.1f, 0.25f));
        GM.vfx_home.SpawnEffect(VFX_Home.VFX.EggShells, VFX_Home.Position.EggHatching);
        GM.homeUI.EnableEggGlow(true);
        GM.homeMonsterManager.SpawnCurrentMonster(monsterSpawn);
        yield return new WaitForSeconds(0.15f);
        GM.homeMonsterManager.monsterAnim[GM.curMonsterID].SetTrigger("hatch");
        yield return new WaitForSeconds(1.0f);

        print("checking rarity and showing banner");
        switch (GM.CurMonsters[GM.curMonsterID].Rarity)
        {
            case eRarity.normal:
                yield return new WaitForSeconds(2.5f);
                
                break;
            case eRarity.rare:
                GM.vfx_home.SpawnEffect(VFX_Home.VFX.EpicBanner,VFX_Home.Position.RarityBanner);
                yield return new WaitForSeconds(3.5f);

                break;
            case eRarity.legendary:
                GM.vfx_home.SpawnEffect(VFX_Home.VFX.LegendaryBanner, VFX_Home.Position.RarityBanner);
                yield return new WaitForSeconds(3.5f);

                break;
            default:
                break;
        }
        print("back to home with da beby");
        Destroy(monsterBody[SlotID], 0.15f);
        GM.homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
        GM.homeMonsterManager.SpawnCurrentMonster(MonsterSpawn[SlotID]);
        GM.homeUI.ShowMonsterStats(true);
        GM.homeUI.TrainButtonActive(true);
        GM.homeMonsterManager.monsterAnim[GM.homeMonsterManager.CurMonster.SlotID].SetBool("isSad", true);
        GM.homeUI.EnableEggGlow(false);
        yield return null;
    }
}
