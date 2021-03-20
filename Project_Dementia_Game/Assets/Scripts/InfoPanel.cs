using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(DestroyWithDelay(3f));
    }

    IEnumerator DestroyWithDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(this.gameObject);
    }
}
