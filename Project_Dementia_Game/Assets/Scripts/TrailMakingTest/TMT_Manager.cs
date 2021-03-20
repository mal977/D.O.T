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
    private GenerateNodes nodeManager;

    [SerializeField]
    private GameObject tutorialScreen;

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

    [SerializeField]
    private GameObject startArrow;

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

    public void StartTestBtn()
    {
        tutorialScreen.SetActive(false);
        nodeManager.gameObject.SetActive(true);
    }

    /**
     * @Author Nicholas
     * This calculateAccuracy method calculate the current session node accuracy
     * 
     */
    private float calculateAccuracy() 
    {
        if (nodesMissed.Count > score)
            return 0;

        float accuracy = ((float)(score - errors)) / numberNodes * 100;

        if (accuracy < 0)
            return 0;
        return accuracy;
    }
    /**
     * @Author Nicholas
     * This method is called in DrawManager to check if the error node touched beyond a certain buffer time
     * This method allows user to draw past the nodes which are not the correct fast enough to the correct node
     * The criteria for an error to happen:
     * - score > 0
     * - HitNode is same as the recorded error node
     * - The current hitNode is not the node that is just been hit
     * - ErrorMode is true => ErrorMode turns false when an error is triggered in this method
     * - ErrorMode turns true when the correct node is hit
     * 
     */
    public void UpdateMistakeNodeHit(GameObject node, float timeHit)
    {
        int nodeID = int.Parse(node.name);
        if (score > 0 && hitNode==nodeID && nodeID != previousNode && errorMode)
        {
            // When the user is in this node for longer than the time stated, error will trigger
            if (timeHit > bufferTimePastNode)
            {
                // Show Next Node with arrow on top
                // Error Feedbacks
                audioSource.clip = wrongSound;
                audioSource.Play();
                // Current node turns red for error
                nodeManager.nodes[nodeID-1].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                ShowArrowOnCurrentNode(currentNode-2);
                NextNodeBlinking();

                //Update Error stats
                errors++;
                hitNode = 0;
                errorMode = false;
            }
        }
    }

    private void ShowArrowOnCurrentNode(int currentNodeID)
    {
        Vector2 nextNodePos = nodeManager.nodes[currentNodeID].transform.position;
        startArrow.transform.position = new Vector2(nextNodePos.x, nextNodePos.y + 1.0f);
        startArrow.SetActive(true);
    }

    // When a mistake is made, the previous node will start blinking
    private void NextNodeBlinking()
    {
        nodeManager.nodes[nodesHit.Count].GetComponent<NodeBehaviour>().StartBlinking();
    }

    // Stops the node from blinking
    private void StopNextNodeBlink()
    {
        nodeManager.nodes[nodesHit.Count].GetComponent<NodeBehaviour>().StopBlinking();
    }

    // Clears the screen of nodes for after game ends
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
                // Hide Tutorial Start Arrow
                startArrow.SetActive(false);
                errorMode = true;
                StopNextNodeBlink();
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
                // Records the node that is wrong, prepare for prolong error hit by user
                hitNode = nodeID;
            }
        }
    }

    // Records previous nodes missed to calculate accuracy
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

        // Logs test data into test data package
        sendingTestData.Score = score;
        sendingTestData.Errors = errors;
        sendingTestData.TimeTaken = (long)(Mathf.Round(timer * 100.0f) / 100.0f);
        sendingTestData.DateTimeCompleted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        Debug.Log("Button clicked:" + DateTimeOffset.Now.ToUnixTimeMilliseconds());

        //Send test data results to TMS, tms will send all data once all test games are completed.
        tms.AddTestData(sendingTestData);

        // Turns off all the setting, clear the game screens and activate win screen
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

    /**
     * @Author Nicholas
     * Updates mainly control the game duration when the game starts and game end controls
     * Constantly updates user game status
     * 
     */
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
        //scoreText.text =
        //    "Accuracy: " + Mathf.Round(calculateAccuracy() * 100.0f) / 100.0f +
        //    "%\nScore: " + score + "\nMistakes: " + errors +
        //    "\nTimer: " + Mathf.Round(timer * 100.0f) / 100.0f + "s";
        scoreText.text = "Timer: " + Mathf.Round(timer * 100.0f) / 100.0f + "s";

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
        scoreText.text = "Timer: 0.00s";
        nodesMissed.Clear();
        nodesHit.Clear();
    }
}
