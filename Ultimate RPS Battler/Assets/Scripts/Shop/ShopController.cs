using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<Shop> allShops = new List<Shop>();
    public List<GameObject> units = new List<GameObject>();

    public GameObject shopPosPrefab;

    private void Start()
    {
        LoadNewShop();
    }

    void LoadNewShop()
    {
        int shopAmmount = Random.Range(4, 6);

        for (int i = 0; i < shopAmmount; i++)
        {
            int randomUnit = Random.Range(0, UnitSaver.Instance.baseUnits.Count);

            GameObject newShopPos = Instantiate(shopPosPrefab, transform);
            allShops.Add(newShopPos.GetComponent<Shop>());

            UnitSaver.Instance.SpawnUnit(randomUnit, allShops[i].transform.position, units);
            allShops[i].GetComponent<Shop>().holdingUnit = units[i];
        }
    }

    public void ReloadShop()
    {
        for (int i = 0; i < allShops.Count; i++)
        {
            //LATER IMPLEMEND PROGRAMMING PATTER MABEY
            Destroy(allShops[i]);
            Destroy(units[i]);
        }

    }
}
