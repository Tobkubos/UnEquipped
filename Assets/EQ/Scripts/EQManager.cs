using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQManager : MonoBehaviour
{

    /// <summary>
    /// dodac dropienie item�w
    /// </summary>
    public GameObject[] Slots;
    public List<Item> items = new List<Item>();
    RecipeManager RecipeManager;

    public Item ItemCursorHolder;
    public GameObject OriginalSlot;
    public int OriginalCount;

    void Start()
    {
        RecipeManager = GameObject.Find("RecipeManager").GetComponent<RecipeManager>();
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].GetComponent<SlotHolder>().item = items[0];
            Slots[i].GetComponent<SlotHolder>().ctext.text = "";
        }
        UpdateSlots();

        for (int i = 0; i < Slots.Length; i++) 
        {
            Slots[i].GetComponent<SlotHolder>().InitNumbers();
        }
        RecipeManager.ShowRecipes();
        RecipeManager.CheckIngredients();
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item != null)
            {
                Slots[i].GetComponent<Image>().sprite = Slots[i].GetComponent<SlotHolder>().item.icon;
                if (Slots[i].GetComponent<SlotHolder>().item.stackSize <= 1)
                {
                    Slots[i].GetComponent<SlotHolder>().ctext.text = "";
                }
                else
                {
                    Slots[i].GetComponent<SlotHolder>().ctext.text = Slots[i].GetComponent<SlotHolder>().count.ToString();
                }
            }
            else
            {
                Slots[i].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void GiveRandomItem()
    {
        int id = Random.Range(1, items.Count);
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item.id == id && Slots[i].GetComponent<SlotHolder>().count < Slots[i].GetComponent<SlotHolder>().item.stackSize)
            {
                Slots[i].GetComponent<SlotHolder>().count += 1;
                UpdateSlots();
                RecipeManager.CheckIngredients();
                return;
            }
        }

        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item.id == 0)
            {
                Slots[i].GetComponent<SlotHolder>().item = items[id];
                Slots[i].GetComponent<SlotHolder>().count += 1;
                UpdateSlots();
                RecipeManager.CheckIngredients();
                return;
            }
        }

        Debug.Log("FULL EQ");
    }

    public void CraftItem(Recipe recipe)
    {
        for(int i = 0; i<recipe.itemsForRecipe.Length; i++)
        {
            int NeededItemCount = recipe.itemsForRecipe[i].count;
            for(int j = 0; j<Slots.Length; j++)
            {
                if (Slots[j].GetComponent<SlotHolder>().item.id == recipe.itemsForRecipe[i].item.id && NeededItemCount > 0)
                {
                    if (Slots[j].GetComponent<SlotHolder>().count - NeededItemCount >= 0 )
                    {
                        int t = Slots[j].GetComponent<SlotHolder>().count;
                        Slots[j].GetComponent<SlotHolder>().count -= NeededItemCount;
                        NeededItemCount -= t;
                        if (Slots[j].GetComponent<SlotHolder>().count == 0)
                        {
                            Slots[j].GetComponent<SlotHolder>().item = items[0];
                        }
                    }
                    else
                    {
                        NeededItemCount -= Slots[j].GetComponent<SlotHolder>().count;
                        Slots[j].GetComponent<SlotHolder>().count = 0;
                        Slots[j].GetComponent<SlotHolder>().item = items[0];
                    }
                    UpdateSlots();
                    RecipeManager.CheckIngredients();
                }
            }
        }




        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item.id == recipe.Product.id && Slots[i].GetComponent<SlotHolder>().count < Slots[i].GetComponent<SlotHolder>().item.stackSize)
            {
                Slots[i].GetComponent<SlotHolder>().count += 1;
                UpdateSlots();
                RecipeManager.CheckIngredients();
                return;
            }
        }

        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].GetComponent<SlotHolder>().item.id == 0)
            {
                Slots[i].GetComponent<SlotHolder>().item = recipe.Product;
                Slots[i].GetComponent<SlotHolder>().count += 1;
                UpdateSlots();
                RecipeManager.CheckIngredients();
                return;
            }
        }

        //INSTANTIATE OBJECT TO PICK UP IN WORLD
        Debug.Log("FULL EQ");


    }
}
