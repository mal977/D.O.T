using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMT_Manager : MonoBehaviour
{
    public int numberNodes = 25;
    public Text scoreText;
    [SerializeField]
    private bool isSceneTransitional;

    private int currentNode = 1;
    private int hitNode = 0;
    private int previousNode = 0;
    private int errors = 0;
    private int score = 0;
    private float timer = 0.0f;

    public float minTimeInNode = 0.01f;
    public float bufferTimePastNode = 0.05f;
    private bool gameEnded = false;

    public List<int> nodesMissed;
    public List<int> nodesHit;

    private TestManagerScript tms;
    private TMTTestData sendingTestData;

    private void Start()
    {
        nodesMissed = new List<int>();
        nodesHit = new List<int>();
        if (tms == null)
        {
            tms = TestManagerScript.GetInstance();
        }

        if (sendingTestData == null)
        {
            sendingTestData = new TMTTestData();
        }
    }

    private float calculateAccuracy() 
    {
        if (nodesMissed.Count > score)
            return 0;
        return ((float)(score - errors))/numberNodes * 100;
    }

    public void UpdateMistakeNodeHit(GameObject node, float timeHit)
    {
        int nodeID = int.Parse(node.name);
        if (score != 0 && hitNode==nodeID)
        {
            //Debug.Log(string.Format("Duration In Node: {0}, BufferTiming: {1}", timeHit, bufferTimePastNode));
            if (timeHit > bufferTimePastNode)
            {
                node.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                //Debug.Log("Mistakes with time:" + timeHit);
                errors++;
                hitNode = 0;
            }
        }
    }

    public void NotifyNodeHit(GameObject node, float timeHit)
    {
        int nodeID = int.Parse(node.name);
        if (score < numberNodes)
        {
            if (nodeID == currentNode)
            {
                score++;
                previousNode = currentNode;
                currentNode++;
                nodesHit.Add(nodeID);
                node.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            }
            else 
            {
                hitNode = nodeID;
            }
        }
    }
    private void generateNodeMissed(int nodeID)
    {
        for(int i = currentNode+1; i < nodeID; i++) 
        {
            nodesMissed.Add(i);
        }
    }

    public void ResetTest()
    {
        gameEnded = false;
        score = 0;
        errors = 0;
        timer = 0;
        currentNode = 1;
        previousNode = 0;
        scoreText.text = "Accuracy: 0.0%\nScore: 0\nMistakes: 0";
        nodesMissed.Clear();
        nodesHit.Clear();
    }

    private void EndGame() {
        sendingTestData.Score = score;
        sendingTestData.Errors = errors;
        sendingTestData.TimeTaken = (long)(Mathf.Round(timer * 100.0f) / 100.0f);
        sendingTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        Debug.Log("Button clicked:" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

        //Send test data results to TMS, tms will send all data once all test games are completed.
        tms.AddTestData(sendingTestData);

        if (isSceneTransitional)
            SceneTransition();
    }

    private void SceneTransition() 
    {
        
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (score < numberNodes)
        {
            if (score > 0)
            {
                timer += Time.deltaTime;
            }
        }
        else 
        {
            if (!gameEnded) {
                gameEnded = true;
                EndGame();
            }    
        }
        scoreText.text =
            "Accuracy: " + Mathf.Round(calculateAccuracy() * 100.0f) / 100.0f +
            "%\nScore: " + score + "\nMistakes: " + errors +
            "\nTimer: " + Mathf.Round(timer * 100.0f) / 100.0f + "s";

    }
}
