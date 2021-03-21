using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNodes : MonoBehaviour
{
    [SerializeField]
    private float startArrow_yOffSet = 10.0f;
    [SerializeField]
    private GameObject startArrow;

    public GameObject nodePrefab;
    public GameObject testManager;
    public bool isAlphabetMode = false;

    // Random Based Nodes
    public float nodeGap = 0.85f;
    public float generateWidth = 2.0f;
    public float generateHeight = 4.0f;
    public float yOffSet = 4.0f;
    public float nodeScale = 0.6f;

    public GameObject[] nodes;

    private int numberNodes;
    private string[] alphaNodes = { "1", "A", "2", "B", "3", "C", "4", "D", "5", "E", "6", "F", "7", "G", "8", "H", "9", "I", "10", "J" };

    // Node generation checker to determine the rules to avoid overlapping circles and also trail making viable positions
    private Vector2[] generateNodeCoordinates()
    {
        float xCoor = 0f;
        float yCoor = 0f;
        bool isExist;
        Vector2[] nodePos = new Vector2[numberNodes];
        float nodeSize = nodePrefab.GetComponent<CircleCollider2D>().radius * nodePrefab.transform.localScale.x;
        int counter = 1000;
        for (int j = 0; j < numberNodes; j++)
        {
            do
            {
                xCoor = Random.Range(-generateWidth, generateWidth);
                yCoor = Random.Range(-generateHeight+ yOffSet, generateHeight+ yOffSet);
                isExist = false;
                for (int i = 0; i < numberNodes; i++)
                {
                    if (xCoor <= nodePos[i].x + (nodeSize + nodeGap) && xCoor >= nodePos[i].x - (nodeSize + nodeGap) &&
                        yCoor <= nodePos[i].y + (nodeSize + nodeGap) && yCoor >= nodePos[i].y - (nodeSize + nodeGap))
                    {
                        isExist = true;
                    }
                }

                counter--;
                if (counter == 0)
                {
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
        numberNodes = testManager.GetComponent<TMT_Manager>().numberNodes;
        if (nodes != null)
        {
            foreach (GameObject n in nodes)
            {
                Destroy(n);
            }
        }
        nodes = new GameObject[numberNodes];

        Vector2[] nodeCoor = generateNodeCoordinates();

        int nodeCounter = 0;
        nodePrefab.transform.localScale = new Vector3(nodeScale, nodeScale, nodeScale);
        for (int nodesCount = 0; nodesCount < numberNodes; nodesCount++)
        {
            if (nodesCount == 0)
                ShowStartArrow(nodeCoor[nodesCount]);
            nodes[nodeCounter] = Instantiate(nodePrefab, new Vector3(nodeCoor[nodesCount].x, nodeCoor[nodesCount].y, 1.0f), Quaternion.identity);
            nodes[nodeCounter].name = "" + (nodeCounter + 1);
            if(!isAlphabetMode)
                nodes[nodeCounter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + (nodeCounter + 1);
            else
                nodes[nodeCounter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = alphaNodes[nodeCounter];
            nodes[nodeCounter].transform.parent = gameObject.transform;
            nodeCounter++;
        }
    }

    private void ShowStartArrow(Vector2 firstNodePos)
    {
        startArrow.transform.position = new Vector3(firstNodePos.x, firstNodePos.y + startArrow_yOffSet, -1.0f); 
        startArrow.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        testManager.GetComponent<TMT_Manager>();
        generateNodesOnRandom();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
