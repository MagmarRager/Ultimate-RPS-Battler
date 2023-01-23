using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickPropegator : MonoBehaviour, IPointerClickHandler
{
    //Buttons for some reason don't send pointer click events so I have to manually do it with this script
    public void OnPointerClick(PointerEventData eventData)
    {
        ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerClickHandler);
    }
}
