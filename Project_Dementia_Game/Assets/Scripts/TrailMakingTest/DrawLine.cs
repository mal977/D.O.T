using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    
    private LineRenderer lineRenderer;

    // Draw line properties
    public float lineStartWidth = 0.1f;
    public float lineEndWidth = 0.1f;
    public Material lineMat;
    public Color lineColor;

    private Vector3 startLinePos;

    // Line interval properties
    public float lineInterval = 0.5f;
    private float timer = 0.0f;

    private float currentLineDist;

    private List<GameObject> allLines;

    // Start is called before the first frame update
    void Start()
    {
        allLines = new List<GameObject>();
    }

    // Create new line is called after the previous touch position has been registered
    // Creates a new line from the previous touch position to latest
    private void createNewLine(Touch touch)
    {
        GameObject newLine = new GameObject();
        newLine.transform.parent = gameObject.transform;
        lineRenderer = newLine.AddComponent<LineRenderer>();
        lineRenderer.material = lineMat;
        lineRenderer.SetColors(lineColor, lineColor);
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;
        newLine.name = "Line ";

        newLine.transform.position = startLinePos;
        allLines.Add(newLine);
        lineRenderer.SetPositions(new Vector3[] { startLinePos, GetTouchPosition(touch) });
    }

    // Clear all lines objects in scene
    public void EraseAllLines()
    {
        foreach (GameObject l in allLines)
        {
            Destroy(l);
        }
        allLines.Clear();
    }

    // Returns touch position based on world screen space
    private Vector3 GetTouchPosition(Touch touch)
    {
        Vector3 currentTouch = Camera.main.ScreenToWorldPoint(touch.position);
        return new Vector3(currentTouch.x, currentTouch.y, 0);
    }

    // Update is called once per frame
    // One finger only draw lines and update screens
    void Update()
    {
        if (Input.touchCount > 0)
        {
            timer += Time.deltaTime;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) 
            {
                // Without creating a line, the initial touch position is recorded
                startLinePos = GetTouchPosition(touch);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                // Based on a short interval, the user's draw movement will register and draw the next line position
                if (timer > lineInterval)
                {
                    createNewLine(touch);
                    // Prepare current touch position for the next interval line
                    startLinePos = GetTouchPosition(touch);
                    timer = 0.0f;
                }
            }
        }
        
    }
}
