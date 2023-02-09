using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject holdingUnit;

    public bool selected = false;

    public TextMeshProUGUI priceText;
    public int unitPrice;

    private void Awake()
    {
        priceText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (holdingUnit != null && !selected)
        {
            if (holdingUnit.transform.position != transform.position)
            {
                holdingUnit.transform.position = transform.position;
                unitPrice = holdingUnit.GetComponent<UnitScript>().unitSO.cost;
                priceText.text = "" + unitPrice;
            }
        }
        else if(holdingUnit == null && priceText.text != "")
        {
            priceText.text = "$";
        }
    }
}
