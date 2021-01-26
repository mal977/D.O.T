using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public float lineStartWidth = 0.1f;
    public float lineEndWidth = 0.1f;
    public Material lineMat;

    private Vector3 startLinePos;
    private int lineCounter = 0;

    public float lineDist = 1f;
    public float lineInterval = 0.5f;

    private float currentLineDist;

    // Start is called before the first frame update
    void Start()
    {
        currentLineDist = lineDist;
    }

    private void createNewLine(Touch touch)
    {
        GameObject newLine = new GameObject();
        newLine.transform.parent = gameObject.transform;
        lineRenderer = newLine.AddComponent<LineRenderer>();
        lineRenderer.material = lineMat;
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;
        newLine.name = "Line ";

        newLine.transform.position = startLinePos;
        lineRenderer.SetPositions(new Vector3[] { startLinePos, GetTouchPosition(touch) });
    }

    private Vector3 GetTouchPosition(Touch touch)
    {
        Vector3 currentTouch = Camera.main.ScreenToWorldPoint(touch.position);
        return new Vector3(currentTouch.x, currentTouch.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) 
            {
                startLinePos = GetTouchPosition(touch);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (currentLineDist > 0)
                {
                    currentLineDist -= lineInterval;
                }
                else
                {
                    createNewLine(touch);
                    startLinePos = GetTouchPosition(touch);
                    currentLineDist = lineDist;
                }
            }
        }
        
    }
}
