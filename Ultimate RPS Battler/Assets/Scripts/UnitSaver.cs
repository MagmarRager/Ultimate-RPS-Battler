using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]

public class UnitInfo
{
    public string name;
    public int unit_Leangth;
    public List<int> units;
}


public class UnitSaver : MonoBehaviour
{
    public static UnitSaver Instance;

    public GameObject unitPrefab;

    public List<BaseUnitSO> baseUnits = new List<BaseUnitSO>();

    private const string UNITS_LENGTH = "UNITS_LENGTH";
    private const string UNITS = "UNITS";

    //NOTE: player index 0 is the Player currently playing!

    public List<List<UnitInfo>> LoadedTier = new List<List<UnitInfo>>();


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

        for (int i = 0; i < baseUnits.Count; i++)
        {
            baseUnits[i].ID = i;
        }

    }

    public void SaveUnits(List<int> units, int pIndex)
    {
        PlayerPrefs.SetInt(pIndex + UNITS_LENGTH, units.Count);

        for (int i = 0; i < units.Count; i++)
        {
            PlayerPrefs.SetInt(pIndex + UNITS + i, units[i]);

        }
    }
    public void LoadUnits(List<int> unitList, int pIndex)
    {

        for (int i = 0; i < PlayerPrefs.GetInt(pIndex + UNITS_LENGTH); i++)
        {
            unitList.Add(PlayerPrefs.GetInt(pIndex + UNITS + i));
        }
    }

    public void SaveUnitsToDatabase(List<int> units, int tier)
    {
        UnitInfo unitInfo = new UnitInfo();

        unitInfo.unit_Leangth = units.Count;
        unitInfo.units = units;
        unitInfo.name = PlayerStatsHandler.Instance.userName;



        string jsonString = JsonUtility.ToJson(unitInfo);

        FirebaseManager.Instance.SaveUnitData(tier, jsonString);
    }

    //Replace current tier to PlayerStatsCount


    public bool LoadingList = false;
    public void LoadTier(int tier)
    {
        LoadingList = true;

        if (LoadedTier.Count < tier + 1)
            FirebaseManager.Instance.LoadUnitData<UnitInfo>(tier, ListLoaded);
        else
            LoadingList = false;
    }

    private void ListLoaded(List<UnitInfo> UsersInfo)
    {
        LoadedTier.Add(UsersInfo);
        LoadingList = false;
    }

    public List<int> LoadUnitsFromDatabase(int tier, int user)
    {
        return LoadedTier[tier][user].units;
    }

    public List<int> LoadUnitsFromDatabase(int tier) // If wanted random
    {
        int randomUser = UnityEngine.Random.Range(0, LoadedTier[tier].Count);


        return LoadedTier[tier][randomUser].units;
    }


    //Spawns multiple units
    public void SpawnUnits(List<int> unitID, List<Transform> spawnPositions, List<GameObject> unitList)
    {
        if (unitID.Count > spawnPositions.Count)
        {
            Debug.LogError(unitID.Count + " Units can't fit at " + spawnPositions.Count + " spawnpoints");
            return;
        }

        for (int i = 0; i < unitID.Count; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab, spawnPositions[i].transform.position, transform.rotation);
            newUnit.GetComponent<UnitScript>().unitSO = baseUnits[unitID[i]];
            unitList.Add(newUnit);
        }
    }

    //Spawns single Unit
    public void SpawnUnit(int unitID, Vector2 spawnPosition, List<GameObject> unitList)
    {
        GameObject newUnit = Instantiate(unitPrefab, spawnPosition, transform.rotation);
        newUnit.GetComponent<UnitScript>().unitSO = baseUnits[unitID];
        unitList.Add(newUnit);
    }
}
