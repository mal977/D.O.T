using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneScript : MonoBehaviour
{
    TestManagerScript tms;
    // Start is called before the first frame update

    public GameObject ResultText;
    void Start()
    {
        tms = TestManagerScript.GetInstance();
        TMTTestData testData = new TMTTestData();
        testData.errors = 11;
        testData.score = 2;
        testData.TimeTaken = 1000;
        testData.date_time_completed = 2131231212;
        tms.AddTestData(testData);
        tms.SendTestDataToServer(SuccessSendingAllDataAction);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SuccessSendingAllDataAction()
    {
        EditorUtility.DisplayDialog("Success", "Sucess sending all data!", "Ok");
        ResultText.GetComponent<Text>().text = "Success sending all data!";
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
