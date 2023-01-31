using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsHandler : MonoBehaviour
{

    private const string PLAYER_LIVES = "PLAYER_LIVES";

    public static PlayerStatsHandler Instance;

    [Header("In Game stats")]
    public int coins = 10;
    public int tier = 0;
    public int shopTier = 0;
    public int lives;

    [Header("Out of Game stats")]
    public int totWins;
    public int totLosses;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        coins = 10;
    }


    public bool EnoughCoins(int cost)
    {
        return (coins >= cost);
    }

    public void LoadAnotherScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
