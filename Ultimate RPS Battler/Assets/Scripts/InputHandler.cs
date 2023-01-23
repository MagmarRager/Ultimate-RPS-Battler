using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private int selectedUnitCost;

    public Shop selectedShop;

    public Board selectedBoard1;
    public Board selectedBoard2;

    public TargetArrow arrowEmitter;
    [SerializeField]
    private List<Transform> arrowPositions = new List<Transform>();

    void Start()
    {
        arrowEmitter.ArrowVisability(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.tag == "Shop")
        {
            DetectShopTile(eventData.pointerEnter.gameObject);
        }
        else if (eventData.pointerEnter.gameObject.tag == "Board")
        {
            DetectBoardTile(eventData.pointerEnter.gameObject);
        }
        else
        {
            //Clicked on nothing cleares all
            DeselectAll();
        }


        //Transitions between shop and board
        if (selectedShop != null && selectedBoard1 != null)
        {
            ShopTransition();
            PlayerStatsHandler.Instance.UpdateUI();
        }

        UpdateArrow();

    }

    void DetectShopTile(GameObject shopObj)
    {
        if (selectedShop == null)
        {
            selectedShop = shopObj.GetComponent<Shop>();

            //If not holding anything and is the first selected or not enough money, then clear
            if (arrowPositions.Count == 0 && (selectedShop.holdingUnit == null || !PlayerStatsHandler.Instance.EnoughCoins(selectedShop.unitPrice)))
            {
                selectedShop = null;
            }
            else
            {
                selectedUnitCost = selectedShop.unitPrice;
                selectedShop.selected = true;
                arrowPositions.Add(selectedShop.transform);
            }
        }
        else
            DeselectAll();
    }

    void DetectBoardTile(GameObject boardObj)
    {
        //If Board1 is occupied and then player selects another board they swap
        if (selectedBoard1 == null)
        {
            selectedBoard1 = boardObj.GetComponent<Board>();
            //If not holding anything and is the first selected, then clear
            if (selectedBoard1.holdingUnit == null && arrowPositions.Count == 0)
            {
                selectedBoard1 = null;
            }
            else
                arrowPositions.Add(selectedBoard1.transform);

        }
        else if (selectedBoard1.gameObject != boardObj) //If tile is not clicked twice 
        {
            //Switching places with board 1 and 2
            selectedBoard2 = boardObj.GetComponent<Board>();
            GameObject tempUnitHold = selectedBoard1.holdingUnit;
            selectedBoard1.holdingUnit = selectedBoard2.holdingUnit;
            selectedBoard2.holdingUnit = tempUnitHold;
            DeselectAll();
        }
    }

    void ShopTransition()
    {
        if (selectedBoard1.holdingUnit == null && selectedShop.holdingUnit != null) //Trying to Buy from shop
        {
            //Unit switch place from shop to board
            PlayerStatsHandler.Instance.coins -= selectedUnitCost;

            selectedBoard1.holdingUnit = selectedShop.holdingUnit;
            selectedShop.holdingUnit = null;
        }
        else if (selectedBoard1.holdingUnit != null && selectedShop.holdingUnit == null)// trying to sell
        {
            PlayerStatsHandler.Instance.coins += selectedUnitCost;

            selectedShop.holdingUnit = selectedBoard1.holdingUnit;
            selectedBoard1.holdingUnit = null;
        }

        DeselectAll();
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


    void UpdateArrow()
    {
        if (arrowPositions.Count == 0)
            arrowEmitter.ArrowVisability(false);
        else
        {
            arrowEmitter.transform.position = arrowPositions[0].position;
            arrowEmitter.ArrowVisability(true);
        }
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
