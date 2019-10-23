using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private int testvalue1 = 0;
    private int testvalue2 = 0;

    public Text textfeld1;
    public Text textfeld2;

    void Update()
    {
        textfeld1.text = testvalue1 + "";
        textfeld2.text = testvalue2 + "";
    }

    public void Test1()
    {
        testvalue1 += 1;
    }    
    public void Test2()
    {
        testvalue2 -= 1;
    }
}
