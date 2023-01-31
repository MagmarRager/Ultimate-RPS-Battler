using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class UserInfo
{
    public int Winns;
    public int Losses;
}



public class SignIn : MonoBehaviour
{

    public TMP_InputField email, password;
    public TextMeshProUGUI status;

    public delegate void OnLoadedDelegate(DataSnapshot snapshot);
    public delegate void OnSaveDelegate();

    FirebaseAuth auth;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;
        });


    }

    public void SignInButton()
    {
        SignInFirebase(email.text, password.text);
    }

    void SignInFirebase(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                status.text = "Invalid mail or password";
                Debug.LogError(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in Successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                status.text = newUser.Email + " is Signed In";
                FirebaseManager.Instance.userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
                LoadMenuScene();
            }
        });
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }




    public void RegisterButton()
    {
        if (email.text.Length < 5)
        {
            status.text = "Real smart there ey? Registerign requiers a mail!";
        }
        else if (password.text.Length < 6)
        {
            status.text = "Password is to short";
        }
        else
        {
            RegisterNewUser(email.text, password.text);
        }
    }

    void RegisterNewUser(string email, string password)
    {

        Debug.Log("Starting Registration");
        status.text = "Starting Registration";
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                status.text = email + " is invalid or already in use";
                Debug.LogError(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User Registerd: {0} ({1})", newUser.DisplayName, newUser.UserId);
                status.text = newUser.Email + " Registerd and Signed In";
                CreateNewStats();
            }
        });
    }

    void CreateNewStats()
    {
        //the database
        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        UserInfo userInfo = new UserInfo();

        userInfo.Winns = 0;
        userInfo.Losses = 0;

        string jsonString = JsonUtility.ToJson(userInfo);

        db.RootReference.Child("users").Child(userId).SetRawJsonValueAsync(jsonString).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
            {
                Debug.Log("DataTestWrite: Complete");
            }
        });
    }

    public void SendNewPasswordMail()
    {
        auth.SendPasswordResetEmailAsync(email.text);
        status.text = "A mail has been sent to " + email.text + " with instructions";
    }


    public void DebugLogIn(int number)
    {
        SignInFirebase("test" + number + "@test.test", "EpicPass");
    }
}
