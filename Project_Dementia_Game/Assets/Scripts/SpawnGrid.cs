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
    public int numberNodes = 9;


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
    private Vector2[] generateNodeCoordinates(int randomRange)
    {
        float xCoor = 0;
        float yCoor = 0;
        bool isExist;
        Vector2[] nodePos = new Vector2[numberNodes];

        for (int j = 0; j < numberNodes; j++) 
        {

            do
            {
                xCoor = Random.Range(-randomRange, randomRange);
                yCoor = Random.Range(-randomRange, randomRange);
                isExist = false;
                for (int i = 0; i < numberNodes; i++)
                {
                    if (nodePos[i].x == xCoor && nodePos[i].y == yCoor)
                    {
                        isExist = true;
                    }
                }
            } while (isExist);

            nodePos[j].x = xCoor;
            nodePos[j].y = yCoor;
        }
        
        return nodePos;
    }

    // Generate nodes on screen
    private void generateNodesOnRandom()
    {
        nodes = new GameObject[numberNodes];

        int randomRange = numberNodes / 2 - 1;
        Vector2[] nodeCoor = generateNodeCoordinates(randomRange);

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
