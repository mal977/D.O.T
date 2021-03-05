using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMT_Manager : MonoBehaviour
{
    public int numberNodes = 25;
    public Text scoreText;

    private int currentNode = 1;
    private int previousNode = 0;
    private int mistakes = 0;
    private int score = 0;
    private float timer = 0.0f;

    public float maxTimeInNode = 0.08f;
    public float bufferTimePastNode = 0.05f;
    private float curretMistakeBufferTime = 0.0f;

    public List<int> nodesMissed;
    public List<int> nodesHit;


    private float calculateAccuracy() 
    {
        if (nodesMissed.Count > score)
            return 0;
        return ((float)(score - mistakes))/numberNodes * 100;
    }

    public void NotifyNodeHit(int nodeID)
    {
        if (score < numberNodes)
        {
            if (nodeID == currentNode)
            {
                score++;
                previousNode = currentNode;
                currentNode++;
                nodesHit.Add(nodeID);
            }
            else
            {
                //generateNodeMissed(nodeID);
                if (score != 0 && previousNode != nodeID)
                   mistakes++;
            }
        }
    }


    public void NotifyNodeHit(int nodeID, float timeHit)
    {
        if (score < numberNodes)
        {
            if (nodeID == currentNode)
            {
                score++;
                previousNode = currentNode;
                currentNode++;
                nodesHit.Add(nodeID);
                curretMistakeBufferTime = 0.0f;
            }
            else
            {
                //generateNodeMissed(nodeID);
                if (score != 0 && previousNode != nodeID) 
                {
                    if(timeHit > bufferTimePastNode)
                    {
                        Debug.Log("Mistakes with time:" + timeHit);
                        mistakes++;
                    }
                }
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
        score = 0;
        mistakes = 0;
        timer = 0;
        currentNode = 1;
        previousNode = 0;
        scoreText.text = "Accuracy: 0.0%\nScore: 0\nMistakes: 0";
        nodesMissed.Clear();
        nodesHit.Clear();
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void Start()
    {
        nodesMissed = new List<int>();
        nodesHit = new List<int>();
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
        scoreText.text =
            "Accuracy: " + Mathf.Round(calculateAccuracy() * 100.0f) / 100.0f +
            "%\nScore: " + score + "\nMistakes: " + mistakes +
            "\nTimer: " + Mathf.Round(timer * 100.0f) / 100.0f + "s";

    }
}
