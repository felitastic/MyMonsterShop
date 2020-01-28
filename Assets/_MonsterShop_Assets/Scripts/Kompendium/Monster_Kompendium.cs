using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Monster_Kompendium : MonoBehaviour
{
    /// <summary>
    /// Reihenfolge wie eMonsterType
    /// </summary>
    public Kompendium_Entry[] MonsterEntry;

    public Image[] ButtonImage;
    public Text[] ButtonText;
    public Text CurMonsterFluff;
    public Image CurMonsterImage;
    public Text CurMonsterName;
    public Text CurMonsterRarity;
    public Text CurMonsterHatchCount;
    public Text CurMonsterHighestPrice;

    public GameObject[] Page;
    private int curPage;

    private GameManager GM;
    
    private void Start()
    {
        GM = GameManager.Instance;
        GM.monsterKompendium = this;
        curPage = 0;
    }

    public void SetButtons()
    {
        for (int i = 0; i < GM.UnlockedLogEntries.Length; ++i)
        {
            if (GM.UnlockedLogEntries[i])
            {
                ButtonImage[i].sprite = MonsterEntry[i].UnlockedButtonImage;
                //ButtonText[i].text = MonsterEntry[i].ButtonText;
                ButtonText[i].text = "";
            }
            else
            {
                ButtonImage[i].sprite = MonsterEntry[i].ShadowButtonImage;
                ButtonText[i].text = "???";
            }
        }
    }

    /// <summary>
    /// Called by button click, shows entry info if unlocked by reading from the monsterentry array    /// 
    /// </summary>
    /// <param name="EntryNo"></param>
    public void ShowEntry(int EntryNo)
    {
        if (GM.UnlockedLogEntries[EntryNo])
        {
            CurMonsterImage.sprite = MonsterEntry[EntryNo].MonsterImage;
            CurMonsterFluff.text = MonsterEntry[EntryNo].MonsterFluff;
            CurMonsterName.text = MonsterEntry[EntryNo].MonsterName;
            CurMonsterRarity.text = "Rarity: "+ MonsterEntry[EntryNo].MonsterRarity;
            CurMonsterHatchCount.text = "Total hatched: "+ MonsterEntry[EntryNo].MonsterHatchCount;
            CurMonsterHighestPrice.text = "Highest price: "+ MonsterEntry[EntryNo].MonsterHighestPrice;
        }
        else
        {
            CurMonsterImage.sprite = MonsterEntry[EntryNo].ShadowButtonImage;
            CurMonsterFluff.text = "Sell adult version of this monster to unlock info!";
            CurMonsterName.text = "???"; 
            CurMonsterRarity.text = "Rarity: ???"; 
            CurMonsterHatchCount.text = "Total hatched: 0"; 
            CurMonsterHighestPrice.text = "Highest price: N/A"; 
        }
    }

    public void SetActive(Button button)
    {
        button.Select();
        button.animator.SetTrigger("Selected");
    }

    public void ChangePage(bool left)
    {
        if (left)
        {
            curPage -= 1;
            if (curPage >= 0 && curPage < Page.Length)
            {
                Page[curPage+1].SetActive(false);
                Page[curPage].SetActive(true);
            }
            else if (curPage < 0)
            {
                Page[curPage + 1].SetActive(false);
                curPage = Page.Length-1;
                Page[curPage].SetActive(true);
            }
        }
        else
        {
            curPage += 1;
            if (curPage >= 0 && curPage < Page.Length)
            {
                Page[curPage - 1].SetActive(false);
                Page[curPage].SetActive(true);
            }
            else if (curPage >= Page.Length)
            {
                Page[curPage - 1].SetActive(false);
                curPage = 0;
                Page[curPage].SetActive(true);
            }
        }
    }
}   
