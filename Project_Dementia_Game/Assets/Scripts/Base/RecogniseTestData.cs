using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecgoniseTestData : TestData
{

    private int id = 0;
    private int score = 0;
    private int errors = 0;
    private long time_taken;     //Unix Milliseconds
    private long date_time_completed;     //Unix Milliseconds
    private long game_test_id = 1;     

    public int Score { get => score; set => score = value; }
    public int Errors { get => errors; set => errors = value; }
    public long TimeTaken { get => time_taken; set => time_taken = value; }
    public long DateTimeCompleted { get => date_time_completed; set => date_time_completed = value; }

    public override void SendData()
    {

    } 
    public override void LogData()
    {
        
        Debug.Log(string.Format("RecogniseTestData Score: {0}, Errors: {1}, Time Taken: {2}, Date Time Completed: {3}", score, errors, time_taken, date_time_completed));

    }
}
