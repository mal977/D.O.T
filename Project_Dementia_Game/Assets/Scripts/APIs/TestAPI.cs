using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System;

public class TestAPI : HttpRequest
{
    private AsyncOperation asyncRequest;
    public int GetTestID(int testNo, string patientID) 
    {
        return 1;
    }

    public void SendTestResult(int testID, int testNo, TestDataPlaceHolder placeHolderTestData) 
    { 
        
    }
    // Note: Patient ID will be stored as token (uid) in the unity persistentData once the login is verified

    // On start test: Get testID with patient uid
    public string GetTrailMakingTestID(string uid) {
        // Need to reconfirm with HeFei, his confirmation not very clear
        string urlPath = "patient/uid/tests";
        var response = GetRequest(urlPath);
        if (!string.IsNullOrEmpty(response.Result))
        {
            throw new Exception();
        }
        response.Wait();
        while (!response.IsCompleted)
            Task.Delay(100);
        return response.Result;
    }

    // On second test: Get testID
    public string GetPictureMatchingTestID(string uid)
    {
        // Need to reconfirm with HeFei, his confirmation not very clear
        string urlPath = "/patient/uid/tests";
        var response = GetRequest(urlPath);
        if (!string.IsNullOrEmpty(response.Result))
        {
            throw new Exception();
        }
        response.Wait();
        while (!response.IsCompleted)
            Task.Delay(100);
        return response.Result;
    }

    // Post TestData for respective testID and test number: Score, Error, Duration, Timestamp Completed
    // Test one
    public string SendTrailMakingTestResult(TestDataPlaceHolder pH, string testID) 
    {
        string urlPath = "/patient/uid/tests";
        SendResult(urlPath, pH);
        return "";
    }

    // Test two
    public string SendPictureMatchingTestResult(TestDataPlaceHolder pH, string testID)
    {
        string urlPath = "/patient/uid/tests";
        SendResult(urlPath, pH);
        return "";
    }

    private string SendResult(string urlPath, TestDataPlaceHolder pH) 
    {
        //Upload()
        return "";
    }
}
