using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestData
{
    public long game_test_id = 0;

    public abstract void SendData(HttpHelper httpHelper );
    public abstract void LogData();
   
}
