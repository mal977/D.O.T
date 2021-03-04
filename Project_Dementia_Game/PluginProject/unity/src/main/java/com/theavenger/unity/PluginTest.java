package com.theavenger.unity;

import android.app.Activity;
import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.os.Build;
import android.util.Log;

import androidx.core.app.NotificationCompat;
import androidx.core.app.NotificationManagerCompat;

public class PluginTest {
    private static final PluginTest instance = new PluginTest();
    private static final String TAG = "UnityAndroidMalPlugin";
    private static final String CHANNEL_ID = "Demencia";

    public long startTime;

    public static Activity mainActivity;

    public static PluginTest getInstance() {
        return instance;
    }

    private PluginTest(){
        Log.i(TAG,"PlugInTest" + System.currentTimeMillis());
        startTime = System.currentTimeMillis();
    }

    public void showNotification() {
        Log.i(TAG,mainActivity.toString());
        NotificationCompat.Builder builder = new NotificationCompat.Builder(mainActivity.getApplicationContext(),CHANNEL_ID)
                .setContentTitle("Hello")
                .setContentText("Err this is a hello message")
                .setPriority(NotificationCompat.PRIORITY_DEFAULT);

        createNotificationChannel();

        NotificationManagerCompat notificationManagerCompat = NotificationManagerCompat.from(mainActivity.getApplicationContext());
        notificationManagerCompat.notify(1,builder.build());
    }

    private void createNotificationChannel() {
        // Create the NotificationChannel, but only on API 26+ because
        // the NotificationChannel class is new and not in the support library
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            CharSequence name = "demencia_channel";
            String description = "demencia_notifications";
            int importance = NotificationManager.IMPORTANCE_DEFAULT;
            NotificationChannel channel = new NotificationChannel(CHANNEL_ID, name, importance);
            channel.setDescription(description);
            // Register the channel with the system; you can't change the importance
            // or other notification behaviors after this
            NotificationManager notificationManager = mainActivity.getSystemService(NotificationManager.class);
            notificationManager.createNotificationChannel(channel);
        }
    }

    public double getElapsedTime(){
        return (System.currentTimeMillis() - startTime)/1000.0f;
    }
}
