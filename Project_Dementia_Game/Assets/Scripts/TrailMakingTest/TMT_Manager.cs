using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TMT_Manager : MonoBehaviour
{

    public int numberNodes = 25;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private DrawManager drawManager;

    [SerializeField]
    private AudioClip correctSound;
    [SerializeField]
    private AudioClip wrongSound;
    [SerializeField]
    private AudioClip winSound;

    private AudioSource audioSource;

    [SerializeField]
    private bool isSceneTransitional;
    [SerializeField]
    private GameObject winScreen;

    private int currentNode = 1;
    private int hitNode = 0;
    private int previousNode = 0;
    private int errors = 0;
    private int score = 0;
    private float timer = 0.0f;
    private bool errorMode = true;

    public float bufferTimePastNode = 0.05f;
    private bool gameEnded = false;

    public List<int> nodesMissed;
    public List<GameObject> nodesHit;

    private TestManagerScript tms;
    private TMTTestData sendingTestData;

    private void Start()
    {
        nodesMissed = new List<int>();
        nodesHit = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
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

        float accuracy = ((float)(score - errors)) / numberNodes * 100;

        if (accuracy < 0)
            return 0;
        return accuracy;
    }

    public void UpdateMistakeNodeHit(GameObject node, float timeHit)
    {
        int nodeID = int.Parse(node.name);
        if (score != 0 && hitNode==nodeID && nodeID != previousNode && errorMode)
        {
            //Debug.Log(string.Format("Duration In Node: {0}, BufferTiming: {1}", timeHit, bufferTimePastNode));
            if (timeHit > bufferTimePastNode)
            {
                audioSource.clip = wrongSound;
                audioSource.Play();
                node.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                PreviousNodeBlinking();
                errors++;
                hitNode = 0;
                errorMode = false;
            }
        }
    }

    // When a mistake is made, the previous node will start blinking
    private void PreviousNodeBlinking()
    {
        if (nodesHit.Count > 0)
            nodesHit[nodesHit.Count - 1].GetComponent<NodeBehaviour>().StartBlinking();
    }

    private void StopPreviousNodeBlink()
    {
        if(nodesHit.Count > 0)
            nodesHit[nodesHit.Count - 1].GetComponent<NodeBehaviour>().StopBlinking();
    }

    private void EraseAllNodes()
    {
        foreach (GameObject l in nodesHit)
        {
            Destroy(l);
        }
        nodesHit.Clear();
    }

    public void NotifyNodeHit(GameObject node, float timeHit)
    {
        int nodeID = int.Parse(node.name);
        if (score < numberNodes)
        {
            if (nodeID == currentNode)
            {
                errorMode = true;
                StopPreviousNodeBlink();
                audioSource.clip = correctSound;
                audioSource.Play();
                score++;
                previousNode = currentNode;
                currentNode++;
                nodesHit.Add(node);
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

    private void EndGame() {

        audioSource.clip = winSound;
        audioSource.Play();

        sendingTestData.Score = score;
        sendingTestData.Errors = errors;
        sendingTestData.TimeTaken = (long)(Mathf.Round(timer * 100.0f) / 100.0f);
        sendingTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        Debug.Log("Button clicked:" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

        //Send test data results to TMS, tms will send all data once all test games are completed.
        tms.AddTestData(sendingTestData);

        if (isSceneTransitional) 
        {
            drawManager.allowDraw = false;
            drawManager.EraseAllLines();
            EraseAllNodes();
            winScreen.SetActive(true);
        }
    }

    public void NextTestTransition() 
    {
        SceneManager.LoadScene("RecgoniseGameScene", LoadSceneMode.Single);
    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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

    public void ResetTest()
    {
        drawManager.allowDraw = true;
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
}
