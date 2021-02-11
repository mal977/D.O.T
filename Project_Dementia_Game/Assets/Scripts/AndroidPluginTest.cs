using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPluginTest : MonoBehaviour
{

    const string pluginName = "com.theavenger.unity.PluginTest";
    static AndroidJavaClass pluginClass;
    static AndroidJavaObject pluginInstance;

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if(pluginClass == null)
            {
                pluginClass = new AndroidJavaClass(pluginName);
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
