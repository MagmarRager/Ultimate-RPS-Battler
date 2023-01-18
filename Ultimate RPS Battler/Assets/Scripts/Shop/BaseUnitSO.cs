using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTypes { Rock = 1, Paper = 2, Scissors = 3};

[CreateAssetMenu(fileName = "new Unit", menuName = "Units/Basic Unit")]
public class BaseUnitSO : ScriptableObject
{
    public string unitName;

    public int health;
    public int damage;

    public UnitTypes unitType;

    public UnitTypes Weakness
    {
        get 
        {
            switch (unitType)
            {
                case UnitTypes.Rock:
                    return UnitTypes.Paper;

                case UnitTypes.Paper:
                    return UnitTypes.Scissors;

                case UnitTypes.Scissors:
                    return UnitTypes.Rock;

                default:
                    return UnitTypes.Rock;

            }
        }
    }

    public Sprite sprite;

    public int cost;

    //work in progress
    public Effects effects;

}
