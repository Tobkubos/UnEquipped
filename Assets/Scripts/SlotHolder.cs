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
    Image temp;

    Canvas EQCanva;
    EQManager EQManager;

    public Sprite empty;

    public void InitNumbers()
    {
        if (item != null && item.id != 0)
        {
            count = 1;
            ctext.text = count.ToString();
        }
    }

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
            EQManager.OriginalCount = this.count;
            icon.sprite = item.icon;
            temp = Instantiate(icon, EQCanva.transform);
            temp.transform.position = eventData.position;
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = EQManager.OriginalCount.ToString();
            temp.raycastTarget = false;
            EQManager.ItemCursorHolder = item;
            item = EQManager.items[0];
            count = 0;
            ctext.text = count.ToString();
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
           
            SlotHolder droppedOnSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHolder>();
            if (droppedOnSlot == null)
            {
                return;
            }
            Debug.Log("DROP NA: " + droppedOnSlot.name);

            //WSTAW W WOLNY SLOT
            if (droppedOnSlot.item.id == 0)
            {
                EQManager.OriginalSlot.GetComponent<SlotHolder>().item = droppedOnSlot.item;
                droppedOnSlot.item = EQManager.ItemCursorHolder;
                EQManager.ItemCursorHolder = null;
                droppedOnSlot.count = EQManager.OriginalCount;
                droppedOnSlot.ctext.text = EQManager.OriginalCount.ToString();
                EQManager.UpdateSlots();
                return;
            }

            //ZAMIEN MIEJSCAMI
            if (droppedOnSlot.item.id != EQManager.ItemCursorHolder.id)
            {
                int tempCount = droppedOnSlot.count;

                EQManager.OriginalSlot.GetComponent<SlotHolder>().item = droppedOnSlot.item;
                droppedOnSlot.item = EQManager.ItemCursorHolder;


                droppedOnSlot.count = EQManager.OriginalCount;
                droppedOnSlot.ctext.text = droppedOnSlot.count.ToString();

                EQManager.OriginalSlot.GetComponent<SlotHolder>().count = tempCount;
                EQManager.OriginalSlot.GetComponent<SlotHolder>().ctext.text = EQManager.OriginalSlot.GetComponent<SlotHolder>().count.ToString();

                EQManager.ItemCursorHolder = null;
                EQManager.UpdateSlots();
                return;
            }

            //ZESTACKUJ
            if (droppedOnSlot.item.id == EQManager.ItemCursorHolder.id)
            {
                if(droppedOnSlot.count + EQManager.OriginalCount <= droppedOnSlot.item.stackSize)
                {
                    droppedOnSlot.count += EQManager.OriginalCount;
                    droppedOnSlot.ctext.text = droppedOnSlot.count.ToString();
                    EQManager.ItemCursorHolder = null;
                    EQManager.UpdateSlots();
                    return;
                }
                if (droppedOnSlot.count + EQManager.OriginalCount > droppedOnSlot.item.stackSize)
                {
                    int toMaxStack = droppedOnSlot.item.stackSize - droppedOnSlot.count;

                    //ZAMIEN MIEJSCAMI JEZELI FULL STACK
                    if (toMaxStack == 0)
                    {
                        int tempCount = droppedOnSlot.count;

                        EQManager.OriginalSlot.GetComponent<SlotHolder>().item = droppedOnSlot.item;
                        droppedOnSlot.item = EQManager.ItemCursorHolder;


                        droppedOnSlot.count = EQManager.OriginalCount;
                        droppedOnSlot.ctext.text = droppedOnSlot.count.ToString();

                        EQManager.OriginalSlot.GetComponent<SlotHolder>().count = tempCount;
                        EQManager.OriginalSlot.GetComponent<SlotHolder>().ctext.text = EQManager.OriginalSlot.GetComponent<SlotHolder>().count.ToString();

                        EQManager.ItemCursorHolder = null;
                        EQManager.UpdateSlots();
                        return;
                    }

                    droppedOnSlot.count += toMaxStack;
                    droppedOnSlot.ctext.text = droppedOnSlot.count.ToString();


                    EQManager.OriginalCount -= toMaxStack;
                    EQManager.OriginalSlot.GetComponent<SlotHolder>().item = EQManager.ItemCursorHolder;
                    EQManager.OriginalSlot.GetComponent<SlotHolder>().count = EQManager.OriginalCount;
                    EQManager.OriginalSlot.GetComponent<SlotHolder>().ctext.text = EQManager.OriginalSlot.GetComponent<SlotHolder>().count.ToString();



                    EQManager.ItemCursorHolder = null;
                    EQManager.UpdateSlots();
                    return;
                }
            }
        }
    }
}
