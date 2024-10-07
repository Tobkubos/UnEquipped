using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropper : MonoBehaviour, IDropHandler
{
    private EQManager EQManager;
    private void Start()
    {
        EQManager = GameObject.Find("EQManager").GetComponent<EQManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (EQManager.ItemCursorHolder != null)
        {
            Debug.Log("DROP");
            EQManager.OriginalSlot.GetComponent<SlotHolder>().item = EQManager.items[0];
            EQManager.OriginalSlot.GetComponent<SlotHolder>().count = 0;
            EQManager.OriginalSlot.GetComponent<SlotHolder>().ctext.text = EQManager.OriginalSlot.GetComponent<SlotHolder>().count.ToString();
            EQManager.ItemCursorHolder = null;
            EQManager.UpdateSlots();
        }
    }
}
