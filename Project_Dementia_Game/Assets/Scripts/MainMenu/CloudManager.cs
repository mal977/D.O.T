using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudManager : MonoBehaviour
{

    public GameObject cloudPrefab;

    public float timePerCloud = 3f;
    public int cloudLimit = 20;

    private float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime < timePerCloud)
        {
            currentTime += Time.deltaTime;
        }

        int cloudCount = GameObject.FindGameObjectsWithTag("cloud").Length;
        if(currentTime > timePerCloud && cloudCount < cloudLimit)
        {
            currentTime = 0f;

            int randomDirection = Random.Range(0, 2);
            Vector3 temp;
            if (randomDirection == 0)
            {
                temp = new Vector3(Screen.width, (float)(Screen.height * 0.60) + (Random.Range(0f, Screen.height / 5)), 10);
            }
            else
            {
                temp = new Vector3(0, (float)(Screen.height * 0.75) + (Random.Range(0f, Screen.height / 5)), 10);
            }
            Vector3 temp2 = Camera.main.ScreenToWorldPoint(temp);
            GameObject cloud = Instantiate(cloudPrefab, temp2,  Quaternion.identity);
            cloud.GetComponent<Cloud>().direction = randomDirection;
            cloud.GetComponent<Cloud>().speed = Random.Range(0.4f , 1.5f);
        }
    }
}
