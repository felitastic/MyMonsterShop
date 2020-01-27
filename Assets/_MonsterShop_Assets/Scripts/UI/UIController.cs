using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
    //Game Manager shortcut
    public GameManager GM;

    public Image[] XPBars;
    //Menus in the Scene
    [Tooltip("Menu Objects to en/disable")]
    public GameObject[] Menus;
    //CanvasGroups in the Scene for Fadeout
    [Tooltip("Canvas Groups for Fadeout/in")]
    public CanvasGroup[] Canvasgroups;
    //Text fields for changing them
    public Text[] Textfields;
    //Buttons in the Scene for en/disable etc
    public Button[] Buttons;

    public virtual void GetGameManager()
    {
        GM = GameManager.Instance;
    }

    public virtual void DisableMenu(int menu)
    {
        Menus[menu].SetActive(false);
    }
    public virtual void EnableMenu(int menu)
    { 
        Menus[menu].SetActive(true);
    }

    public virtual void SetText(int textfield, string newText)
    {
        Textfields[textfield].text = newText;
    }
    public virtual void EnableButton(int thisButton)
    {
        Buttons[thisButton].interactable = true;
    }
    public virtual void DisableButton(int thisButton)
    { 
        Buttons[thisButton].interactable = false;
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

    public virtual void InfoPopup()
    {

    }


}


