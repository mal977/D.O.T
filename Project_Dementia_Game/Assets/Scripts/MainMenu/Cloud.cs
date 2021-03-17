using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public int direction = 0; // left = 0, right = 1;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        Vector2 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        if(screenPos.x < -50 && direction == 0 || screenPos.x > Screen.width + 50 && direction == 1 )
        {
            Destroy(gameObject);
        }
    }
}
