using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnNodeTouch : MonoBehaviour
{
    private Collider2D collider;
    private bool allowEntry = true;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
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
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                if (collider == Physics2D.OverlapPoint(GetTouchPosition(touch)))
                {
                    if (allowEntry)
                    {
                        gameObject.transform.parent.GetComponent<GenerateNodes>().testManager.GetComponent<TMT_Manager>().NotifyNodeHit(int.Parse(gameObject.name));
                        allowEntry = false;
                    }
                }
                else {
                    allowEntry = true;
                }
            }
        }
    }
}
