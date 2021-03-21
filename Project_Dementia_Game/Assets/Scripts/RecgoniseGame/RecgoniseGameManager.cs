using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecgoniseGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RecgoniseObjects> recgoniseObjectList;
    public Sprite sprite;
    public GameObject spriteObject;
    public GameObject btn0Object;
    public GameObject btn1Object;
    public GameObject btn2Object;
    public GameObject btn3Object;

    public GameObject infoPanel;

    public GameObject resultText;
    public GameObject timerText;

    private RecgoniseObjects currentRecgoniseObject;
    private Image spriteRender;

    [SerializeField]
    private int numberOfRounds = 2;
    [SerializeField]
    private float secondsPerRound = 30f;  
    [SerializeField]
    private float lagTimeBetweenRound = 3f;

    // Sending Variables
    private RecgoniseTestData sendingTestData;

    private long startTime = 0;
    private Boolean timer = false;
    private float timeAmount = 0;

    private TestManagerScript tms;
    void Start()
    {

        if(tms == null)
        {
            tms = TestManagerScript.GetInstance();
        }

        if(sendingTestData == null)
        {
            sendingTestData = new RecgoniseTestData();
        }

        spriteRender = spriteObject.GetComponent<Image>();

        infoPanel.SetActive(true);
        StartCoroutine(StartGame());
        
    }

    void StartNewRound()
    {
        resultText.SetActive(false);

        timer = true;
        timerText.SetActive(true);
        timeAmount = secondsPerRound;

        int temp = UnityEngine.Random.Range(0, recgoniseObjectList.Count);
        currentRecgoniseObject = recgoniseObjectList[temp];
        recgoniseObjectList.RemoveAt(temp);
        PopulateNewObject(currentRecgoniseObject);

    }

    void EndCurrentRound(bool correctAnswer)
    {
        timer = false;
        timerText.SetActive(false);

        if (correctAnswer)
        {
            Debug.Log("Correct Answer!");
            sendingTestData.Score++;
            resultText.GetComponent<Text>().text = "Correct Answer!";
            resultText.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong Answer!");
            sendingTestData.Errors++;
            resultText.GetComponent<Text>().text = "Wrong Answer!";
            resultText.SetActive(true);
        }

        if (numberOfRounds > 0)
        {
            numberOfRounds--;
            StartCoroutine(DelayStartNewRound());

        }
        else
        {
            EndGame();
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        timerText.SetActive(true);
        spriteObject.SetActive(true);
        btn0Object.SetActive(true);
        btn1Object.SetActive(true);
        btn2Object.SetActive(true);
        btn3Object.SetActive(true);
        btn0Object.GetComponent<Button>().onClick.AddListener(() => BtnOnClick(0));
        btn1Object.GetComponent<Button>().onClick.AddListener(() => BtnOnClick(1));
        btn2Object.GetComponent<Button>().onClick.AddListener(() => BtnOnClick(2));
        btn3Object.GetComponent<Button>().onClick.AddListener(() => BtnOnClick(3));
        startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (numberOfRounds > 0)
        {
            numberOfRounds--;
            StartNewRound();

        }
    }

    void EndGame()
    {
        long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        sendingTestData.TimeTaken = (endTime - startTime)/1000; // convert to seconds for backend team
        sendingTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        Debug.Log("Button clicked:" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

        //Send test data results to TMS, tms will send all data once all test games are completed.
        tms.AddTestData(sendingTestData);
        StartCoroutine(DelayedLoadNextScene());
    }

    void PopulateNewObject(RecgoniseObjects recgoniseObjects)
    {
        spriteRender.sprite = recgoniseObjects.ObjectIcon;
        btn0Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.Option0;
        btn1Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.Option1;
        btn2Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.Option2;
        btn3Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.Option3;

        btn0Object.GetComponent<Button>().interactable = true;
        btn1Object.GetComponent<Button>().interactable = true;
        btn2Object.GetComponent<Button>().interactable = true;
        btn3Object.GetComponent<Button>().interactable = true;
    }

    void BtnOnClick(int number)
    {
        Debug.Log("Button clicked:" + number);
        //Turn off all buttons to prevent user from spamming button
        btn0Object.GetComponent<Button>().interactable = false;
        btn1Object.GetComponent<Button>().interactable = false;
        btn2Object.GetComponent<Button>().interactable = false;
        btn3Object.GetComponent<Button>().interactable = false; 
       
        if (number == currentRecgoniseObject.CorrectOption)
        {
           
            EndCurrentRound(true);
        }
        else
        {
          
            EndCurrentRound(false);
        }
    }
    IEnumerator DelayStartNewRound()
    {
        yield return new WaitForSeconds(lagTimeBetweenRound);
        StartNewRound();
    }
    IEnumerator DelayedLoadNextScene()
    {
        yield return new WaitForSeconds(lagTimeBetweenRound);
        loadNextScene();
    }

    void loadNextScene()
    {
        SceneManager.LoadScene("ResultScreen");

    }
    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timeAmount -= Time.deltaTime;
            timerText.GetComponent<Text>().text = string.Format("Time: {0}",Mathf.FloorToInt(timeAmount % 60));
            if(timeAmount <= 0f)
            {
                EndCurrentRound(false);

            }
        }
    }
}
