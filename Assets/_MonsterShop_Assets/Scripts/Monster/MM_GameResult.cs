using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_GameResult : MonsterManager
{
    public new void Start()
    {
        print("nicht vererbt: set slot symbole");
        GM.gameResultMonsterManager = this;
    }


}
