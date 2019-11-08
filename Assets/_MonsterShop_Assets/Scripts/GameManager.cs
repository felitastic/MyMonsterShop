using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public UIController curUI;
    public int MinigamesPlayed;
    public eScene CurScene;

    public List<Creature> CurCreatures = new List<Creature>(3);



    void Start()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScene(eScene curScene)
    {
        CurScene = curScene;

        switch (CurScene)
        {
            case eScene.home:

                break;
            case eScene.shop:
                break;
            case eScene.minigames:
                break;
            case eScene.endlessRunner:
                break;
            default:
                break;
        }
    }
}
