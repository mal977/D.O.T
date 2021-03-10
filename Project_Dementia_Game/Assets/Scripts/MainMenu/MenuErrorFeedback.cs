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

    public void DisplayError(string errorMsg) 
    {
        errorMessageText.text = errorMsg;
        errorPanel.SetActive(true);
    }

    public void HideErrorDisplay()
    {
        errorPanel.SetActive(false);
        errorMessageText.text = "";
    }
}
