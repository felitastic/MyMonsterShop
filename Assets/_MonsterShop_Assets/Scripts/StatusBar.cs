//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class StatusBar : MonoBehaviour
//{
//    [Header("Transform Objects of the bar")]
//    public RectTransform BarTransform;

//    [Tooltip("X Position = 100%")]
//    public float BarFull;
//    [Tooltip("X Position for HP = 0%")]
//    public float BarEmpty;

//    [Tooltip("Max value when 100%")]
//    public float MaxValue;
//    [Tooltip("Current value")]
//    public float CurValue;

//    [Tooltip("Current Y Pos., grab and does not change")]
//    public float cur_yPos;
//    [Tooltip("Current X Pos., grab at each call")]
//    public float cur_xPos;
//    [Tooltip("Desired X Pos., where to move the bar to")]
//    public float desired_xPos;


//    //public RectTransform RPnormalTransform;
//    //public RectTransform HPnormalTransform;
//    //public RectTransform HPhurtTransform;

//    //[Header("Values for xPos of the bar")]
//    //[Tooltip("X Position for HP = 100%")]
//    //public float minXValueHP;
//    //[Tooltip("X Position for HP = 0%")]
//    //public float maxXValueHP;
//    //[Tooltip("X Position for RP = 100%")]
//    //public float minXValueRP;
//    //[Tooltip("X Position for RP = 0%")]
//    //public float maxXValueRP;

//    //[Header("Positions for the change")]

//    //[Header("HP and RP values")]
//    //[Tooltip("Max HP in this fight")]
//    //public float maxHP;
//    //[Tooltip("Max RP in this fight, usually 100")]
//    //public float maxRP;
//    //[Tooltip("Current RP")]
//    //public float curRP;
//    //[Tooltip("Current HP")]
//    //public float curHP;
//    //[Tooltip("HP after reduction")]
//    //public float futureHP;
//    //[Tooltip("RP after reduction")]
//    //public float futureRP;

//    //[Header("For the credits lerp")]
//    //[Tooltip("Regulates how fast the hp decrease, higher no = slower")]
//    [SerializeField]
//    private float lerpTime = 0.75f;
//    private float curLerpTime;
//    private Vector3 StartPos;
//    private Vector3 EndPos;
//    private bool lerping;

//    public void FixedUpdate()
//    {
//        if (lerping)
//        {

//            if (BarTransform.anchoredPosition == new Vector2(EndPos.x, EndPos.y))
//            {
//                lerping = false;
//            }

//            curLerpTime += Time.deltaTime;
//            if (curLerpTime > lerpTime)
//                curLerpTime = lerpTime;

//            float percentage = curLerpTime / lerpTime;
//            BarTransform.anchoredPosition = Vector3.Lerp(StartPos, EndPos, percentage);
//        }
//    }



//    public void GetValues(float maxHitPoints, float maxRagePoints, float MaxXValueHP, float MaxXValueRP, float MinXValueRP, float MinXValueHP)
//    {
//        maxHP = Mathf.Round(maxHitPoints);
//        maxRagePoints = Mathf.Round(100f);
//        maxRP = 100.0f;

//        minXValueHP = Mathf.Round(MinXValueHP);
//        maxXValueHP = Mathf.Round(MaxXValueHP);

//        minXValueRP = Mathf.Round(MinXValueRP);
//        maxXValueRP = Mathf.Round(MaxXValueRP);
//    }

//    public void HPTick(float CurHP)
//    {
//        //print("tick hp");
//        futureHP = CurHP;

//        //set current position of the bar
//        cur_yPos = HPnormalTransform.anchoredPosition.y;
//        cur_xPos = HPnormalTransform.anchoredPosition.x;

//        //percentage values
//        float curPercent = 100 - ((futureHP / maxHP) * 100);
//        desired_xPos = maxXValueHP * (curPercent / 100);

//        HPnormalTransform.anchoredPosition = new Vector2(desired_xPos, cur_yPos);

//        //TODO remove when lerp is working?
//        curHP = futureHP;
//    }

//    public void RPTick(int CurRP)
//    {
//        //print("tick rp");
//        futureRP = (float)CurRP;

//        //set current position of the bar
//        cur_yPos = RPnormalTransform.anchoredPosition.y;
//        cur_xPos = RPnormalTransform.anchoredPosition.x;

//        //percentage values
//        float curPercent = 100 - ((futureRP / maxRP) * 100);
//        desired_xPos = maxXValueRP * (curPercent / 100);

//        RPnormalTransform.anchoredPosition = new Vector2(desired_xPos, cur_yPos);

//        curRP = futureRP;
//    }

//    public void HPLerp()
//    {
//        StartPos = HPhurtTransform.anchoredPosition;
//        EndPos = HPnormalTransform.anchoredPosition;
//        lerping = true;
//    }
//}