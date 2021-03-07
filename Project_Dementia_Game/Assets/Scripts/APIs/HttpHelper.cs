using Proyecto26;
using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class HttpHelper : MonoBehaviour
{
   
    private string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNjQ2NTgxMDc4LCJqdGkiOiIzM2U0ODU4ZTBiZDg0NDcxYjIzM2RmNGM1NDVmZTliMiIsInVzZXJfaWQiOjF9.g2cshvhfMZfU-w82dELwg9ypFbGc4WDSLPwXPwgnHZI";
    
    private string url = "http://172.21.148.163/";
    private static HttpHelper instance;
    public static HttpHelper GetInstance()
    {
        if (instance == null)
        {
            instance = new HttpHelper();
        }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        //RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
        //RestClient.Post<CustomResponse>("http://172.21.148.163/api/patients/new-test/", null).Then(response =>
        //{
        //    EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
        //});

        // Handicap code to implement promises by myself @Malcom
        //PostPromise().Then(() => { EditorUtility.DisplayDialog("hwe", "Ewe", "Ok"); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MPromise PostPromise()
    {
        var promise = new MPromise("Test");

        return promise;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = new HttpHelper();
        }
        DontDestroyOnLoad(this);
    }

    public void Login(String inEmail, String inPassword, Action resolveAction)
    {
        string path = url + "api/auth/login";
        RestClient.Post<LoginResponse>(path, new LogInUser{ email = inEmail, password = inPassword }).Then(response =>
        {
            // Very hacky here, the server sends the token data as one long string, cant parse as Json cause of the single quotes
            // So i parse it to a string, replace the single quotes with double quotes to conform to Json standards, then parse it again
            EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
            PlayerPrefs.SetString(PlayerPrefsHelper.PREF_ACCESS_TOKEN, response.access_token);
            resolveAction();
        }).Catch(err =>{
            EditorUtility.DisplayDialog("Error", err.ToString(), "Ok");
            
        });

    }

}

[Serializable]
public class CustomResponse
{
    public int user_id;
    public string user_name;
    public int patient_id;
    public int new_test_id;
}

[Serializable]
public class LogInUser
{
    public string email;
    public string password;

}

[Serializable]
public class LoginResponse
{
    public string email;
    public string username;
    public string access_token;
    public string refresh_token;
}

// Handicap code to implement promises by myself @Malcom


enum States
{
    PENDING = 0,
    FULFILLED = 1,
    REJECTED = 2

}
public class MPromise : MonoBehaviour
{
    States state;
    string value;
    string reason;

    public Queue<MPromise> thenQueue = new Queue<MPromise>();
    public Queue<MPromise> finallyQueue = new Queue<MPromise>();
    public MPromise(object func)
    {
        this.state = States.PENDING;
        try
        {
            if (func == typeof(System.String))
            {
                StartCoroutine(GetText());
            }
            this.onDone();
        }
        catch (Exception e)
        {

        }

    }

   
    public MPromise Then(Type func)
    {
        MPromise controlledPromise = new MPromise(func);
        this.thenQueue.Enqueue(controlledPromise);

        if(this.state == States.FULFILLED)
        {
            this.propogateDone();
        }

        return controlledPromise;

    }

    private void onDone()
    {
        this.state = States.FULFILLED;
        this.propogateDone();
    }

    private void propogateDone()
    {
        foreach (MPromise mPromise in thenQueue)
        {

        }
    }

    IEnumerator GetText()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163/api/patients/new-test/", formData);
        www.SetRequestHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNjQ2NTgxMDc4LCJqdGkiOiIzM2U0ODU4ZTBiZDg0NDcxYjIzM2RmNGM1NDVmZTliMiIsInVzZXJfaWQiOjF9.g2cshvhfMZfU-w82dELwg9ypFbGc4WDSLPwXPwgnHZI");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            value = www.downloadHandler.text;
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}