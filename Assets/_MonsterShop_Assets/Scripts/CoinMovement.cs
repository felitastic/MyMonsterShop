using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Animator coinAnim;
    public RectTransform coinPos;
    public RectTransform coinDestination;
    private float speed;
    private Vector2 curPos;
    private Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        coinDestination = GameManager.Instance.homeUI.CoinDestination;
        speed = 1.5f;
        curPos = new Vector2(coinPos.anchoredPosition.x, coinPos.anchoredPosition.y);
        newPos = curPos;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        coinAnim.SetBool("moving", true);
        print("start pos: " + curPos);

        while (!Mathf.Approximately(coinPos.anchoredPosition.x, coinDestination.anchoredPosition.x) 
            && !Mathf.Approximately(coinPos.anchoredPosition.y, coinDestination.anchoredPosition.y))
        {
            float x = curPos.x;
            float y = curPos.y;

            if (!Mathf.Approximately(coinPos.anchoredPosition.x, coinDestination.anchoredPosition.x))
            {
                x = curPos.x - speed;
            }
            else
            {
                x = curPos.x;
            }

            if (!Mathf.Approximately(coinPos.anchoredPosition.y, coinDestination.anchoredPosition.y))
            {
                y = curPos.y + speed * 5;
            }
            else
            {
                y = curPos.y;
            }

            Vector2 newPos = new Vector2(x,y);
            coinPos.anchoredPosition = newPos;
            curPos = newPos;
            print("new pos: " + newPos);
            yield return null;
        }
        //wait for spawn to finish
        //move up to destination
        //play despawn when reached
        //destroy this object
        coinAnim.SetBool("moving", false);
        yield return null;
    }
}
