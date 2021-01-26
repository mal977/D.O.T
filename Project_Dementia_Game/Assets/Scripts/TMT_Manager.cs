using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMT_Manager : MonoBehaviour
{
    private int currentNode = 1;
    public int numberNodes = 25;
    public Text scoreText;
    private int mistakes = 0;
    private int score = 0;

    private float calculateAccuracy() 
    {
        if (mistakes > score)
            return 0;
        return ((float)(score - mistakes))/numberNodes * 100;
    }

    public void NotifyNodeHit(int nodeID)
    {
        Debug.Log(nodeID + " HIT");
        if (nodeID == currentNode)
        {
            score++;
            currentNode++;
        }
        else {
            mistakes++;
        }
        scoreText.text = "Accuracy: " + calculateAccuracy() + "%\nScore: " + score + "\nMistakes: " + mistakes;
    }

    public void ResetTest()
    {
        score = 0;
        mistakes = 0;
        currentNode = 1;
        scoreText.text = "Accuracy: 0.0%\nScore: 0\nMistakes: 0";
    }

    public void ExitApp()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
