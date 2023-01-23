using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{

    public TMP_InputField email, password;
    public TextMeshProUGUI status;

    public Button playButton;

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

                playButton.interactable = true;
            }
        });
    }

    public void RegisterButton()
    {
        if(email.text.Length < 5)
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

                playButton.interactable = true;
            }
        });
    }

    public void SendNewPasswordMail()
    {
        auth.SendPasswordResetEmailAsync(email.text);
        status.text = "A mail has been sent to " + email.text + " with instructions to reset password";
    }


    public void DebugLogIn(int number)
    {
        SignInFirebase("test" + number + "@test.test", "password");
    }
}
