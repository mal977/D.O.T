using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TestUpload() 
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        string myData = "Hello World Here, will be replaced with JSON data";
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8000/testServer/example/", myData);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Upload Completed!");
        }
    }
    /*IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }*/
}
