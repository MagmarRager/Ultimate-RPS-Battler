using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI userText;

    public TextMeshProUGUI winnsText;
    public TextMeshProUGUI lossesText;


    //Must find another way to do this
    private const string UNITS_LENGTH = "UNITS_LENGTH";
    private const string UNITS = "UNITS";

    private void Start()
    {
        userText.text = PlayerStatsHandler.Instance.userName + "!";

        winnsText.text = PlayerStatsHandler.Instance.totWinns + "";
        lossesText.text = PlayerStatsHandler.Instance.totLosses + "";
    }

    public void PlayButton()
    {
        for (int i = 0; i < PlayerPrefs.GetInt(0 + UNITS_LENGTH); i++)
        {
            PlayerPrefs.DeleteKey(0 + UNITS + i);
        }

        PlayerPrefs.DeleteKey(0 + UNITS_LENGTH);

        PlayerStatsHandler.Instance.lives = 5;
        PlayerStatsHandler.Instance.winns = 0;
        PlayerStatsHandler.Instance.tier = 0;
        SceneManager.LoadScene("ShopScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }



}
