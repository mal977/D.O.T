using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuErrorFeedback : MonoBehaviour
{
    [SerializeField]
    private GameObject errorPanel;
    [SerializeField]
    private Text errorMessageText;
    [SerializeField]
    private float errorPanelTimer = 4;
    [SerializeField]
    private float fadingStartTimer = 3;
    [SerializeField]
    private bool isTimerHidePanel = true;

    private float timer = 0;

    public void DisplayError(string errorMsg) 
    {
        errorMessageText.text = errorMsg;
        errorPanel.SetActive(true);
    }

    private void Update()
    {
        if (errorPanel.activeSelf && isTimerHidePanel) 
        {
            timer += Time.deltaTime;
            if (timer >= fadingStartTimer) 
            {
                errorPanel.GetComponent<Animator>().SetTrigger("OnFadeOut");
            }
            if (timer >= errorPanelTimer) 
            {
                errorPanel.SetActive(false);
                timer = 0;
            }
        }
    }

    public void HideErrorDisplay()
    {
        errorPanel.SetActive(false);
        errorMessageText.text = "";
    }
}
