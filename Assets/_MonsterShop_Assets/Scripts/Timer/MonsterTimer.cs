using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterTimer : MonoBehaviour
{   
    private GameManager GM;
    private DateTime curDate { get { return DateTime.Now; } }
    private MonsterSlot curMonster { get { return GM.CurMonsters[(int)GM.curMonsterID]; } }

    //Seconds for the timer until you can pet again
    private float curPetWaitTime;
    //Seconds for the timer until you can play again
    private float curPlayWaitTime;
    //Seconds until the dungeon lord is available again
    private float curDungeonLordWaitTime;

    private bool runDLTimer;
    private bool runPetTimer;
    private bool runPlayTimer;

    ////test values
    //bool testbool;
    //DateTime EndTime;
    //double waitMinutes;
    //float waitSeconds;
    //double curStartTime;
    //float curWaitingTime;

    private void Awake()
    {
        runPetTimer = false;
        runPlayTimer = false;
        runDLTimer = false;
    }

    private void Start()
    {
        GM = GameManager.Instance;
        GM.monsterTimer = this;        
        CheckDateTimes();
    }
    
    private void Update()
    {
        if (runPetTimer)
        {
            PetTimer();
        }
        if (runPlayTimer)
        {
            PlayTimer();
        }
        if (runDLTimer)
        {
            DLTimer();
        }
    }
    /// <summary>
    /// Timer for the Dungeon Lord
    /// </summary>
    private void DLTimer()
    {
        curDungeonLordWaitTime -= Time.deltaTime;
        GM.homeUI.UpdateDLTimer(ConvertTimeToText(curDungeonLordWaitTime));

        if (curDungeonLordWaitTime <= 0.0f)
        {
            EnableDungeonLord();
        }
    }

    private void EnableDungeonLord()
    {
        runDLTimer = false;
        GM.DLIsGone = false;
        curDungeonLordWaitTime = 0.0f;
        GM.homeUI.UpdateDLTimer("Sell");
        print("Dungeon lord is ready");
    }

    /// <summary>
    /// Timer for the petting sessions
    /// </summary>
    private void PetTimer()
    {
        curPetWaitTime -= Time.deltaTime;

        string SendToPetUI = ConvertTimeToText(curPetWaitTime);
        //print("PetTimer: " + SendToPetUI);

        if (curPetWaitTime <= 0.0f)
        {
            EnablePetSession();
        }
    }

    private void EnablePetSession()
    {
        runPetTimer = false;
        GM.CurMonsters[GM.curMonsterID].IsHappy = false;
        curPetWaitTime = 0.0f;
        GM.homeUI.SetPettingSymbol(true);
        print("Pet timer done");
    }

    private void PlayTimer()
    {
        curPlayWaitTime -= Time.deltaTime;
        GM.homeUI.UpdatePlayTimer(ConvertTimeToText(curPlayWaitTime));

        if (curPlayWaitTime <= 0.0f)
        {
            EnableMinigames();
        }
    }

    private void EnableMinigames()
    {
        runPlayTimer = false;
        GM.CurMonsters[GM.curMonsterID].IsTired = false;
        curPlayWaitTime = 0.0f;
        GM.homeUI.SetPlayTimer(false);
        GM.homeUI.TrainButtonActive(true);
        print("Play timer done");
    }
    /// <summary>
    /// Checks if a timer should be running for the current monster
    /// </summary>
    public void CheckDateTimes()
    {
        if (curMonster.Monster != null)
        {
            if (curMonster.PetTimerEnd <= curDate)
            {
                EnablePetSession();
            }
            else
            {
                curPetWaitTime = CalculateWaitingTimes(curMonster.PetTimerEnd);
                GM.homeUI.SetPettingSymbol(false);
                runPetTimer = true;
            }

            if (curMonster.PlayTimerEnd <= curDate)
            {
                EnableMinigames();
            }
            else
            {
                curPlayWaitTime = CalculateWaitingTimes(curMonster.PlayTimerEnd);
                GM.homeUI.TrainButtonActive(false);
                GM.homeUI.SetPlayTimer(true);
                runPlayTimer = true;
            }
        }

        if (GM.DungeonLordWaitTimeEnd <= curDate)
        {
            EnableDungeonLord();
        }
        else
        {
            curDungeonLordWaitTime = CalculateWaitingTimes(GM.DungeonLordWaitTimeEnd);
            runDLTimer = true;
        }
    }

    /// <summary>
    /// Returns the current waiting time in seconds for the timer
    /// </summary>
    /// <param name="endTime"></param>
    /// <returns></returns>
    private float CalculateWaitingTimes(DateTime endTime)
    {
        // Find time difference between two dates
        TimeSpan TimeDifference = curDate - endTime;

        int hours = TimeDifference.Hours * -1;
        //print("hours to wait: " + hours);
        int mins = TimeDifference.Minutes * -1;
        //print("mins to wait: " + mins);
        int secs = TimeDifference.Seconds * -1;
        //print("secs to wait: " + secs);

        return (float)hours * 3600 + (float)mins * 60 + (float)secs;
    }

    private string ConvertTimeToText(float curTime)
    {
        int curMin = Mathf.FloorToInt(curTime / 60F);
        int curSec = Mathf.FloorToInt(curTime - curMin * 60);

        if (curSec >= 10)
        {
            if (curMin >= 10)
            {
                return (curMin + ":" + curSec);
            }
            else 
            {
                return ("0"+curMin + ":" + curSec);
            }
        }
        else
        {
            if (curMin >= 10)
            {
                return (curMin + ":0" + curSec);
            }
            else 
            {
                return ("0" + curMin + ":0" + curSec);
            }
        }
    }

    /// <summary>
    /// Sets values for the currently selected monster on camera movement in home screen
    /// </summary>
    //public void OnSceneChange()
    //{
    //    if (GM.CurMonsters[GM.curMonsterID].Monster != null)
    //    {
    //        if (GM.CurMonsters[GM.curMonsterID].IsHappy || GM.CurMonsters[GM.curMonsterID].IsTired)
    //        {
    //            CompareDateTimes();
    //        }
    //    }
    //    else
    //    {
    //        GM.homeUI.SetPettingSymbol(false);
    //    }
    //}

    ///// <summary>
    ///// Compares Endtime to current Time on SceneChange to see if the timer is even necessary
    ///// </summary>
    //private void CompareDateTimes()
    //{
    //    if (GM.PetWaitTimeEnd[GM.curMonsterID] <= curDate)
    //    {
    //        GM.CurMonsters[GM.curMonsterID].IsHappy = true;
    //        GM.homeUI.SetPettingSymbol(false);
    //        //happy idle
    //        print("Monster ready for affection <3");
    //    }
    //    else
    //    {
    //        curPetWaitTime = GetWaitingTimeInSeconds(GM.PetWaitTimeEnd[GM.curMonsterID]);
    //        GM.homeUI.SetPettingSymbol(true);
    //        //sad idle
    //    }

    //    if (GM.PlayWaitTimeEnd[GM.curMonsterID] <= curDate)
    //    {
    //        GM.CurMonsters[GM.curMonsterID].IsTired = true;
    //        //GM.homeUI.SetPlayTimer(true);
    //        print("Monster wants to play! :D");
    //    }
    //    else
    //    {
    //        curPlayWaitTime = GetWaitingTimeInSeconds(GM.PlayWaitTimeEnd[GM.curMonsterID]);
    //        //GM.homeUI.SetPlayTimer(false);
    //    }

    //    GM.runTimers = true;
    //} 

    //public void SetPetTimerValues()
    //{
    //    if (GM.CurMonsters[GM.curMonsterID].IsHappy)
    //    {
    //        GM.PetWaitTimeEnd[GM.curMonsterID] = curDate.AddMinutes(petWaitInMinutes);
    //        print("Pet Monster " + GM.curMonsterID + ": " + GM.PetWaitTimeEnd[GM.curMonsterID]);
    //        curPetWaitTime = GetWaitingTimeInSeconds(GM.PetWaitTimeEnd[GM.curMonsterID]);
    //        print("Seconds til PET session: " + curPetWaitTime);
    //        GM.homeUI.SetPettingSymbol(false);
    //    }      
    //}

    //public void SetPlayTimerValues()
    //{
    //    if (GM.CurMonsters[GM.curMonsterID].IsTired)
    //    {
    //        GM.PlayWaitTimeEnd[GM.curMonsterID] = curDate.AddMinutes(playWaitInMinutes);
    //        print("Play Monster " + GM.curMonsterID + ": " + GM.PlayWaitTimeEnd[GM.curMonsterID]);
    //        curPlayWaitTime = GetWaitingTimeInSeconds(GM.PlayWaitTimeEnd[GM.curMonsterID]);
    //        print("Seconds til PLAY session: " + curPlayWaitTime);
    //        GM.homeUI.SetPlayTimer(true);
    //        GM.homeUI.TrainButtonActive(false);
    //    }
    //}
    //################################################################


    ////test functions

    ////when starting counter after scene reload, test if its already done
    //private void CheckCounters()
    //{
    //    //Else if nowTime = EndTime, set bool true
    //    if (EndTime <= DateTime.Now)
    //    {
    //        testbool = true;
    //        print("Hooray, countdown finished");
    //    }
    //}

    //public void StartTestTimer()
    //{
    //    //Get StartTime = current Time
    //    DateTime StartTime = DateTime.Now;
    //    print("StartTime:" + StartTime);
    //    //Set EndTime by adding x minutes and x hours to StartTime
    //    waitMinutes = 1.0d;
    //    curWaitingTime = (float)waitMinutes * 60;
    //    EndTime = StartTime.AddMinutes(waitMinutes);
    //        //new DateTime(StartTime.Year, StartTime.Month, StartTime.Day).AddMinutes(waitMinutes);

    //    print("Endtime:" + EndTime);
    //    print("waiting for " + curWaitingTime + " seconds");
    //    //Calculate difference for the UI timer
    //    double WaitTimeInMinutes = (StartTime - EndTime).TotalMinutes;
    //    //Set bool false
    //    print("bool is false, timer running");
    //    testbool = false;
    //}

    //private void TestTimer()
    //{
    //    curWaitingTime -= Time.deltaTime;

    //    //gives these to UI for the countdown
    //    int curMin = Mathf.FloorToInt(curWaitingTime / 60F);
    //    int curSec = Mathf.FloorToInt(curWaitingTime - curMin * 60);  
    //    if (curSec >= 10)
    //        print("Timer: " + curMin+":"+ curSec);
    //    else
    //        print("Timer: " + curMin+":0"+ curSec);

    //    if (curWaitingTime <= 0.0f)
    //    {
    //        testbool = true;
    //        curWaitingTime = 0.0f;
    //        print("Hooray, countdown finished");
    //    }
    //}   

    ////private void Update()
    ////{
    ////    //Run Countdown for UI timer while bool false
    ////    if (!testbool)
    ////        TestTimer();

    ////    //updates all counters
    ////    //starts and finishes counters
    ////    //sets bools according to timer result

    ////    //UI for petting and minigame is updated via camera movement

    ////    //run pet timer for each pet -> if not monster null
    ////    //run dungeon lord timer -> if not all monsterslots null
    ////    //run minigame timer for each pet -> after playing
    ////}
}
