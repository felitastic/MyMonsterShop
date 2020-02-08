using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntry", menuName = "MonsterShop/KompendiumEntry", order = 2)]
public class Kompendium_Entry : ScriptableObject
{
    //shadow image for button
    //unlocked image for button
    //??? text for button
    //unlocked text for button
    public Sprite ShadowButtonImage;
    public Sprite UnlockedButtonImage;
    public string ButtonText;
    public Sprite MonsterImage;
    public string MonsterFluff;
    public string MonsterName;
    public string MonsterRarity;
    public int MonsterHatchCount;
    public int MonsterHighestPrice;
    public eMonsterType MonsterType;
}
