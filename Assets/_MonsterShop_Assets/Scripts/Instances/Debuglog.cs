using core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debuglog : Singleton<Debuglog>
{
    protected Debuglog() { }

    public Text BuildInfo;
    public GameObject LogTexts;
    public KeyCode OnOffKey = KeyCode.Escape;
    public Color textColor = Color.green;

    public Text[] line;
    private int curLine = 0;

    public void OnEnable()
    {
        //BuildInfo.text = "Development Build \nNo final assets";
    }

    public void Update()
    {
        if (Input.GetKeyDown(OnOffKey))
        {
            if (LogTexts.activeSelf)
            {
                LogTexts.SetActive(false);
            }
            else
            {
                LogTexts.SetActive(true);
            }
        }
    }

    public void PrintToLog(string newline)
    {
        line[curLine].text = newline;
        line[curLine].color = textColor;
        curLine += 1;

        if (curLine > line.Length)
            curLine = 0;
    }
}
