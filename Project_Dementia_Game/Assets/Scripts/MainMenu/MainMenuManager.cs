using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startGameButton;

    public GameObject animationController;

    // Login Screen
    [Header("Login Input Fields")]
    public GameObject login_username_textfield;
    public GameObject login_password_textfield;
    public GameObject login_button;

    // CreateAccount Screen
    [Header("CreateAccount Input Fields")]
    public GameObject create_email_textfield;
    public GameObject create_username_textfield;
    public GameObject create_password_textfield;
    public GameObject create_password_confirm_textfield;
    public GameObject create_address_textfield;
    public GameObject create_phonen_number_textfield;
    public GameObject create_button;

    Animator m_Animator;
    void Start()
    {

        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartTests());
        create_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => CreateAccount());
        login_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Login());

        m_Animator = animationController.GetComponent<Animator>();

        if (CheckForPlayerLoggedIn())
        {
            Debug.Log("Current State" + m_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
            m_Animator.SetTrigger("home_open");
        }
        else
        {
            m_Animator.SetTrigger("login_open");
        }
    }

    void StartTests()
    {
        SceneManager.LoadScene("RecgoniseGameScene");
    }

    void Login()
    {
        String username = login_username_textfield.GetComponent<InputField>().text;
        String password = login_password_textfield.GetComponent<InputField>().text;

        Debug.Log("Username: " + username + " Password: " + password);

        //if Login successful trigger transition to home screen
        m_Animator.SetTrigger("login_close");
        m_Animator.SetTrigger("home_open");
    }

    void CreateAccount()
    {

        String email = create_email_textfield.GetComponent<InputField>().text;
        String username = create_username_textfield.GetComponent<InputField>().text;
        String password = create_password_textfield.GetComponent<InputField>().text;
        String password_confirm = create_password_confirm_textfield.GetComponent<InputField>().text;
        String address = create_address_textfield.GetComponent<InputField>().text;
        String phonen_number = create_phonen_number_textfield.GetComponent<InputField>().text;

        String debugMessage = String.Format("Email: {0} Username: {1} Password: {2} PasswordConfirm: {3} Address: {4} PhoneNumber: {5}", email, username, password, password_confirm, address, phonen_number);
        Debug.Log(debugMessage);

        //if CreateAccount successful trigger transition to log in screen
        m_Animator.SetTrigger("create_account_close");
        m_Animator.SetTrigger("login_open");
    }

    Boolean CheckForPlayerLoggedIn()
    {
        if (PlayerPrefs.HasKey("user_cookie"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
