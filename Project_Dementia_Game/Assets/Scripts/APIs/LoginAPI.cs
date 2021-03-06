using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginAPI : HttpRequest
{
    // Register form data
    public string SendRegisterData(string email, string username, string password, string workingAddress, string phoneNo, string userRole)
    {
        string urlPath = "/auth/register";
        string registerData = "{'email': " + email + "'username': " + username + "'password': " + password + "'workingAddress': " + workingAddress + "'phoneNo': " + phoneNo + "'userRole': " + userRole + "}";
        Upload(urlPath, registerData);
        return "Response";
    }

    // Send Login form data
    public string SendLoginData(string email, string password) 
    {
        string urlPath = "/auth/login";
        string loginData = "{'email': " + email + "'password': " + password + "}";
        Upload(urlPath, loginData);
        return "Response";
    }

    // Password Reset PATCH /auth/ password-reset
    public string ResetPassword()
    {
        return "";
    }

    // Password reset email POST /auth/ password-reset-email
    public string PasswordResetEmail()
    {
        return "";
    }

    // Register email verify  GET /auth /register-email-verify
    public string RegisterEmailVerify()
    {
        return "";
    }

    // Token Refresh POST /auth /token /refresh /
    public string RefreshToken()
    {
        return "";
    }


    // Log out POST /auth/ logout /
    public string SendLogOut()
    {
        return "";
    }

}
