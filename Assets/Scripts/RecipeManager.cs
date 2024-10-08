using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] Recipes;
    public List<RecipeInfo> RecipesOnCanva = new List<RecipeInfo>(); 


    public GameObject RecipeList;
    public GameObject RecipeTemplate;
    bool NoIngredients;
    public EQManager EQManager;
    private void Awake()
    {
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
            RecipesOnCanva.Add(RecipeTemp.GetComponent<RecipeInfo>());

            int index = i;
            RecipeTemp.GetComponent<RecipeInfo>().image.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("PREPARING ITEM " + Recipes[index].Product.name);
                EQManager.GiveItem(Recipes[index]);
            });

            for (int j = 0; j < Recipes[i].itemsForRecipe.Length; j++)
            {
                GameObject ingredient = Instantiate(RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientTemplate, RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientsList.transform);
                ingredient.GetComponent<IngredientInfo>().ingredientCount.text = "x" + Recipes[i].itemsForRecipe[j].count;
                ingredient.GetComponent<IngredientInfo>().ingredientImage.sprite = Recipes[i].itemsForRecipe[j].item.icon;
                RecipeTemp.GetComponent<RecipeInfo>().ingredientsOnCanva.Add(ingredient.GetComponent<IngredientInfo>());
                //RecipeTemp.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 70);
            }
            
        }
    }

    public void CheckIngredients()
    {
        
        for(int j = 0; j< RecipesOnCanva.Count; j++)
        {
            NoIngredients = false;
            for(int i = 0; i< RecipesOnCanva[j].ingredientsOnCanva.Count; i++ )
            {
                bool AreItemsInEQ = CheckForItems(Recipes[j].itemsForRecipe[i].item, Recipes[j].itemsForRecipe[i].count);
                if (AreItemsInEQ == false) 
                {
                    NoIngredients = true;
                    RecipesOnCanva[j].ingredientsOnCanva[i].ingredientCount.color = Color.red;
                }
                else 
                {
                    RecipesOnCanva[j].ingredientsOnCanva[i].ingredientCount.color = Color.green;
                }
            }

            if(NoIngredients == false)
            {
                RecipesOnCanva[j].name.color = Color.green;
            }
            else
            {
                RecipesOnCanva[j].name.color = Color.red;
            }
        }
    }
}
