using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Animator coinAnim;
    public RectTransform coinPos;
    public RectTransform coinDestination;

    //how many steps you wanna take
    private float totalSteps;

    private float stepX;
    private float stepY;

    private Vector2 curPos;
    private Vector2 newPos;
    //how often movement updates
    private float timeUnit;


    void Start()
    {
        coinDestination = GameManager.Instance.homeUI.CoinDestination;
        totalSteps = 60.0f;
        timeUnit = 0.75f/totalSteps;
        curPos = new Vector2(coinPos.anchoredPosition.x, coinPos.anchoredPosition.y);
        newPos = curPos;
        StartCoroutine(Move());
        stepX = CalculateStep(curPos.x, coinDestination.anchoredPosition.x);
        stepY = CalculateStep(curPos.y, coinDestination.anchoredPosition.y);
        //print("move x by " + stepX + ", move y by " + stepY);
    }

    /// <summary>
    /// calculates how much coin moves to x/y per time unit
    /// </summary>
    /// <returns></returns>
    private float CalculateStep(float start, float end)
    {
        return (Mathf.Abs(end - start)/ totalSteps);
    }

    private IEnumerator Move()
    {
        coinAnim.SetBool("moving", true);
        //print("start pos: " + curPos);

        for (int i = 0; i <= Mathf.RoundToInt(totalSteps); ++i)
        {
            newPos = new Vector2(curPos.x - stepX, curPos.y + stepY);
            coinPos.anchoredPosition = newPos;
            curPos = newPos;
            //print("new pos: " + newPos);
            yield return new WaitForSeconds(timeUnit);
        }

        yield return new WaitForSeconds(0.5f);
        coinAnim.SetBool("moving", false);
        Destroy(this.gameObject,0.3f);
    }
}
