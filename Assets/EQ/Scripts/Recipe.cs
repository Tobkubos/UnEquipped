using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipes", order = 1)]
public class Recipe : ScriptableObject
{
    //public int stackSize;
    public ItemsForRecipe[] itemsForRecipe;
    public Item Product;
}

[System.Serializable]
public class ItemsForRecipe
{
    public Item item;
    public int count;
}
