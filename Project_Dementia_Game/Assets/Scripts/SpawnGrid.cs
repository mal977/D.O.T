using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGrid : MonoBehaviour
{
    public GameObject nodePrefab;

    // Grid Based Nodes
    public int rowGap = 5;
    public int colGap = 5;
    public int gridSize = 3;

    // Random Based Nodes
    public int numberNodes = 25;
    public float nodeGap = 0.85f;
    public float generateWidth = 2.0f;
    public float generateHeight = 4.0f;


    private GameObject[] nodes;
    private Collider2D collider2D;

    // Placeholder Node generation based on grid system for trail making test
    private void generateNodesOnGrid()
    {
        nodes = new GameObject[gridSize * gridSize];
        int centerOffset = gridSize / 2;
        int nodeCounter = 0;
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                nodes[nodeCounter] = Instantiate(nodePrefab, new Vector3((col - centerOffset) * colGap, (-(row - centerOffset)) * rowGap, 0), Quaternion.identity);
                nodes[nodeCounter].name = "node x: " + (col - centerOffset) + "/y: " + (row - centerOffset);
                nodes[nodeCounter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + (nodeCounter + 1);
                nodeCounter++;
            }
        }
    }

    // Node generation checker to determine the rules to avoid overlapping circles and also trail making viable positions
    private Vector2[] generateNodeCoordinates()
    {
        float xCoor = 0f;
        float yCoor = 0f;
        bool isExist;
        Vector2[] nodePos = new Vector2[numberNodes];
        int counter = 1000;

        for (int j = 0; j < numberNodes; j++) 
        {

            do
            {
                xCoor = Random.Range(-generateWidth, generateWidth);
                yCoor = Random.Range(-generateHeight, generateHeight);
                isExist = false;
                for (int i = 0; i < numberNodes; i++)
                {
                    if (xCoor <= nodePos[i].x + nodeGap && xCoor >= nodePos[i].x - nodeGap &&
                        yCoor <= nodePos[i].y + nodeGap && yCoor >= nodePos[i].y - nodeGap)
                    {
                        isExist = true;
                    }
                }

                counter--;
                if (counter == 0) {
                    Debug.Log("Broke");
                }
            } while (isExist && counter > 0);
            counter = 1000;
            nodePos[j].x = xCoor;
            nodePos[j].y = yCoor;
        }
        
        return nodePos;
    }

    // Generate nodes on screen
    public void generateNodesOnRandom()
    {

        if (nodes[0] != null)
        {
            foreach(GameObject n in nodes)
            {
                Destroy(n);
            }
            nodes = new GameObject[numberNodes];
        }

        Vector2[] nodeCoor = generateNodeCoordinates();

        int nodeCounter = 0;
        for (int nodesCount = 0; nodesCount < numberNodes; nodesCount++)
        {
            nodes[nodeCounter] = Instantiate(nodePrefab, new Vector3(nodeCoor[nodesCount].x, nodeCoor[nodesCount].y, 0), Quaternion.identity);
            nodes[nodeCounter].name = "node x: " + nodeCoor[nodesCount].x + "/y: " + nodeCoor[nodesCount].y;
            nodes[nodeCounter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + (nodeCounter + 1);
            nodeCounter++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider2D = new Collider2D();
        nodes = new GameObject[numberNodes];
        generateNodesOnRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Vector2 pos = touch.position;
                Debug.Log(pos);
                //if (collider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos)))
                //{
                //    Debug.Log("Hello");
                //}
            }
        }    
    }
}
