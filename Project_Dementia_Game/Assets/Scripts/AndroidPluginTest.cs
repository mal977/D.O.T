using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPluginTest : MonoBehaviour
{

    const string pluginName = "com.theavenger.unity.PluginTest";
    static AndroidJavaClass pluginClass;
    static AndroidJavaObject pluginInstance;

    public Button btn;
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if(pluginClass == null)
            {
                pluginClass = new AndroidJavaClass(pluginName);
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
                pluginClass.SetStatic<AndroidJavaObject>("mainActivity", activity);
            }
            return pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if(pluginInstance == null)
            {
                pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return pluginInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Elapsed Time: " + getElapsedTime());
        btn.onClick.AddListener(showNotification);
    }

    void showNotification()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        var notification = new AndroidNotification();
        notification.Title = "SomeTitle";
        notification.Text = "SomeText";
        notification.FireTime = System.DateTime.Now.AddSeconds(5);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    float elapsedTime = 0;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 5)
        {
            elapsedTime -= 5;
            Debug.Log("Tick: " + getElapsedTime());
        }
        
    }

    double getElapsedTime()
    {
        if (Application.platform == RuntimePlatform.Android)
            return PluginInstance.Call<double>("getElapsedTime");
        Debug.LogWarning("Wrong Platform");
        return 0;
    }


}
