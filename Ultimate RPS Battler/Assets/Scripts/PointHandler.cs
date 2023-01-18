using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    public List<Transform> leftPoints = new List<Transform>();
    public List<Transform> rightPoints = new List<Transform>();

    public List<Transform> leftStartPoints = new List<Transform>();
    public List<Transform> rightStartPoints = new List<Transform>();


    public Transform leftBattlePos;
    public Transform rightBattlePos;

    private void Start()
    {

        PointShuffler(rightStartPoints);
        PointShuffler(leftStartPoints);

        PointShuffler(rightPoints);
        PointShuffler(leftPoints);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PointShuffler(rightPoints);
            PointShuffler(leftPoints);
        }
    }

    public void ShufflePoints(bool isLeft)
    {
        if (isLeft)
            PointShuffler(leftPoints);
        else
            PointShuffler(rightPoints);
    }

    public void PointShuffler(List<Transform> listToShuffel)
    {
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = Random.Range(1, leftPoints.Count);
            var temp = listToShuffel[randomIndex];
            listToShuffel[randomIndex] = listToShuffel[0];
            listToShuffel[0] = temp;
        }
    }
}
