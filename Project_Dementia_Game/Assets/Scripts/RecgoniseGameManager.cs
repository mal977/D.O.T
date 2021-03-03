using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    // Sending Variables
    private int mistakes = 0;
    private int score = 0;
    private long timeTaken = 0;
    private string dateTimeCompleted = "";

    private long startTime = 0;
    private Boolean timer = false;
    private float timeAmount = 0;

    void Start()
    {

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

    void endCurrentRound()
    {
        timer = false;
        timerText.SetActive(false);
    }

    void StartGame()
    {
        startTime = System.DateTime.Now.Millisecond;
        if (numberOfRounds > 0)
        {
            numberOfRounds--;
            StartNewRound();

        }
    }

    void EndGame()
    {
        long endTime = System.DateTime.Now.Millisecond;
        timeTaken = endTime - startTime;
    }

    void PopulateNewObject(RecgoniseObjects recgoniseObjects)
    {
        spriteRender.sprite = recgoniseObjects.Icon;
        btn0Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.optionOne;
        btn1Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.optionTwo;
        btn2Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.optionThree;
        btn3Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjects.optionFour;
    }

    void BtnOnClick(int number)
    {
        Debug.Log("Button clicked:" + number);
        if(number == currentRecgoniseObject.getCorrectOption)
        {
            Debug.Log("Correct Answer!");
            resultText.GetComponent<Text>().text = "Correct Answer!";
            resultText.SetActive(true);
            endCurrentRound();
        }
        else
        {
            Debug.Log("Wrong Answer!");
            resultText.GetComponent<Text>().text = "Wrong Answer!";
            resultText.SetActive(true);
            endCurrentRound();
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
      
        yield return new WaitForSeconds(5f);
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
