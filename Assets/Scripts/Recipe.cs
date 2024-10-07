using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipes", order = 1)]
public class Recipe : ScriptableObject
{
    public int id;
    public string name;
    public int stackSize;
    public Sprite icon;
    public ItemsForRecipe[] itemsForRecipe;
    public Item Product;
}

[System.Serializable]
public class ItemsForRecipe
{
    public Item item;
    public int count;
}
