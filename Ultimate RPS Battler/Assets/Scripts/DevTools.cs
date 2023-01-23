using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.L))
        {
            Debug.LogWarning("Player prefs Cleared!");
            PlayerPrefs.DeleteAll();
        }
    }
}
