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

        btn0Object.GetComponent<Button>().onClick.AddListener(()=>BtnOnClick(0));
        btn1Object.GetComponent<Button>().onClick.AddListener(()=>BtnOnClick(1));
        btn2Object.GetComponent<Button>().onClick.AddListener(()=>BtnOnClick(2));
        btn3Object.GetComponent<Button>().onClick.AddListener(()=>BtnOnClick(3));

        StartGame();
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

    void EndCurrentRound()
    {
        timer = false;
        timerText.SetActive(false);
    }

    void StartGame()
    {
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

        sendingTestData.TimeTaken = endTime - startTime;
        sendingTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        Debug.Log("Button clicked:" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

        //Send test data results to TMS, tms will send all data once all test games are completed.
        tms.AddTestData(sendingTestData);

        SceneManager.LoadScene("ResultScreen");
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
            Debug.Log("Correct Answer!");
            sendingTestData.Score++;
            resultText.GetComponent<Text>().text = "Correct Answer!";
            resultText.SetActive(true);
            EndCurrentRound();
        }
        else
        {
            Debug.Log("Wrong Answer!");
            sendingTestData.Errors++;
            resultText.GetComponent<Text>().text = "Wrong Answer!";
            resultText.SetActive(true);
            EndCurrentRound();
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
    IEnumerator DelayStartNewRound()
    {
      
        yield return new WaitForSeconds(lagTimeBetweenRound);
        StartNewRound();
    }
    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timeAmount -= Time.deltaTime;
            timerText.GetComponent<Text>().text = string.Format("Time: {0}",Mathf.FloorToInt(timeAmount % 60));
        }
    }
}
