using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public TextMeshProUGUI ctext;
    public int count;
    public Image icon;
    private Transform originalParent;
    Image temp;

    Canvas EQCanva;
    EQManager EQManager;

    public Sprite empty;
    private void Start()
    {
        EQCanva = GameObject.Find("EQ").GetComponent<Canvas>();
        EQManager = GameObject.Find("EQManager").GetComponent<EQManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && item.id != 0)
        {
            Debug.Log("AAAAAAAA");
            EQManager.OriginalSlot = this.gameObject;
            icon.sprite = item.icon;
            temp = Instantiate(icon, EQCanva.transform);
            temp.transform.position = eventData.position;
            temp.raycastTarget = false;
            EQManager.ItemCursorHolder = item;
            item = EQManager.items[0];
            temp.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            EQManager.UpdateSlots();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (temp != null)
        {
            temp.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("END DRAG");
        if (temp != null)
        {
            Destroy(temp.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (EQManager.ItemCursorHolder != null)
        {
            Debug.Log("DROP");
            SlotHolder droppedOnSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHolder>();
            if (droppedOnSlot.item.id == 0)
            {
                EQManager.OriginalSlot.GetComponent<SlotHolder>().item = droppedOnSlot.item;
                droppedOnSlot.item = EQManager.ItemCursorHolder;
                EQManager.ItemCursorHolder = null;
                EQManager.UpdateSlots();
                return;
            }
            if (droppedOnSlot.item.id != EQManager.ItemCursorHolder.id)
            {
                EQManager.OriginalSlot.GetComponent<SlotHolder>().item = droppedOnSlot.item;
                droppedOnSlot.item = EQManager.ItemCursorHolder;
                EQManager.ItemCursorHolder = null;
                EQManager.UpdateSlots();
                return;
            }
        }
    }
}
