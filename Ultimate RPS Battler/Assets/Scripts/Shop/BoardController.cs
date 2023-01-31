using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public List<int> boardUnitsID = new List<int>();
    [SerializeField] List<GameObject> boardUnits = new List<GameObject>();

    [SerializeField] List<Board> allBoards = new List<Board>();
    public GameObject boardPref;
    

    [SerializeField] private int boardAmmount = 6;

    private void Start()
    {
        LoadBoard();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.B))
        {
            SaveBoard();
        }
    }

    public void StartBattle()
    {
        SaveBoard();
        PlayerStatsHandler.Instance.LoadAnotherScene("BattleScene");
    }

    void LoadBoard()
    {
        UnitSaver.Instance.LoadUnits(boardUnitsID, 0);

        for (int i = 0; i < boardAmmount; i++)
        {
            GameObject newShopPos = Instantiate(boardPref, transform);
            allBoards.Add(newShopPos.GetComponent<Board>());
        }
        StartCoroutine(waitForBoardPlacement());

    }

    IEnumerator waitForBoardPlacement()
    {
        yield return new WaitForEndOfFrame();

        if(boardUnitsID.Count > 0)
        {
            for (int i = 0; i < boardUnitsID.Count; i++)
            {
                UnitSaver.Instance.SpawnUnit(boardUnitsID[i], allBoards[i].transform.position, boardUnits);
                allBoards[i].GetComponent<Board>().holdingUnit = boardUnits[i];
            }
        }
    }

    void UpdateBoard()
    {
        boardUnitsID.Clear();
        boardUnits.Clear();

        for (int i = 0; i < allBoards.Count; i++)
        {
            if (allBoards[i].holdingUnit != null)
            {
                boardUnits.Add(allBoards[i].holdingUnit);
            }
        }

        for (int i = 0; i < boardUnits.Count; i++)
        {
            boardUnitsID.Add(boardUnits[i].GetComponent<UnitScript>().unitSO.ID);
        }
    }

    void SaveBoard()
    {
        UpdateBoard();
        UnitSaver.Instance.SaveUnits(boardUnitsID, 0);
    }
}
