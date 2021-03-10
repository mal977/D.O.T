using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyDataTest : MonoBehaviour
{
    public TMTTestData tmtTestData;
    public RecgoniseTestData recgoniseTestData;
    private TestManagerScript tms;

    [SerializeField]
    private Button populateButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button clearButton;

    void Start()
    {
        if (tms == null)
        {
            tms = TestManagerScript.GetInstance();
        }
        loadButton.onClick.AddListener(()=>tms.LoadTestData());
        clearButton.onClick.AddListener(() => tms.ClearAllTestData());
        populateButton.onClick.AddListener(() => PopulateData());
        saveButton.onClick.AddListener(() => tms.SaveTestData());
    }

    public void PopulateData() 
    {
        tmtTestData = new TMTTestData();
        tmtTestData.Score = 14;
        tmtTestData.Errors = 1;
        tmtTestData.TimeTaken = 15;
        tmtTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        recgoniseTestData = new RecgoniseTestData();
        recgoniseTestData.Score = 10;
        recgoniseTestData.Errors = 3;
        recgoniseTestData.TimeTaken = 40;
        recgoniseTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        tms.AddTestData(tmtTestData);
        tms.AddTestData(recgoniseTestData);
    }
}
