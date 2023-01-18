using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Variables")]
    public float fightSpeed = 0.5f;
    private float timer;
    public bool startGame = false;
    private bool gameStarted = false;

    public float defultMoveModifier = 2;
    [SerializeField] private bool gameEnd = false;

    [Header("Components")]
    private PointHandler posHandler;

    [Header("List")]
    public List<GameObject> LeftBoardUnits = new List<GameObject>();
    public List<GameObject> RightBoardUnits = new List<GameObject>();

    [Header("TEST")]
    public int[] unitIDTestL;

    public int[] unitIDTestR;


    [SerializeField] private List<int> unitIDLoadL;
    [SerializeField] private List<int> unitIDLoadR;


    // Start is called before the first frame update
    void Start()
    {
        timer = -1f;
        gameEnd = false;
        posHandler = GetComponentInChildren<PointHandler>();
        gameStarted = false;

        UnitSaver.Instance.SaveUnits(unitIDTestL, 1);
        UnitSaver.Instance.SaveUnits(unitIDTestR, 2);

        UnitSaver.Instance.LoadUnits(unitIDLoadL, 1);
        UnitSaver.Instance.LoadUnits(unitIDLoadR, 2);


        if (startGame)
        {
            gameStarted = true;

            Invoke(nameof(SpawnUnits), 1);
        }

    }

    void SpawnUnits()
    {
        UnitSaver.Instance.SpawnUnits(unitIDLoadL, posHandler.leftStartPoints, LeftBoardUnits);
        UnitSaver.Instance.SpawnUnits(unitIDLoadR, posHandler.rightStartPoints, RightBoardUnits);

        MoveUnitsToPoints(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame && !gameStarted)
        {
            gameStarted = true;

            SpawnUnits();
        }

        if (!gameEnd && gameStarted)
        {
            if (timer >= fightSpeed)
            {
                CurrentlyBatteling(LeftBoardUnits[0].GetComponent<UnitScript>(), RightBoardUnits[0].GetComponent<UnitScript>());
                CheckWinner();
                MoveUnitsToPoints(fightSpeed);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }

    void CurrentlyBatteling(UnitScript leftPlayer, UnitScript rightEnemy)
    {
        //Play animations here

        leftPlayer.TakeDamage(rightEnemy.currentDamage, rightEnemy.unitSO.unitType);
        rightEnemy.TakeDamage(leftPlayer.currentDamage, leftPlayer.unitSO.unitType);

        if (leftPlayer.isDead)
            RemoveFighter(leftPlayer.gameObject, true);
        if (rightEnemy.isDead)
            RemoveFighter(rightEnemy.gameObject, false);

    }

    public void RemoveFighter(GameObject dyingUnit, bool isLeft)
    {
        if (isLeft)
        {
            LeftBoardUnits.Remove(dyingUnit);
            posHandler.ShufflePoints(true);
        }
        else
        {
            RightBoardUnits.Remove(dyingUnit);
            posHandler.ShufflePoints(false);
        }
    }

    private void CheckWinner()
    {
        if (LeftBoardUnits.Count <= 0 || RightBoardUnits.Count <= 0)
        {
            gameEnd = true;
            if (LeftBoardUnits.Count <= 0 && RightBoardUnits.Count <= 0)
            {
                Debug.Log("DRAW!");
            }
            else if (RightBoardUnits.Count <= 0)
            {
                Debug.Log("LEFT WINS!!");
            }
            else if (LeftBoardUnits.Count <= 0)
            {
                Debug.Log("RIGHT WINS!?");
            }
        }
    }

    public void MoveUnitsToPoints(float moveTime)
    {
        if (LeftBoardUnits.Count > 0)
        {
            for (int i = 1; i < LeftBoardUnits.Count; i++)
            {
                LeftBoardUnits[i].transform.DOMove(posHandler.leftPoints[i - 1].position, moveTime);
            }
            LeftBoardUnits[0].transform.DOMove(posHandler.leftBattlePos.position, moveTime);
        }

        if (RightBoardUnits.Count > 0)
        {
            for (int i = 1; i < RightBoardUnits.Count; i++)
            {
                RightBoardUnits[i].transform.DOMove(posHandler.rightPoints[i - 1].position, moveTime);
            }

            RightBoardUnits[0].transform.DOMove(posHandler.rightBattlePos.position, moveTime);
        }
    }



}