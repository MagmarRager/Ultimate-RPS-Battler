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
    public int fightTier = 0;
    private int lives;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI healthText;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(2);
        }
    }

    private void Start()
    {
        lives = PlayerPrefs.GetInt(0 + PLAYER_LIVES);
        coins = 10;
        UpdateUI();
    }

    public void TakeDamage(int ammount)
    {
        lives += ammount;
        PlayerPrefs.SetInt(0 + PLAYER_LIVES, lives);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "" + coins;
        }
        if(healthText != null)
        {
            healthText.text = "" + lives;
        }
    }

    public bool EnoughCoins(int cost)
    {
        return (coins >= cost);
    }

    public void LoadAnotherScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
