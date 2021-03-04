using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMTTestData : TestData
{
    private int score;
    private int errors;
    private long timeTaken;
    private long dateTimeCompleted;

    public int Score { get => score; set => score = value; }
    public int Errors { get => errors; set => errors = value; }
    public long TimeTaken { get => timeTaken; set => timeTaken = value; }
    public long DateTimeCompleted { get => dateTimeCompleted; set => dateTimeCompleted = value; }

    public override void SendData()
    {

    } 
    public override void LogData()
    {
        Debug.Log("TMTTestData Score: {0}, Errors: {1}, Time Taken: {2}, Date Time Completed: {3}" );
    }
}
