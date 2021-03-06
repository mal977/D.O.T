using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerScript : MonoBehaviour
{
    private ArrayList testDataList = new ArrayList();
    private static TestManagerScript instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

}
