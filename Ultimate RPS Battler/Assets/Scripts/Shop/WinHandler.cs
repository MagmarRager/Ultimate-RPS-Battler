using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinHandler : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public Button[] buttonsToDisable;

    private void Start()
    {

        winPanel.SetActive(false);
        losePanel.SetActive(false);

        if (PlayerStatsHandler.Instance.lives < 1)
        {
            losePanel.SetActive(true);
            PlayerStatsHandler.Instance.totLosses++;

            FirebaseManager.Instance.UpdatePlayeStats();


            DisableShop();
        }
        else if(PlayerStatsHandler.Instance.tier >= 10 || PlayerStatsHandler.Instance.winns >= 5)
        {
            winPanel.SetActive(true);
            PlayerStatsHandler.Instance.totWinns++;

            FirebaseManager.Instance.UpdatePlayeStats();


            DisableShop();
        }
    }
    public void ReturnToMenu()
    {
        PlayerStatsHandler.Instance.LoadAnotherScene("Menu");
    }

    void DisableShop()
    {
        GetComponent<InputHandler>().enabled = false;

        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].interactable = false;
        }
    }



}
