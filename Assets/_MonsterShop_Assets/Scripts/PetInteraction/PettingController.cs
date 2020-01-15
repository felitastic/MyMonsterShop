using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PettingController : MonoBehaviour
{
    //regelt ansprache des animators etc
    public MonsterManager MM;
    public Image HeartMeter;

    public Image[] XPBars = new Image[3];
    [SerializeField]
    private GameManager GM;
    [SerializeField]
    private float monsterStroked;
    private bool strokeDelay;
    private float petTime = 0.15f;

    private void Start()
    {
        GM = GameManager.Instance;
        MM = GM.homeMonsterManager;
        monsterStroked = 0;
        HeartMeter.fillAmount = 0;
    }

    public virtual void SetXPBars()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterLevel < 7)
        {
            //print("cur XP: " + GM.CurMonsters[(int)GM.curMonsterSlot].MonsterXP);
            float[] fillAmount = new float[3];

            switch (GM.curScreen)
            {
                case eScene.home:
                    fillAmount = GM.homeMonsterManager.XPBarFillAmount();
                    break;
                case eScene.runner:
                    fillAmount = GM.runnerMonsterManager.XPBarFillAmount();
                    break;
                case eScene.slidingpicture:
                    //TODO: put monster manager of sliding picture game here
                    break;
                default:
                    print("cant find monster manager cause I dont know which scene we're in");
                    break;
            }
            XPBars[0].fillAmount = fillAmount[0];
            XPBars[1].fillAmount = fillAmount[1];
            XPBars[2].fillAmount = fillAmount[2];
        }
    }

    public IEnumerator cEndPetSession()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage != eMonsterStage.Adult)
        {
            GM.homeUI.ShowPetSessionResult();

            GM.CurMonsters[(int)GM.curMonsterSlot].StrokeTimes += 1;
            GM.CurMonsters[(int)GM.curMonsterSlot].IsHappy = true;
            print("monster has been stroked " + GM.CurMonsters[(int)GM.curMonsterSlot].StrokeTimes + " times");
            //TODO change monster idle to happy

            yield return new WaitForSeconds(0.25f);

            SetXPBars();

            MM.SetMonsterXP(GM.XPGainPerPettingSession);
            if (GM.CurMonsters[(int)GM.curMonsterSlot].StrokeTimes > 1)
            {
                MM.SetMonsterXP(GM.XPAffectionBonus);
            }

            //the whole shitty check for levelup and call levelup scene
            while (MM.CheckForMonsterLevelUp())
            {
                if (MM.CheckForStageChange())
                {
                    StartCoroutine(MM.cLevelUpMonster(GM.CurMonsters[(int)GM.curMonsterSlot].MonsterStage, MM.MonsterSpawn[(int)GM.curMonsterSlot]));
                    yield return new WaitForSeconds(0.5f);
                }
                SetXPBars();
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            MM.SetMonsterXP(GM.XPGainPerPettingSession);
            if (GM.CurMonsters[(int)GM.curMonsterSlot].StrokeTimes > 1)
            {
                MM.SetMonsterXP(GM.XPAffectionBonus);
            }
        }
        GM.CurMonsters[(int)GM.curMonsterSlot].MonsterSad = false;
        GM.homeUI.ExitPetSession();
    }

    public void PetMonster()
    {
        if (!strokeDelay && monsterStroked < GM.StrokesPerPettingSession)
        {
            print("you have pet the monster! :)");
            monsterStroked += 1;
            HeartMeter.fillAmount = monsterStroked / GM.StrokesPerPettingSession;
            strokeDelay = true;

            if (monsterStroked == GM.StrokesPerPettingSession)
            {
                print("oh yay, monster is happy");
                GM.homeUI.SetPettingSymbol(false);
                StartCoroutine(cEndPetSession());
            }
            //else if (monsterStroked < 0)
            //{
            //    //play monster does not want anim
            //}
        }
        else
        {
            print("wait a moment before stroking again");
        }

        StartCoroutine(cStrokingMonstser());
    }

    private IEnumerator cStrokingMonstser()
    {
        //play animation
        //wait for it to finish
        yield return new WaitForSeconds(petTime);
        strokeDelay = false;
    }

    private void OnDisable()
    {
        monsterStroked = 0;
        HeartMeter.fillAmount = 0;
    }
}
