using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecgoniseTestData : TestData
{

    [NonSerializedAttribute] private int id = 0;

    public int score = 0;
    public int errors = 0;
    public long time_taken;     //Unix Milliseconds
    public long date_time_completed;     //Unix Milliseconds

    [NonSerializedAttribute] public long game_test_id = 1;     

    public int Score { get => score; set => score = value; }
    public int Errors { get => errors; set => errors = value; }
    public long TimeTaken { get => time_taken; set => time_taken = value; }
    public long DateTimeCompleted { get => date_time_completed; set => date_time_completed = value; }

    public override void SendData(HttpHelper httpHelper)
    {
        httpHelper.SendRecgoniseObjectData(this);
    } 
    public override void LogData()
    {
        
        Debug.Log(string.Format("RecogniseTestData Score: {0}, Errors: {1}, Time Taken: {2}, Date Time Completed: {3}", score, errors, time_taken, date_time_completed));

    }
}
