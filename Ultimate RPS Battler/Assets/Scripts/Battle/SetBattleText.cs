using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetBattleText : MonoBehaviour
{

    public TextMeshProUGUI playerLeft;
    public TextMeshProUGUI playerRight;
    public TextMeshProUGUI healthText;


    private void Start()
    {
        if (healthText != null)
            healthText.text = "" + PlayerStatsHandler.Instance.lives;

        playerLeft.text = healthText.text;

    }


}
