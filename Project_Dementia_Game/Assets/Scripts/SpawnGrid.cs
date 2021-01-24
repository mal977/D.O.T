using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGrid : MonoBehaviour
{
    public GameObject nodePrefab;
    public int rowGap = 5;
    public int colGap = 5;
    public int gridSize = 3;
    public GameObject[] nodes;
    private Collider2D collider2D;


    // Start is called before the first frame update
    void Start()
    {
        collider2D = new Collider2D();
        nodes = new GameObject[gridSize * gridSize];
        int centerOffset = gridSize / 2;
        int nodeCounter = 0;
        for (int row = 0; row < gridSize; row++) 
        {
            for (int col = 0; col < gridSize; col++)
            {
                nodes[nodeCounter] = Instantiate(nodePrefab, new Vector3((col - centerOffset) * colGap, (-(row - centerOffset)) * rowGap, 0), Quaternion.identity);
                nodes[nodeCounter].name = "node x: " + (col- centerOffset) + "/y: " + (row - centerOffset);
                nodes[nodeCounter].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ""+(nodeCounter+1);
                nodeCounter++;
            }
        }
            
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
