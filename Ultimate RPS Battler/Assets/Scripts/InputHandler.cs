using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;


public class InputHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    private ShopController shopCtrl;
    public Shop selectedShop;

    public Board selectedBoard1;
    public Board selectedBoard2;

    public GameObject arrowEmitter;
    [SerializeField]
    private List<Transform> arrowPositions = new List<Transform>();

    void Start()
    {
        arrowEmitter.SetActive(false);
        shopCtrl = GetComponentInChildren<ShopController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.gameObject.name);

        if (eventData.pointerEnter.gameObject.tag == "Shop")
        {
            if (selectedShop == null)
            {
                selectedShop = eventData.pointerEnter.gameObject.GetComponent<Shop>();

                //If not holding anything and is the first selected, then clear
                if (selectedShop.holdingUnit == null && arrowPositions.Count == 0)
                {
                    selectedShop = null;
                }
                else
                {
                    selectedShop.selected = true;
                    arrowPositions.Add(selectedShop.transform);
                }
            }
            else
                DeselectAll();

        }
        else if (eventData.pointerEnter.gameObject.tag == "Board")
        {
            //If Board1 is occupied and then player selects another board they swap
            if (selectedBoard1 == null)
            {
                selectedBoard1 = eventData.pointerEnter.gameObject.GetComponent<Board>();

                //If not holding anything and is the first selected, then clear
                if (selectedBoard1.holdingUnit == null && arrowPositions.Count == 0)
                {
                    selectedBoard1 = null;
                }
                else
                    arrowPositions.Add(selectedBoard1.transform);

            }
            else
            {
                //Switching places with board 1 and 2
                selectedBoard2 = eventData.pointerEnter.gameObject.GetComponent<Board>();
                GameObject tempUnitHold = selectedBoard1.holdingUnit;
                selectedBoard1.holdingUnit = selectedBoard2.holdingUnit;
                selectedBoard2.holdingUnit = tempUnitHold;
                DeselectAll();
            }

        }
        else
        {
            //Clicked on nothing cleares all
            DeselectAll();
        }


        //Transitions between shop and board
        if (selectedShop != null && selectedBoard1 != null)
        {
            if (selectedBoard1.holdingUnit == null && selectedShop.holdingUnit != null) //Trying to Buy from shop
            {
                //Unit switch place from shop to board
                selectedBoard1.holdingUnit = selectedShop.holdingUnit;
                selectedShop.holdingUnit = null;
            }
            else if (selectedBoard1.holdingUnit != null && selectedShop.holdingUnit == null)// trying to sell
            {
                selectedShop.holdingUnit = selectedBoard1.holdingUnit;
                selectedBoard1.holdingUnit = null;
            }

            DeselectAll();
        }

        UpdateArrow();

    }

    void UpdateArrow()
    {
        if (arrowPositions.Count == 0)
            arrowEmitter.SetActive(false);
        else
        {
            arrowEmitter.transform.position = arrowPositions[0].position;
            arrowEmitter.SetActive(true);
        }
    }

    void DeselectAll()
    {
        arrowPositions.Clear();
        if (selectedShop != null)
            selectedShop.selected = false;

        selectedShop = null;
        selectedBoard1 = null;
        selectedBoard2 = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("I'm being dragged!");

        // find what I'm holding
        //Find where I'm dropped

        // if on nothing do nothing
        // if on unit replace unit
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }
}
