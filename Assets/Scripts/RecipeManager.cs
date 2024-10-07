using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] Recipes;

    public GameObject RecipeList;
    public GameObject RecipeTemplate;

    EQManager EQManager;
    private void Start()
    {
        EQManager = GameObject.Find("EQManager").GetComponent<EQManager>();
        //ShowRecipes();
        //CheckIngredients();
    }


    public bool CheckForItems(Item item, int count)
    {
        int num = 0;
        for(int i = 0; i < EQManager.Slots.Length; i++)
        {
            if (EQManager.Slots[i].GetComponent<SlotHolder>().item.id == item.id)
            {
                num += EQManager.Slots[i].GetComponent<SlotHolder>().count;
            }
        }

        if(num >= count)
        {
            return true;
        }
        return false;
    }


    public void ShowRecipes()
    {
        for(int i = 0; i<Recipes.Length; i++)
        {
            GameObject RecipeTemp = Instantiate(RecipeTemplate, RecipeList.transform);
            RecipeTemp.GetComponent<RecipeInfo>().name.text = Recipes[i].Product.name;
            RecipeTemp.GetComponent<RecipeInfo>().image.sprite = Recipes[i].Product.icon;
 

            
            for(int j = 0; j < Recipes[i].itemsForRecipe.Length; j++)
            {
                GameObject ingredient = Instantiate(RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientTemplate, RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientsList.transform);
                ingredient.GetComponent<IngredientInfo>().ingredientName.text = Recipes[i].itemsForRecipe[j].item.name;
                ingredient.GetComponent<IngredientInfo>().ingredientCount.text = "x" + Recipes[i].itemsForRecipe[j].count;
                ingredient.GetComponent<IngredientInfo>().ingredientImage.sprite = Recipes[i].itemsForRecipe[j].item.icon;
                RecipeTemp.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 70);
            }
            
        }
    }

    public void CheckIngredients()
    {
        
        foreach(Recipe recipe in Recipes)
        {
            for (int i = 0; i < recipe.itemsForRecipe.Length; i++)
            {
                bool AreItemsInEQ = CheckForItems(recipe.itemsForRecipe[i].item, recipe.itemsForRecipe[i].count);
                if (AreItemsInEQ == false) 
                {
                    Debug.Log("nie ma na " + recipe.Product.name);
                    return; 
                }

            }
            Debug.Log("SA ITEMKI NA " + recipe.Product.name);
        }
    }
}
