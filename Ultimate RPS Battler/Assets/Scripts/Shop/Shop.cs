using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject holdingUnit;

    public bool selected = false;

    public TextMeshProUGUI price;

    private void Awake()
    {
        price = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (holdingUnit != null && !selected)
        {
            holdingUnit.transform.position = transform.position;
            price.text = "" + holdingUnit.GetComponent<UnitScript>().unitSO.cost;
        }
    }

    public void Selected()
    {

    }

    public void Deselected()
    {

    }
}
