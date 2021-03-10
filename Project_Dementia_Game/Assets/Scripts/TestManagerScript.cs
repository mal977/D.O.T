using System;
using System.Collections;
using UnityEngine;

public class TestManagerScript : MonoBehaviour
{
    private ArrayList testDataList = new ArrayList();
    private static TestManagerScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = new TestManagerScript();
        }
        DontDestroyOnLoad(this);
    }

    public static TestManagerScript GetInstance()
    {
        if (instance == null)
        {
            instance = new TestManagerScript();
        }
        return instance;
    }

    public void AddTestData(TestData testData)
    {
        testDataList.Add(testData);
        testData.LogData();
    }

    // Save and Test Datas are hard coded for player prefs saving method

    // Save data into database/Player Prefs
    public void SaveTestData()
    {
        if (testDataList.Count > 0) 
        {
            if (testDataList[0] != null)
            {
                TMTTestData tmtData = (TMTTestData)testDataList[0];
                PlayerPrefs.SetInt("tmt_score", tmtData.Score);
                PlayerPrefs.SetInt("tmt_errors", tmtData.Errors);
                PlayerPrefs.SetString("tmt_time_taken", tmtData.TimeTaken.ToString());
                PlayerPrefs.SetString("tmt_date_time_completed", tmtData.DateTimeCompleted.ToString());
                PlayerPrefs.Save();
            }
            if (testDataList[1] != null)
            {
                RecgoniseTestData recgoniseData = (RecgoniseTestData)testDataList[1];
                PlayerPrefs.SetInt("recognise_score", recgoniseData.Score);
                PlayerPrefs.SetInt("recognise_errors", recgoniseData.Errors);
                PlayerPrefs.SetString("recognise_time_taken", recgoniseData.TimeTaken.ToString());
                PlayerPrefs.SetString("recognise_date_time_completed", recgoniseData.DateTimeCompleted.ToString());
                PlayerPrefs.Save();
            }
            if (testDataList.Count >= 2)
                Debug.Log("Both tests data are saved");
            return;
        }
        Debug.Log("No data to save!");
    }

    // Load data from database/Player Prefs
    public void LoadTestData()
    {
        if (testDataList.Count >= 2)
        {
            Debug.Log("The test data are already full");
            return;
        }
        if (PlayerPrefs.HasKey("tmt_score"))
        {
            TMTTestData tmtData = new TMTTestData();
            tmtData.Score = PlayerPrefs.GetInt("tmt_score");
            tmtData.Errors = PlayerPrefs.GetInt("tmt_errors");
            tmtData.TimeTaken = Convert.ToInt64(PlayerPrefs.GetString("tmt_time_taken"));
            tmtData.DateTimeCompleted = Convert.ToInt64(PlayerPrefs.GetString("tmt_date_time_completed"));
            testDataList.Add(tmtData);
            tmtData.LogData();
            Debug.Log("TMT Test User Data loaded!");

            // Nested only if the 1st test is already taken
            if (PlayerPrefs.HasKey("recognise_score"))
            {
                RecgoniseTestData recgoniseData = new RecgoniseTestData();
                recgoniseData.Score = PlayerPrefs.GetInt("recognise_score");
                recgoniseData.Errors = PlayerPrefs.GetInt("recognise_errors");
                recgoniseData.TimeTaken = Convert.ToInt64(PlayerPrefs.GetString("recognise_time_taken"));
                recgoniseData.DateTimeCompleted = Convert.ToInt64(PlayerPrefs.GetString("recognise_date_time_completed"));
                testDataList.Add(recgoniseData);
                recgoniseData.LogData();
                Debug.Log("Recognise Test User Data loaded!");
            }
            return;
        }
        Debug.Log("No data are found in player prefs");
    }

    // Clear data from database/Player Prefs
    public void ClearAllTestData()
    {
        PlayerPrefs.DeleteAll();
        testDataList.Clear();
        Debug.Log("All data are cleared from player prefs and in application");
    }

}
