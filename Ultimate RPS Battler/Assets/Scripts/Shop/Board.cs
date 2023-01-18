using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject holdingUnit;

    public bool selected = false;

    private void Update()
    {
        if (holdingUnit != null)
            holdingUnit.transform.position = transform.position;
    }
}
