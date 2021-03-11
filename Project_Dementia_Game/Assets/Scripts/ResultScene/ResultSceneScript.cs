using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
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

        ResultText.GetComponent<Text>().text = "He sending all data!";
        ScheduleNextTest();

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
#if UNITY_EDITOR
        EditorUtility.DisplayDialog("Success", "Sucess sending all data!", "Ok");
#endif
        ResultText.GetComponent<Text>().text = "Success sending all data!";
    }

    void ScheduleNextTest()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ResultText.GetComponent<Text>().text = "Android sending all data!";

            var c = new AndroidNotificationChannel()
            {
                Id = "dementia_schedule_test",
                Name = "Dementia Test",
                Importance = Importance.High,
                Description = "Reminder for the patient to take the scheduled test.",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notification = new AndroidNotification();
            notification.Title = "Wellness check!";
            notification.Text = "Tap the notification to take your weekly wellness check!";
            notification.FireTime = System.DateTime.Now.AddDays(1);
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
