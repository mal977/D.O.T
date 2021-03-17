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
    public GameObject exit_button;

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
    public GameObject create_phone_number_textfield;
    public GameObject create_button;
    public GameObject back_button;

    public GameObject main_menu_loading_icon;

    [Header("Debug Options")]
    [SerializeField]
    Boolean stayOnLoginScreen = false;

    Animator m_Animator;
    private HttpHelper httpHelper;
    void Start()
    {
        httpHelper = HttpHelper.GetInstance();

        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartTests());
        create_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => CreateAccount());
        login_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Login());
        back_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BackToLogin());
        exit_button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ExitApp());

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
        main_menu_loading_icon.SetActive(true);
        httpHelper.StartNewTest(() =>
        {
            main_menu_loading_icon.SetActive(false);
            //Only if we are succesful in getting a new test id, then we can start the tests
            SceneManager.LoadScene("TMT", LoadSceneMode.Single);
        },(errorMessage)=> {

            main_menu_loading_icon.SetActive(false);
            //TODO: Add action when http fails here.
            GetComponent<MenuErrorFeedback>().DisplayError(errorMessage);
        });
    }

    void Login()
    {
        String username = login_username_textfield.GetComponent<InputField>().text;
        String password = login_password_textfield.GetComponent<InputField>().text;

        if (username == "" || password == "")
        {
            GetComponent<MenuErrorFeedback>().DisplayError("Please fill in username and password!");
            return;
        }

        Debug.Log("Username: " + username + " Password: " + password);
        httpHelper.Login(username, password, () =>
         {
             //if Login successful trigger transition to home screen
             m_Animator.SetTrigger("login_close");
             m_Animator.SetTrigger("home_open");
         }, (errorMessage) => {
             GetComponent<MenuErrorFeedback>().DisplayError(errorMessage);
         });

    }

    void CreateAccount()
    {
        String email = create_email_textfield.GetComponent<InputField>().text;
        String username = create_username_textfield.GetComponent<InputField>().text;
        String password = create_password_textfield.GetComponent<InputField>().text;
        String password_confirm = create_password_confirm_textfield.GetComponent<InputField>().text;
        String address = create_address_textfield.GetComponent<InputField>().text;
        String phone_number = create_phone_number_textfield.GetComponent<InputField>().text;


        if (email == "" || username == "" || password == "" || password_confirm == "" || address == "" || phone_number == "") 
        {
            GetComponent<MenuErrorFeedback>().DisplayError("Please fill in all the blanks!");
            return;
        }
        String debugMessage = String.Format("Email: {0} Username: {1} Password: {2} PasswordConfirm: {3} Address: {4} PhoneNumber: {5}", email, username, password, password_confirm, address, phone_number);
        Debug.Log(debugMessage);


        httpHelper.CreateNewAccount(
            new Register { email = email, username = username, password = password, working_address = address, phone_number = phone_number }, () =>
            {
                Debug.Log("Account created");
                m_Animator.SetTrigger("create_account_close");
                m_Animator.SetTrigger("login_open");
            }, (errorMessage)=> { 
                
            });
        // Use MenuErrorFeedback Component to show error message
        // GetComponent<MenuErrorFeedback>().DisplayError(errorMessageHere);

    }

    void BackToLogin() 
    {
        create_email_textfield.GetComponent<InputField>().text = "";
        create_username_textfield.GetComponent<InputField>().text = "";
        create_password_textfield.GetComponent<InputField>().text = "";
        create_password_confirm_textfield.GetComponent<InputField>().text = "";
        create_address_textfield.GetComponent<InputField>().text = "";
        create_phone_number_textfield.GetComponent<InputField>().text = "";
        m_Animator.SetTrigger("create_account_close");
        m_Animator.SetTrigger("login_open");
    }

    void ExitApp()
    {
        Application.Quit();
    }

    Boolean CheckForPlayerLoggedIn()
    {
        if (stayOnLoginScreen)
            return false;
        if (PlayerPrefs.HasKey(PlayerPrefsConst.PREF_ACCESS_TOKEN))
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
