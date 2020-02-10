using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot holding the current monster
/// </summary>
public class MonsterManager : MonoBehaviour
{
    public GameManager GM;
    public MonsterSlot CurMonster { get { return GM.CurMonsters[SlotID]; } }
    public int SlotID { get { return (int)GM.curMonsterSlot; } }

    //currently spawned monster model prefabs
    public GameObject[] monsterBody = new GameObject[3];
    public Rigidbody[] monsterRigid = new Rigidbody[3];
    public Animator[] monsterAnim = new Animator[3];

    //Pos to spawn creature 
    public Transform[] MonsterSpawn = new Transform[3];
    
    public void GetGameManager()
    {
        GM = GameManager.Instance;
    }

    // calculates the monsters selling value
    public int CalculateMonsterValue()
    {
        CurMonster.MonsterValue = (CurMonster.GoldModificator * CurMonster.MonsterXP) + (float)CurMonster.BaseValue;
        //print("cur monster value: " + CurMonster.MonsterValue);
        return Mathf.RoundToInt(CurMonster.MonsterValue);
    }

    // calculates the monsters level on xp gain
    public bool CheckForMonsterLevelUp()
    {
        if (CurMonster.MonsterLevel < 7)
        {
            // if xp is same or higher as required for levelup
            if (CurMonster.MonsterXP > NextLvlUpAt() || Mathf.Approximately(CurMonster.MonsterXP, NextLvlUpAt()))
            {
                //print("curXp" + CurMonster.MonsterXP + ", LVLup requires: " + NextLvlUpAt());
                CurMonster.MonsterLevel += 1;
                print("level up to " + CurMonster.MonsterLevel);
                return true;
            }
        }
        return false;
    }

    public void DeleteMonsterBody(int slotID)
    {
        Destroy(monsterBody[slotID]);
        monsterBody[slotID] = null;
        monsterAnim[slotID] = null;
        monsterRigid[slotID] = null;
    }

    public void ScaleMonsterBody(float scale)
    {
        foreach(GameObject monster in monsterBody)
        {
            if(monster != null)
                monster.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public bool CheckForStageChange()
    {
        if (CurMonster.MonsterStage != eMonsterStage.Adult)
        {
            if (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel == 4 || GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel == 7)
            {
                CurMonster.MonsterStage += 1;
                return true;
            }
        }
        return false;
    }

    // Changes the monsters xp
    public void SetMonsterXP(float gainValue)
    {
        if (Mathf.Approximately(CurMonster.MonsterXP,CurMonster.XPCap))
        {
            print("xp: " + CurMonster.MonsterXP);
            print("monster has reached max XP: "+CurMonster.XPCap);
        }
        else
        {
            CurMonster.MonsterXP += gainValue;
            //print("xp: " + CurMonster.MonsterXP);
        }
    }

    // at which XP a level up occurs
    public float NextLvlUpAt()
    {
        return (CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1]);
    }

    //returns 3 floats for each xp bar
    public float[] XPBarFillAmount()
    {
        float xpBar1, xpBar2, xpBar3;
        xpBar1 = 0.0f;
        xpBar2 = 0.0f;
        xpBar3 = 0.0f;

        switch (CurMonster.MonsterLevel)
        {
            case 1:
                xpBar1 = CurMonster.MonsterXP / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                break;
            case 2:
                xpBar1 = 1.0f;
                xpBar2 = (CurMonster.MonsterXP - CurMonster.MonsterLevel) / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                break;
            case 3:
                xpBar1 = 1.0f;
                xpBar2 = 1.0f;
                if (Mathf.Approximately(CurMonster.MonsterXP, CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1]))
                {
                    xpBar3 = 1.0f;
                }
                else
                {
                    xpBar3 = (CurMonster.MonsterXP - CurMonster.MonsterLevel) / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                }
                break;
            case 4:
                xpBar1 = (CurMonster.MonsterXP - CurMonster.MonsterLevel) / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                break;
            case 5:
                xpBar1 = 1.0f;
                xpBar2 = (CurMonster.MonsterXP - CurMonster.MonsterLevel) / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                break;
            case 6:
                xpBar1 = 1.0f;
                xpBar2 = 1.0f;
                xpBar3 = (CurMonster.MonsterXP - CurMonster.MonsterLevel) / CurMonster.LevelThreshold_current[CurMonster.MonsterLevel - 1];
                break;
            default:
                break;
        }

        return new float[] { xpBar1, xpBar2, xpBar3 };
    }


    public int RoundedXPValue()
    {
        return Mathf.RoundToInt(CurMonster.MonsterXP);
    }

    public void SpawnCurrentMonster(Transform monsterSpawn)
    {
        GameObject newMonster = Instantiate(CurMonster.Monster.MonsterPrefabs[(int)CurMonster.Rarity, (int)CurMonster.MonsterStage], monsterSpawn);
        newMonster.transform.SetParent(monsterSpawn);
        monsterBody[SlotID] = newMonster;
        monsterAnim[SlotID] = monsterBody[SlotID].GetComponentInChildren<Animator>();
        monsterRigid[SlotID] = monsterBody[SlotID].GetComponentInChildren<Rigidbody>();
    }

    public void SpawnAnyMonster(GameObject monster, Transform monsterSpawn, int slotID)
    {
        monsterBody[slotID] = Instantiate(monster, monsterSpawn);
        monsterBody[slotID].transform.SetParent(monsterSpawn);
        monsterAnim[slotID] = monsterBody[slotID].GetComponentInChildren<Animator>();
        monsterRigid[slotID] = monsterBody[slotID].GetComponentInChildren<Rigidbody>();
    }

    public IEnumerator cLevelUpMonster(eMonsterStage newStage, Transform monsterSpawn)
    {
        CurMonster.MonsterStage = newStage;
        //print(newStage);

        //play effect for level up            
        if (CurMonster.MonsterStage != eMonsterStage.none)
        {
            switch (GM.curScreen)
            {
                case eScene.home:
                    GM.homeUI.LevelUpScreen.SetTrigger("glow");

                    break;
                case eScene.runner:
                    //TODO runner ui animator glow thing setzen GM.runnerUI.LevelUpScreen.SetTrigger("glow");
                    GM.runnerUI.LevelUpScreen.SetTrigger("glow");

                    break;
                case eScene.slidingpicture:
                    //TODO: put monster manager of sliding picture game here
                    break;
                default:
                    print("cant find vfx script cause I dont know which scene we're in");
                    break;
            }
        
            yield return new WaitForSeconds(0.4f);
            Destroy(monsterBody[SlotID]);
            SpawnCurrentMonster(monsterSpawn);
            yield return new WaitForSeconds(0.5f);
            switch (GM.curScreen)
            {
                case eScene.home:
                    StartCoroutine(GM.HomeCam.cShake(0.07f, 0.2f));
                    GM.vfx_home.SpawnEffektAtPosition(VFX_Home.VFX.Confetti, monsterSpawn.transform.position);

                    break;
                case eScene.runner:
                    StartCoroutine(GameManager.Instance.runnerController.cShake(0.07f, 0.25f));
                    GM.vfx_runner.SpawnEffektAtPosition(VFX_Runner.VFX.Confetti, monsterSpawn.transform.position);

                    break;
                case eScene.slidingpicture:
                    //TODO: put monster manager of sliding picture game here
                    break;
                default:
                    print("cant find vfx script cause I dont know which scene we're in");
                    break;
            }
            yield return new WaitForSeconds(4.0f);
            yield return null;
        }
        else
        {
            Destroy(monsterBody[SlotID]);
            yield return null;
            //empty slot, monster sold? 
        }
    }
}
