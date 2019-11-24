using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Home : MonsterManager
{
    public GameObject[] Lock;
    public GameObject[] Plus;
    public Transform EggSpawn;

    public void Start()
    {
        print("home mm has been started");
        GetGameManager();
        GM.homeMonsterManager = this;
        //SetSlotSymbol();
    }

    public void SetSlotSymbol()
    {
        for (int i = 0; i < 3; i++)
        {
            if (GM.CurMonsters[i].Monster == null && !GM.CurMonsters[i].Unlocked)
            {
                Lock[i].SetActive(true);
                Plus[i].SetActive(false);
            }
            else if (GM.CurMonsters[i].Monster == null && GM.CurMonsters[i].Unlocked)
            {
                Plus[i].SetActive(true);
                Lock[i].SetActive(false);
            }
            else
            {
                Plus[i].SetActive(false);
                Lock[i].SetActive(false);
            }
        }
    }

    //For the egg hatching
    public void SetEggRarity()
    {
        GM.CurMonsters[(int)GM.curMonsterSlot].Rarity = (eRarity)Random.Range(0, 3);

        switch (GM.CurMonsters[(int)GM.curMonsterSlot].Rarity)
        {
            case eRarity.normal:
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.LevelThreshold_normal;
                //SetText((int)eTextfields.Hatch_Rarity, "Normal");
                break;
            case eRarity.rare:
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.LevelThreshold_rare;
                //SetText((int)eTextfields.Hatch_Rarity, "Rare!");

                break;
            case eRarity.legendary:
                GM.CurMonsters[(int)GM.curMonsterSlot].LevelThreshold_current = GM.CurMonsters[(int)GM.curMonsterSlot].Monster.LevelThreshold_legendary;
                //SetText((int)eTextfields.Hatch_Rarity, "Legendary!");

                break;
            default:
                break;
        }
        print(GM.CurMonsters[(int)GM.curMonsterSlot].Rarity);
    }

    public IEnumerator cSpawnEgg()
    {
        //effect for spawning
        //yield return new WaitForSeconds(0.5f);
        CurMonster.MonsterStage = eMonsterStage.Egg;
        SpawnAnyMonster(CurMonster.Monster.EggPrefab, EggSpawn);
        yield return null;
    }

    public IEnumerator cHatchEgg(Transform monsterSpawn)
    {
        CurMonster.MonsterStage = eMonsterStage.Baby;
        //play effect for level up            
        Destroy(monsterBody[SlotID], 0.25f);
        yield return new WaitForSeconds(0.3f);
        GM.homeMonsterManager.SpawnCurrentMonster(monsterSpawn);
        yield return new WaitForSeconds(0.3f);
        GM.homeUI.SetUIStage(HomeUI.eHomeUIScene.Home);
        GM.homeMonsterManager.SpawnCurrentMonster(MonsterSpawn[SlotID]);
        yield return null;
    }
}
