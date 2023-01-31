using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<Shop> allShops = new List<Shop>();
    public List<GameObject> shopUnits = new List<GameObject>();

    public int rerollCost = 2;
    bool unitsLoaded = false;


    public GameObject shopPosPrefab;

    private void Start()
    {
        LoadNewShop();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            PressReroll();
        }
    }

    public void PressReroll()
    {
        if (unitsLoaded && PlayerStatsHandler.Instance.EnoughCoins(rerollCost))
        {
            ReloadShop();
            PlayerStatsHandler.Instance.coins -= rerollCost;
        }
    }

    void LoadNewShop()
    {
        unitsLoaded = false;

        int shopAmmount = Random.Range(4, 6);

        for (int i = 0; i < shopAmmount; i++)
        {

            GameObject newShopPos = Instantiate(shopPosPrefab, transform);
            allShops.Add(newShopPos.GetComponent<Shop>());

        }

        StartCoroutine(waitForShopPlacement());
    }

    IEnumerator waitForShopPlacement()
    {

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < allShops.Count; i++)
        {
            int randomUnit = Random.Range(0, UnitSaver.Instance.baseUnits.Count);
            UnitSaver.Instance.SpawnUnit(randomUnit, allShops[i].transform.position, shopUnits);
            allShops[i].GetComponent<Shop>().holdingUnit = shopUnits[i];
        }

        unitsLoaded = true;
    }
    void ReloadShop()
    {
        //LATER IMPLEMEND PROGRAMMING PATTER MABEY       

        UpdateShop();

        for (int i = 0; i < allShops.Count; i++)
        {
            Destroy(allShops[i].gameObject);
        }
        for (int i = 0; i < shopUnits.Count; i++)
        {
            Destroy(shopUnits[i]);
        }

        shopUnits.Clear();
        allShops.Clear();

        LoadNewShop();
    }

    void UpdateShop()
    {
        shopUnits.Clear();

        for (int i = 0; i < allShops.Count; i++)
        {
            if (allShops[i].holdingUnit != null)
            {
                shopUnits.Add(allShops[i].holdingUnit);
            }
        }
    }


}
