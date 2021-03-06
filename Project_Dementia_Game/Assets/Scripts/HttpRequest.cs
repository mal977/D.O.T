using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    public TestDataPlaceHolder pH;
    public string testData;
    private string postURL = "http://localhost:8000/testServer/example/";
    private UnityWebRequest requestSender;
    //private string 

    public string getURL = "www.google.com";

    void Start()
    {
        // TODO: To be removed
        pH = new TestDataPlaceHolder();
        testData = JsonParser(pH);
        // END TODO
    }


    public async void GetRequest(string url)
    {
        UnityWebRequestAsyncOperation asyncRequest;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            asyncRequest = webRequest.SendWebRequest();
            while (!asyncRequest.isDone)
                await Task.Delay(100);
            string[] pages = url.Split('/');
            int page = pages.Length - 1;
            Debug.Log(asyncRequest.ToString());
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                //return webRequest.downloadHandler.text;
            }
        }
        //return "";
    }
    public string GetData(string url)
    {
        GetRequest(postURL);
        return "";
    }

    public void UploadTest()
    {
        Upload();
    }

    private UnityWebRequest CreateSendRequest(string url, string data)
    {

        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        byte[] jsonDataRaw = Encoding.UTF8.GetBytes(data);
        UploadHandler uploader = new UploadHandlerRaw(jsonDataRaw);
        DownloadHandler downloader = new DownloadHandlerBuffer();

        request.uploadHandler = uploader;
        request.downloadHandler = downloader;
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    void Upload()
    {
        requestSender = CreateSendRequest(postURL, testData);

        requestSender.SendWebRequest();

        if (requestSender.isNetworkError || requestSender.isHttpError)
        {
            Debug.Log(requestSender.error);
        }
        else
        {
            Debug.Log("Upload Completed!");
        }
    }

    //Parse Test Object to Json Format
    private string JsonParser(TestDataPlaceHolder obj)
    {
        return JsonUtility.ToJson(obj);
    }

}
