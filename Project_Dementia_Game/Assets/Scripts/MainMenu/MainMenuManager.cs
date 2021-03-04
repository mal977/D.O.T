using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startGameButton;

    void Start()
    {

        startGameButton.GetComponent<Button>().onClick.AddListener(()=>StartTests());
        startGameButton.transform.GetChild(0).GetComponent<Text>().text = "Help Me";
    }

    void StartTests()
    {
        SceneManager.LoadScene("RecgoniseGameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
