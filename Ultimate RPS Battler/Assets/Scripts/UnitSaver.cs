using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSaver : MonoBehaviour
{
    public static UnitSaver Instance;

    public GameObject unitPrefab;

    public List<BaseUnitSO> baseUnits = new List<BaseUnitSO>();

    private const string UNITS_LENGTH = "UNITS_LENGTH";
    private const string UNITS = "UNITS";

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

    public void SaveUnits(int[] units, int pIndex)
    {
        PlayerPrefs.SetInt(pIndex + UNITS_LENGTH, units.Length);

        for (int i = 0; i < units.Length; i++)
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

    public void SpawnUnit(int unitID, Vector2 spawnPosition, List<GameObject> unitList)
    {
        GameObject newUnit = Instantiate(unitPrefab, spawnPosition, transform.rotation);
        newUnit.GetComponent<UnitScript>().unitSO = baseUnits[unitID];
        unitList.Add(newUnit);
    }
}
