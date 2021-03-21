using Newtonsoft.Json;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Networking;

public class HttpHelper : MonoBehaviour
{
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
        if (PlayerPrefs.HasKey(PlayerPrefsConst.PREF_ACCESS_TOKEN))
        {
            //If a token already exists, use that token for API calls, set the token as default header
            RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString(PlayerPrefsConst.PREF_ACCESS_TOKEN);

        }


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

    /**
     * @Author Malcom
     * This Login Method attempts to login the account using the input email and password. If its successful, it calls the resolveAction input Action
     * This method also saves access token in the player prefs
     * 
     * TODO add a action for failure? Maybe
     */
    public void Login(String inEmail, String inPassword, Action resolveAction, Action<string> errorAction)
    {
        string path = url + "api/auth/login";
        RestClient.Post<LoginResponse>(path, new LogInUser { email = inEmail, password = inPassword }).Then(response =>
         {
#if UNITY_EDITOR
             EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
#endif
             PlayerPrefs.SetString(PlayerPrefsConst.PREF_ACCESS_TOKEN, response.access_token);
             RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + response.access_token;
             resolveAction.Invoke();
         }).Catch(err =>
         {
#if UNITY_EDITOR
             EditorUtility.DisplayDialog("Error", err.ToString(), "Ok");
#endif
             errorAction.Invoke("Username or password is incorrect!");
         });
    }

    /**
     * @Author Malcom
     * This method starts a new test. Access token is added to the header. If successful, it calls the resolveAction input Action
     * This method also saves the newTestId response from the server in player prefs.
     * 
     * TODO: error checking for no code returned, someone could make a version which accepts an error action
     */
    public void StartNewTest(Action resolveAction, Action<string> errorAction)
    {
        string path = url + "api/patients/new-test/";
        RestClient.Post<NewTestResponse>(path, null).Then(response =>
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
#endif
            PlayerPrefs.SetString(PlayerPrefsConst.PREF_NEW_TEST_ID, response.new_test_id.ToString());
            resolveAction.Invoke();
        }).Catch((err) =>
        {
            RequestException error = err as RequestException;
            Debug.Log("Error: " + err.Message);
            errorAction.Invoke(err.Message);
        });
    }

    /**
     * @Author Malcom
     * This method attempts to register a new account
     * If successful, it will save the accss token in the player prefs. This application does not store the user's password
     * This method's error handling is really handicap, limited by Unity Json parser and the response by the backend server
     * 
     * TODO: add birthday field
     */
    public void CreateNewAccount(Register register, Action resolveAction, Action<string> errorAction)
    {
        string path = url + "api/auth/register";

        RestClient.Post<RegisterResponse>(path, register).Then(response =>
        {
            resolveAction.Invoke();
        }).Catch((err) =>
        {
            RequestException error = err as RequestException;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(error.Response);
            Debug.Log("Error: " + err.Message);
            string errorMessage = "";

            //Somewhat hacky error message handling. ask @Malcom for more info
            if (myDeserializedClass.errors.email != null)
            {
                foreach (string s in myDeserializedClass.errors.email)
                {
                    Debug.Log(s);
                    errorMessage += "email";
                }
            }
            if (myDeserializedClass.errors.username != null)
            {
                foreach (string s in myDeserializedClass.errors.username)
                {
                    Debug.Log(s);
                }
            }
            if (myDeserializedClass.errors.phone_number != null)
            {
                foreach (string s in myDeserializedClass.errors.phone_number)
                {
                    Debug.Log(s);
                    errorMessage += "phone";
                }
            }
            if (myDeserializedClass.errors.password != null)
            {
                foreach (string s in myDeserializedClass.errors.password)
                {
                    Debug.Log(s);
                    errorMessage += "password";
                }
            }
            if (myDeserializedClass.errors.message != null)
            {
                foreach (string s in myDeserializedClass.errors.message)
                {
                    Debug.Log(s);
                }
            }
            errorAction.Invoke(errorMessage);
        });
    }

    /**
    * @Author Malcom
    * This method attempts to send recgonise test data to backend
    * If successful, the test data will be reflected in the database
    * This method's error handling is really handicap, limited by Unity Json parser and the response by the backend server
    * 
    * TODO: why cant i get the response from the call, the value is updated correctly, the json fields are correct???
    */
    public void SendRecgoniseObjectData(RecgoniseTestData recgoniseTestData)
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConst.PREF_NEW_TEST_ID))
        {
            Debug.LogError("No Test ID!");
        }
        string path = url + "api/patients/tests/" + PlayerPrefs.GetString(PlayerPrefsConst.PREF_NEW_TEST_ID) + "/picture-object-matchs/";
        RestClient.Post<RecgoniseObjectResponse>(path, recgoniseTestData).Then(response =>
        {
            // I have no idea why i cant get the response for this call. Everything checks out, and the value is reflected correctly in the server @Malcom
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
#endif
        }).Catch((err) =>
        {
            RequestException error = err as RequestException;
            TestManagerScript.errList.Add(error);
            Debug.Log("Error: " + error.Response);

        }).Finally(() => { TestManagerScript.FinishSendingData(recgoniseTestData); });
    }

    /**
    * @Author Malcom
    * This method attempts to send TMT test data to backend
    * If successful, the test data will be reflected in the database
    * This method's error handling is really handicap, limited by Unity Json parser and the response by the backend server
    * 
    * TODO: why cant i get the response from the call, the value is updated correctly, the json fields are correct???
    */
    public void SendTMTData(TMTTestData tmtTestData)
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConst.PREF_NEW_TEST_ID))
        {
            Debug.LogError("No Test ID!");
        }
        string path = url + "api/patients/tests/" + PlayerPrefs.GetString(PlayerPrefsConst.PREF_NEW_TEST_ID) + "/trail-makings/";
        RestClient.Post<RecgoniseObjectResponse>(path, tmtTestData).Then(response =>
        {
            // I have no idea why i cant get the response for this call. Everything checks out, and the value is reflected correctly in the server @Malcom
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
#endif
        }).Catch((err) =>
        {
            RequestException error = err as RequestException;
            TestManagerScript.errList.Add(error);
            Debug.Log("Error: " + error.Response);

        }).Finally(() => { TestManagerScript.FinishSendingData(tmtTestData); });
    }

    public void sendTestDatas(TestDataPackage testDataPackage)
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConst.PREF_NEW_TEST_ID))
        {
            Debug.LogError("No Test ID!");
        }
        string path = url + "api/patients/tests/" + PlayerPrefs.GetString(PlayerPrefsConst.PREF_NEW_TEST_ID) + "/results/";
        RestClient.Post<RecgoniseObjectResponse>(path, testDataPackage).Then(response =>
        {
            // I have no idea why i cant get the response for this call. Everything checks out, and the value is reflected correctly in the server @Malcom
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Json", JsonUtility.ToJson(response, true), "Ok");
#endif
        }).Catch((err) =>
        {
            RequestException error = err as RequestException;
            TestManagerScript.errList.Add(error);
            Debug.Log("Error: " + error.Response);

        }).Finally(() => { TestManagerScript.FinishSendingAllData(); });
    }
}

/**
 * 
 * Classes for Json Parsing
 * 
 */
[Serializable]
public class TestDataPackage
{
    public TMTTestData trail_making;
    public RecgoniseTestData picture_object_matching;
}


[Serializable]
public class NewTestResponse
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

[Serializable]
public class Register
{
    public string email;
    public string username;
    public string password;
    public string working_address;
    public string phone_number;
    public string birthday;
    public string user_role = "patient";
}

[Serializable]
public class RegisterResponse
{
    public string email;
    public string username;
    public string working_address;
    public string phone_number;
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class Errors
{
    public List<string> email { get; set; }
    public List<string> username { get; set; }
    public List<string> password { get; set; }
    public List<string> phone_number { get; set; }
    public List<string> message { get; set; }
}

public class Root
{
    public Errors errors { get; set; }
}

[Serializable]
public class RecgoniseObjectResponse
{
    public int id { get; set; }
    public int score { get; set; }
    public int errors { get; set; }
    public int time_taken { get; set; }
    public int date_time_completed { get; set; }
    public int game_test_id { get; set; }
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

        if (this.state == States.FULFILLED)
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