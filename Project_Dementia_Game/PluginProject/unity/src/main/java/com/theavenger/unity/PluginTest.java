package com.theavenger.unity;

import android.util.Log;

public class PluginTest {
    private static final PluginTest instance = new PluginTest();
    private static final String TAG = "UnityAndroidMalPlugin";

    public long startTime;

    public static PluginTest getInstance() {
        return instance;
    }

    private PluginTest(){
        Log.i(TAG,"PlugInTest" + System.currentTimeMillis());
        startTime = System.currentTimeMillis();
    }

    public double getElapsedTime(){
        return (System.currentTimeMillis() - startTime)/1000.0f;
    }
}
