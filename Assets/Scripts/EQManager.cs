using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQManager : MonoBehaviour
{
    public GameObject[] Slots;
    public List<Item> items = new List<Item>();


    public Item ItemCursorHolder;
    public GameObject OriginalSlot;
    public int OriginalCount;

    void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].GetComponent<SlotHolder>().item = items[0];
            Slots[i].GetComponent<SlotHolder>().ctext.text = "";
        }


        Slots[0].GetComponent<SlotHolder>().item = items[1];
        Slots[1].GetComponent<SlotHolder>().item = items[2];
        Slots[2].GetComponent<SlotHolder>().item = items[2];
        Slots[3].GetComponent<SlotHolder>().item = items[2];
        Slots[4].GetComponent<SlotHolder>().item = items[2];
        UpdateSlots();

        for (int i = 0; i < Slots.Length; i++) 
        {
            Slots[i].GetComponent<SlotHolder>().InitNumbers();
        }
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item != null)
            {
                Slots[i].GetComponent<Image>().sprite = Slots[i].GetComponent<SlotHolder>().item.icon;
                Slots[i].GetComponent<SlotHolder>().ctext.text = Slots[i].GetComponent<SlotHolder>().count.ToString();
            }
            else
            {
                Slots[i].GetComponent<Image>().sprite = null;
            }
        }
    }
}
