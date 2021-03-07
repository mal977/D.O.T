using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    // TODO: Change to TestData Object
    public TestDataPlaceHolder pH;
    public string testData;
    private UnityWebRequest requestSender;
    private UnityWebRequestAsyncOperation asyncRequest;

    public string getURL = "www.google.com";
    private string hostURL = "localhost:8000";

    void Start()
    {
        // TODO: To be removed
        //pH = new TestDataPlaceHolder();
        // JSON Parser for parsing objects into json <== Reference
        testData = Utility.JsonParser(pH);
        // END TODO
    }


    public async Task<string> GetRequest(string urlPath)
    {
        // Merge hostURL with path
        string getterURL = hostURL + urlPath;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(getterURL))
        {
            // Request and wait for the desired page.
            asyncRequest = webRequest.SendWebRequest();
            while (!asyncRequest.isDone)
                await Task.Delay(100);
            // To be updated to parse the format from the API
            string[] pages = getterURL.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log("error");
                return "error";
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                return webRequest.downloadHandler.text;
            }
        }
    }

    private UnityWebRequest CreateWebSender(string url, string data)
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

    public async Task<string> Upload(string urlPath, string testData)
    {
        // Merge hostURL with path
        string getterURL = hostURL + urlPath;

        requestSender = CreateWebSender(getterURL, testData);

        asyncRequest = requestSender.SendWebRequest();
        while (!asyncRequest.isDone)
            await Task.Delay(100);

        if (requestSender.isNetworkError || requestSender.isHttpError)
        {
            Debug.Log(requestSender.error);
        }
        else
        {
            Debug.Log("Upload Completed!");
        }
        return requestSender.downloadHandler.text;
    }
}
