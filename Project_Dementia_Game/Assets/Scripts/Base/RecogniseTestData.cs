using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecgoniseTestData : TestData
{

    private int score = 0;
    private int errors = 0;
    private long timeTaken;     //Unix Milliseconds
    private long dateTimeCompleted;     //Unix Milliseconds

    public int Score { get => score; set => score = value; }
    public int Errors { get => errors; set => errors = value; }
    public long TimeTaken { get => timeTaken; set => timeTaken = value; }
    public long DateTimeCompleted { get => dateTimeCompleted; set => dateTimeCompleted = value; }

    public override void SendData()
    {

    } 
    public override void LogData()
    {
        
        Debug.Log(string.Format("RecogniseTestData Score: {0}, Errors: {1}, Time Taken: {2}, Date Time Completed: {3}", score, errors, timeTaken, dateTimeCompleted));

    }
}
