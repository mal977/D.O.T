using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHoverScript : MonoBehaviour
{
    public Vector2 arrowPos = new Vector2(0,0);
    private float peak = 1.0f;
    private bool isPeak = false;
    private float animationRate = 0.1f;
    private float currentY = 0;
    private float hoverY_pos = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(!isPeak)
            currentY = arrowPos.y + hoverY_pos + Time.deltaTime * animationRate;
        else
            currentY = arrowPos.y + hoverY_pos - Time.deltaTime * animationRate;
        if (currentY >= arrowPos.y + hoverY_pos + peak)
            isPeak = true;
        else
            isPeak = false;
        GetComponent<Transform>().position = new Vector3(arrowPos.x, currentY, -1.0f);
    }
}
