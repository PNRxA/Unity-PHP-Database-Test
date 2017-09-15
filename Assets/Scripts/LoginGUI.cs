using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class LoginGUI : MonoBehaviour
{
    private float scrH, scrW;
    public bool onLoginScreen;
    public bool forgotPassword = false;
    public bool hasAccount = true;
    public bool authConfirm = false;
    public bool resetPasswordScreen = false;
    public bool loggedIn = false;
    public bool customisingCharacter = false;
    public string newPassword;
    public string confirmNewPassword;
    public string authCode;
    public string username;
    public string email;
    public string password;
    public string confirmPassword;
    public string aCode;
    public string loginInfo;
    public GUISkin gUISkin;

    // Use this for initialization
    void Start()
    {
        Debug.Log(AuthGenerator.Generate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = gUISkin;
        GUI.skin.settings.cursorColor = Color.black;
        

        scrH = Screen.height / 9;
        scrW = Screen.width / 16;

        LoginInfo();

        if (forgotPassword && authConfirm && resetPasswordScreen)
        {
            ResetPasswordScreen();
        }
        else if (forgotPassword && authConfirm)
        {
            AuthScreen();
        }
        else if (forgotPassword)
        {
            ForgotScreen();
        }
        else if (hasAccount)
        {
            LoginScreen();
        }
        else if (loggedIn && customisingCharacter)
        {
            CustomiseScreen();
        }
        else if (loggedIn)
        {
            AccountScreen();
        }
        else
        {
            SignupScreen();
        }
    }

    void LoginInfo()
    {
        GUI.Box(new Rect(scrW * 0, scrH * 7.5f, scrW * 7, scrH), loginInfo);
    }

    void LoginScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH * 0.5f, scrW * 4, scrH * 1), "Login");
        GUI.Box(new Rect(scrW * 3, scrH * 2.5f, scrW * 4, scrH * 1), "Username");
        username = GUI.TextField(new Rect(scrW * 3, scrH * 3, scrW * 4, scrH * .5f), username, 30);
        GUI.Box(new Rect(scrW * 8, scrH * 2.5f, scrW * 4, scrH * 1), "Password");
        password = GUI.PasswordField(new Rect(scrW * 8, scrH * 3, scrW * 4, scrH * .5f), password, '*', 100);
        if (GUI.Button(new Rect(scrW * 3, scrH * 6, scrW * 4, scrH), "Login"))
        {
            StartCoroutine(LoginToDB(username, password));
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 6, scrW * 4, scrH), "Sign Up!"))
        {
            hasAccount = !hasAccount;
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 7.5f, scrW * 4, scrH), "Forgot Password?"))
        {
            forgotPassword = !forgotPassword;
        }
    }

    void SignupScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH * 0.5f, scrW * 4, scrH * 1), "Sign Up");
        GUI.Box(new Rect(scrW * 3, scrH * 2.5f, scrW * 4, scrH * 1), "Username");
        username = GUI.TextField(new Rect(scrW * 3, scrH * 3, scrW * 4, scrH * .5f), username, 30);
        GUI.Box(new Rect(scrW * 8, scrH * 2.5f, scrW * 4, scrH * 1), "Email Address");
        email = GUI.TextField(new Rect(scrW * 8, scrH * 3, scrW * 4, scrH * .5f), email, 30);
        GUI.Box(new Rect(scrW * 3, scrH * 4.5f, scrW * 4, scrH * 1), "Password");
        password = GUI.PasswordField(new Rect(scrW * 3, scrH * 5, scrW * 4, scrH * .5f), password, '*', 100);
        GUI.Box(new Rect(scrW * 8, scrH * 4.5f, scrW * 4, scrH * 1), "Confirm Password");
        confirmPassword = GUI.PasswordField(new Rect(scrW * 8, scrH * 5, scrW * 4, scrH * .5f), confirmPassword, '*', 100);
        if (GUI.Button(new Rect(scrW * 3, scrH * 6, scrW * 4, scrH), "Create"))
        {
            StartCoroutine(CreateUser(username, password, email));
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 6, scrW * 4, scrH), "Sign In!"))
        {
            hasAccount = !hasAccount;
        }
    }

    void ResetPasswordScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH * 0.5f, scrW * 4, scrH * 1), "Reset Password");
        GUI.Box(new Rect(scrW * 3, scrH * 2.5f, scrW * 4, scrH * 1), "New Password");
        newPassword = GUI.PasswordField(new Rect(scrW * 3, scrH * 3, scrW * 4, scrH * .5f), newPassword, '*', 100);
        GUI.Box(new Rect(scrW * 8, scrH * 2.5f, scrW * 4, scrH * 1), "Confirm Password");
        confirmNewPassword = GUI.PasswordField(new Rect(scrW * 8, scrH * 3, scrW * 4, scrH * .5f), confirmNewPassword, '*', 100);
        if (GUI.Button(new Rect(scrW * 3, scrH * 6, scrW * 4, scrH), "Reset"))
        {
            StartCoroutine(UpdatePassword(email, newPassword));
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 7.5f, scrW * 4, scrH), "Cancel"))
        {
            resetPasswordScreen = !resetPasswordScreen;
            authConfirm = !authConfirm;
            forgotPassword = !forgotPassword;
        }
    }

    void AuthScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH * 0.5f, scrW * 4, scrH * 1), "Authentication");
        GUI.Box(new Rect(scrW * 3, scrH * 2.5f, scrW * 4, scrH * 1), "Auth Code");
        authCode = GUI.TextField(new Rect(scrW * 3, scrH * 3, scrW * 4, scrH * .5f), authCode, 30);

        if (GUI.Button(new Rect(scrW * 3, scrH * 6, scrW * 4, scrH), "Confirm"))
        {
            if (authCode == aCode)
            {
                resetPasswordScreen = true;
            }
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 7.5f, scrW * 4, scrH), "Back"))
        {
            authConfirm = !authConfirm;
        }
    }

    void ForgotScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH * 0.5f, scrW * 4, scrH * 1), "Forgot Password");
        GUI.Box(new Rect(scrW * 3, scrH * 2.5f, scrW * 4, scrH * 1), "Email");
        email = GUI.TextField(new Rect(scrW * 3, scrH * 3, scrW * 4, scrH * .5f), email, 30);

        if (GUI.Button(new Rect(scrW * 3, scrH * 6, scrW * 4, scrH), "Send Email"))
        {
            StartCoroutine(GetUser(email));
        }

        if (GUI.Button(new Rect(scrW * 8, scrH * 7.5f, scrW * 4, scrH), "Back to login"))
        {
            forgotPassword = !forgotPassword;
        }
    }

    void AccountScreen()
    {
        GUI.Box(new Rect(scrW * 6, scrH, scrW * 4, scrH), "Account");
        GUI.Box(new Rect(scrW, scrH, scrW * 4, scrH * 6), "Stats\n\nStat1: 1\nStat2: 2\nStat3: 3");
        if (GUI.Button(new Rect(scrW * 13, scrH, scrW * 2, scrH), "Customise"))
        {
            customisingCharacter = true;
        }
    }

    void CustomiseScreen()
    {
        GUI.Box(new Rect(scrW, scrH, scrW, scrH), "Customise Me");
    }

    IEnumerator UpdatePassword(string email, string newPassword)
    {
        string passwordURL = "http://localhost/databass_wubwubwub/updatepassword.php";
        WWWForm passwordForm = new WWWForm();
        passwordForm.AddField("emailPost", email);
        passwordForm.AddField("passwordPost", newPassword);
        WWW www = new WWW(passwordURL, passwordForm);

        yield return www;

        Debug.Log(www.text);

        resetPasswordScreen = !resetPasswordScreen;
        authConfirm = !authConfirm;
        forgotPassword = !forgotPassword;
    }

    IEnumerator GetUser(string email)
    {
        string getUsername = "http://localhost/databass_wubwubwub/checkuser.php";
        WWWForm getUserForm = new WWWForm();
        getUserForm.AddField("emailPost", email);
        WWW www = new WWW(getUsername, getUserForm);

        yield return www;

        Debug.Log(www.text);
        loginInfo = www.text;

        if (www.text != "No User")
        {
            username = www.text;
            SendEmail();
            authConfirm = true;
        }
    }

    void SendEmail()
    {
        aCode = AuthGenerator.Generate();
        MailMessage mail = new MailMessage();
        mail.To.Add(email);
        mail.Subject = "Password Reset";
        mail.Body = "Hello " + username + "\nEnter this code: " + aCode;
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        };
        smtpServer.Send(mail);
        Debug.Log("Success");
    }

    IEnumerator CreateUser(string username, string password, string email)
    {
        string CreateUserUrl = "http://localhost/databass_wubwubwub/insertuser.php";

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        form.AddField("emailPost", email);

        WWW www = new WWW(CreateUserUrl, form);

        yield return www;

        Debug.Log(www.text);
        loginInfo = www.text;

        if (www.text == "Created User")
        {
            hasAccount = !hasAccount;
        }
    }

    IEnumerator LoginToDB(string username, string password)
    {
        string LoginUrl = "http://localhost/databass_wubwubwub/login.php";

        WWWForm loginForm = new WWWForm();
        loginForm.AddField("usernamePost", username);
        loginForm.AddField("passwordPost", password);

        WWW www = new WWW(LoginUrl, loginForm);
        yield return www;

        Debug.Log(www.text);
        loginInfo = www.text;

        if (www.text == "Login Success")
        {
            hasAccount = false;
            loggedIn = true;
        }
    }
}
