using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    //ui controller schreibt sich bei onenable/awake hier rein
    public UIController CurUI;

    public CameraMovement HomeCam;
    
    public int CurPlayerID;

    public int CurCreatureSlot;

    public int PlayerMoney;

    //wie oft spieler schon minigames gespielt hat (für unlocks)
    public int MinigamesPlayed;
    public eScene CurScene;
    public eCurHomeScreen curHomeScreen;

    //momentane unlocked slots and creatures, die der Spieler hat
    public List<MonsterSlot> CurMonsters = new List<MonsterSlot>(3);

    //Lautstärke vom Spieler eingestellt
    public float BGMVolume;
    public float SFXVolume;


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        foreach (MonsterSlot slot in CurMonsters)
        {
            slot.SlotID = CurMonsters.IndexOf(slot);
        }
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
