using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private GameManager GM;
    private bool counting;

    void Start()
    {
        GM = GameManager.Instance;
    }

    void Update()
    {
        if (GM.CurMonsters[(int)GM.curMonsterSlot] != null)
        {
            if (!GM.CurMonsters[(int)GM.curMonsterSlot].MonsterSad)
            {
                StartCoroutine(cPettingTimer());
            }
        }
    }

    private IEnumerator cPettingTimer()
    {
        yield return new WaitForSeconds(GM.TimeTilPetting);
        GM.CurMonsters[(int)GM.curMonsterSlot].MonsterSad = true;
        GM.homeUI.SetPettingSymbol(true);
    }
}
