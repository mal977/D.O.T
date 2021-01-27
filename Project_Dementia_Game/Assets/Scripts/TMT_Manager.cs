using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMT_Manager : MonoBehaviour
{
    public int numberNodes = 25;
    public Text scoreText;

    private int currentNode = 1;
    private int mistakes = 0;
    private int score = 0;
    private float timer = 0.0f;

    private float calculateAccuracy() 
    {
        if (mistakes > score)
            return 0;
        return ((float)(score - mistakes))/numberNodes * 100;
    }

    public void NotifyNodeHit(int nodeID)
    {
        Debug.Log(nodeID + " HIT");
        if (score < numberNodes)
        {
            if (nodeID == currentNode)
            {
                score++;
                currentNode++;
            }
            else
            {
                if(score != 0)
                    mistakes++;
            }
        }
    }

    public void ResetTest()
    {
        score = 0;
        mistakes = 0;
        timer = 0;
        currentNode = 1;
        scoreText.text = "Accuracy: 0.0%\nScore: 0\nMistakes: 0";
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
            scoreText.text = 
                "Accuracy: " + Mathf.Round(calculateAccuracy() * 100.0f) / 100.0f + 
                "%\nScore: " + score + "\nMistakes: " + mistakes +
                "\nTimer: " + Mathf.Round(timer * 100.0f) / 100.0f + "s";
        }

    }
}
