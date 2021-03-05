using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class TestDataPlaceHolder
{
    public int testNo;
    public int score;
    public int numberErrors;
    public long timeToComplete;
    public string timeStampCompleted;

    public TestDataPlaceHolder() 
    {
        this.testNo = 1;
        this.score = 5;
        this.numberErrors = 3;
        this.timeToComplete = 12345;
        this.timeStampCompleted = System.DateTime.Now.ToString();
    }

    public TestDataPlaceHolder(int testNo, int score, int numberErrors, long timeToComplete)
    {
        this.testNo = testNo;
        this.score = score;
        this.numberErrors = numberErrors;
        this.timeToComplete = timeToComplete;
        this.timeStampCompleted = System.DateTime.Now.ToString();
    }
}
