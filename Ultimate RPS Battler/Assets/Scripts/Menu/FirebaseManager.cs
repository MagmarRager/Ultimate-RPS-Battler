using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class FirebaseManager : MonoBehaviour
{

    private static FirebaseManager instance;
    public static FirebaseManager Instance { get { return instance; } } //Is this needed?



    //Function that gets called after load or save
    public delegate void OnLoadedDelegate(DataSnapshot snapshot);
    public delegate void OnLoadedStatsDelegate();
    public delegate void OnSaveDelegate();

    FirebaseDatabase db;
    public string userId;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        db = FirebaseDatabase.DefaultInstance;
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db.SetPersistenceEnabled(false); //Fix data cache problems


        for (int i = 0; i < 5; i++)
        {
            //SaveUnitData(i, ToString(1f));
        }
    }

    //loads the data at "path" then returns json result to the delegate/callback function
    public void LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Send our result (datasnapshot) to whom asked for it.
            onLoadedDelegate(task.Result);
        });
    }

    //Save the data at the given path, save callback optional
    public void SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        Debug.Log("step 3 done");
        db.RootReference.Child(path).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call our delegate if it's not null
            onSaveDelegate?.Invoke();
        });
    }

    //Save the data at the given path, save callback optional
    public void SaveUnitData(int tierIndex, string data, OnSaveDelegate onSaveDelegate = null)
    {
        db.RootReference.Child("games").Child("Tier" + tierIndex).Child(userId).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call our delegate if it's not null, AKA Plays optional function
            onSaveDelegate?.Invoke();
        });
    }
    public delegate void OnLoadedMultipleDelegate<T>(List<T> data); //list of objects

    public void LoadUnitData<T>(int tierIndex, OnLoadedMultipleDelegate<T> onLoadedDelegate)
    {
        db.RootReference.Child("games").Child("Tier" + tierIndex).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            var ListOfT = new List<T>();

            foreach (var item in task.Result.Children)
            {
                ListOfT.Add(JsonUtility.FromJson<T>(item.GetRawJsonValue()));
            }
            Debug.Log(tierIndex+" Load Unit Data Test");

            onLoadedDelegate(ListOfT);
        });
    }

    public void LoadStatsData(OnLoadedStatsDelegate onLoadedStatsDelegate)
    {
        db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);


            PlayerStatsHandler.Instance.RefreshStats(JsonUtility.FromJson<UserInfo>(task.Result.GetRawJsonValue()));
            onLoadedStatsDelegate.Invoke();
        });
    }

    public void UpdatePlayeStats()
    {
        UserInfo userInfo = new UserInfo();

        userInfo.UserName = PlayerStatsHandler.Instance.userName;
        userInfo.Winns = PlayerStatsHandler.Instance.totWinns;
        userInfo.Losses = PlayerStatsHandler.Instance.totLosses;

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
}



