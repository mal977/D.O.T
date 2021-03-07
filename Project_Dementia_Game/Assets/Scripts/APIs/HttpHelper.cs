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

    // Start is called before the first frame update
    void Start()
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
        RestClient.Post<CustomResponse>("http://172.21.148.163/api/patients/new-test/", null).Then(response => {
            EditorUtility.DisplayDialog("Json",JsonUtility.ToJson(response, true), "Ok");
        });

        // Handicap code to implement promises by myself @Malcom
        testPromise().Done(() => { EditorUtility.DisplayDialog("hwe","Ewe","Ok"); }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MPromise<string> testPromise()
    {
        var promise = new MPromise<string>();
        StartCoroutine(GetText(promise));
        return promise;
    }

    IEnumerator GetText(MPromise<string> mPromise)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163/api/patients/new-test/", formData);
        www.SetRequestHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNjQ2NTgxMDc4LCJqdGkiOiIzM2U0ODU4ZTBiZDg0NDcxYjIzM2RmNGM1NDVmZTliMiIsInVzZXJfaWQiOjF9.g2cshvhfMZfU-w82dELwg9ypFbGc4WDSLPwXPwgnHZI");
        yield return www.SendWebRequest();

        if (www.result==UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            mPromise.Resolve();

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
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

// Handicap code to implement promises by myself @Malcom
public class MPromise<T>
{
    int status = 0;
    Action onResolved;

    public void Done(Action action)
    {
        if (status == 0)
        {
            onResolved = action;
        }
        else
        {
            action();
        }
    }
    public void Resolve()
    {
        status = 1;
        if (onResolved != null)
        {
            onResolved();
        }
    }
}