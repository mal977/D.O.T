using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingScript : MonoBehaviour
{

    [SerializeField] Vector3 from = new Vector3(0.0f,0f, 45.0f);
    [SerializeField] Vector3 to = new Vector3(0.0f, 0f,-45.0f);
    [SerializeField] float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion mFrom = Quaternion.Euler(this.from);
        Quaternion mTo = Quaternion.Euler(this.to);

        this.transform.localRotation = Quaternion.Lerp(mFrom, mTo, 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * speed)));
    }
}
