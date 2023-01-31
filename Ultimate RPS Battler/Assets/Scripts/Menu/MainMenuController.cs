using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject profilePanel;

    //Must find another way to do this
    private const string UNITS_LENGTH = "UNITS_LENGTH";
    private const string UNITS = "UNITS";

    private void Start()
    {
        profilePanel.SetActive(false);
    }

    public void ToggleStats()
    {
        if (profilePanel.activeSelf)
            profilePanel.SetActive(false);

        else
            profilePanel.SetActive(true);
    }

    public void PlayButton()
    {
        for (int i = 0; i < PlayerPrefs.GetInt(0 + UNITS_LENGTH); i++)
        {
            PlayerPrefs.DeleteKey(0 + UNITS + i);
        }

        PlayerPrefs.DeleteKey(0 + UNITS_LENGTH);

        PlayerStatsHandler.Instance.lives = 5;
        SceneManager.LoadScene("ShopScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }



}
