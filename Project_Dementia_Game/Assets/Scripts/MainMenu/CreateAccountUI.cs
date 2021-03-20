using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountUI : MonoBehaviour
{
    [SerializeField]
    private Button loginBtn;
    [SerializeField]
    private Button useridNextBtn;
    [SerializeField]
    private Button phoneBackBtn;
    [SerializeField]
    private Button phoneNextBtn;
    [SerializeField]
    private Button bdayBackBtn;
    [SerializeField]
    private Button signUpBtn;
    [SerializeField]
    private Animator createAccountAnimator;
    [SerializeField]
    private Animator mainMenuAnimator;

    void Start()
    {
        loginBtn.onClick.AddListener(() => OnLoginButtonPressed());
        useridNextBtn.onClick.AddListener(() => OnUserIdNextButtonPressed());
        phoneBackBtn.onClick.AddListener(() => OnPhoneBackButtonPressed());
        phoneNextBtn.onClick.AddListener(() => OnPhoneNextButtonPressed());
        bdayBackBtn.onClick.AddListener(() => OnBdayBackButtonPressed());
        signUpBtn.onClick.AddListener(() => OnSignUpButtonPressed());
    }

    private void OnLoginButtonPressed()
    {
        mainMenuAnimator.SetTrigger("create_account_close");
        mainMenuAnimator.SetTrigger("login_open");
    }

    private void OnUserIdNextButtonPressed()
    {
        createAccountAnimator.SetTrigger("userid_to_phone");
    }

    private void OnPhoneBackButtonPressed()
    {
        createAccountAnimator.SetTrigger("phone_to_userid");
    }

    private void OnPhoneNextButtonPressed()
    {
        createAccountAnimator.SetTrigger("phone_to_bday");
    }

    private void OnBdayBackButtonPressed()
    {
        createAccountAnimator.SetTrigger("bday_to_phone");
    }

    private void OnSignUpButtonPressed()
    {
        createAccountAnimator.SetTrigger("reset");
        // TODO: These 2 statements shouldn't be handled by this component, but the main menu manager instead for error processing handling
        mainMenuAnimator.SetTrigger("create_account_close");
        mainMenuAnimator.SetTrigger("login_open");
    }


}
