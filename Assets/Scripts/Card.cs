using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public int water;
    public int startNode;
    public List<int> connectNode = new();
    public int kind;
    public int cardIndex;
    private Image TheImage => GetComponent<Image>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        CameraRoller.Instance.enabled = false;
        HandCardsManager.Instance.dragImage.sprite = TheImage.sprite;
        HandCardsManager.Instance.dragImage.SetNativeSize();
        HandCardsManager.Instance.theDraggingCard = this;
        HandCardsManager.Instance.dragImage.transform.position = new Vector3(
            Input.mousePosition.x /1920f * Screen.width, Input.mousePosition.y / 1080 * Screen.height, 0);
        HandCardsManager.Instance.dragImage.enabled = true;
        TheImage.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandCardsManager.Instance.dragImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CameraRoller.Instance.enabled = true;
        HandCardsManager.Instance.dragImage.enabled = false;
        TheImage.enabled = true;
        
        if (eventData.pointerCurrentRaycast.gameObject==null) return;
        if (GameManager.Instance.water - water < 0)
        {
            UIManager.Instance.WaterIsNotEnough();
            AudioManager.Instance.error.Play();
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<DropCardZone>())
        {
            DropCardZone.Instance.GiveUpCard(this,water);
            GameManager.Instance.ReplenishHandCards(kind,cardIndex);
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>()==null) return;
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>().isOccupied) return;
        if (!eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>()
                .CheckPuttable(water, startNode, connectNode)) return;
        
        GameManager.Instance.ChangeWater(-water,260);
        eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>().theCard.sprite = TheImage.sprite;
        eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>().isOccupied = true;
        GameManager.Instance.AddRow(eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>().rowIndex);
        eventData.pointerCurrentRaycast.gameObject.GetComponent<MyGrid>().ConnectNodes(water,startNode,connectNode);

        Destroy(gameObject);
        GameManager.Instance.ReplenishHandCards(kind,cardIndex);
    }
}
