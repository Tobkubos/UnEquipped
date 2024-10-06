using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class OutsideEQ : MonoBehaviour, IDropHandler
{
    private EQManager EQManager;
    private void Start()
    {
        EQManager = GameObject.Find("EQManager").GetComponent<EQManager>(); 
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (EQManager.ItemCursorHolder != null) {

            EQManager.OriginalSlot.GetComponent<SlotHolder>().item = EQManager.ItemCursorHolder;
            EQManager.ItemCursorHolder = null;
            EQManager.UpdateSlots();
        }
    }
}