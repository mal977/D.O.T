using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnNodeTouch : MonoBehaviour
{
    private bool allowEntry = true;
    private float timerInNode = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 GetTouchPosition(Touch touch)
    {
        Vector3 currentTouch = Camera.main.ScreenToWorldPoint(touch.position);
        return new Vector3(currentTouch.x, currentTouch.y, 0);
    }

    private TMT_Manager getTestManager()
    {
        return gameObject.transform.parent.GetComponent<GenerateNodes>().testManager.GetComponent<TMT_Manager>();
    }    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(GetTouchPosition(touch)))
            {
                timerInNode += Time.deltaTime;
                if (allowEntry)
                {
                    if (timerInNode >= getTestManager().maxTimeInNode)
                    {
                        getTestManager().NotifyNodeHit(int.Parse(gameObject.name), timerInNode);
                        //Debug.Log("Time taken in Node:" + timerInNode);
                        timerInNode = 0.0f;
                        allowEntry = false;
                    }
                }
            }
            else
            {
                allowEntry = true;
            }
        }
    }
}
