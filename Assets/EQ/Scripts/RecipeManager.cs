using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] Recipes;
    public RecipeFolder[] RecipeFolders;
    public List<RecipeInfo> RecipesOnCanva = new List<RecipeInfo>();
    public List<GameObject> RecipeFoldersOnCanva = new List<GameObject>();

    public GameObject FolderList;
    public GameObject FolderMenu;
    public GameObject RecipeTemplate;
    public GameObject RecipeFolderTemplate;
    public GameObject RecipeFolderIconTemplate;
    bool NoIngredients;
    public EQManager EQManager;


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
        /*
        for(int i = 0; i<Recipes.Length; i++)
        {
            GameObject RecipeTemp = Instantiate(RecipeTemplate, RecipeList.transform);
            RecipeTemp.GetComponent<RecipeInfo>().name.text = Recipes[i].Product.name;
            RecipeTemp.GetComponent<RecipeInfo>().image.sprite = Recipes[i].Product.icon;
            RecipesOnCanva.Add(RecipeTemp.GetComponent<RecipeInfo>());

            int index = i;
            RecipeTemp.GetComponent<RecipeInfo>().image.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("PREPARING ITEM " + Recipes[index].Product.name);
                EQManager.CraftItem(Recipes[index]);
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
        */

        for(int i = 0;i < RecipeFolders.Count(); i++)
        {
            GameObject recipeFolderIcon = Instantiate(RecipeFolderIconTemplate, FolderMenu.transform);
            recipeFolderIcon.GetComponent<Image>().sprite = RecipeFolders[i].Recipes[0].Product.icon;

            GameObject recipeFolder = Instantiate(RecipeFolderTemplate, FolderList.transform);
            RecipeFoldersOnCanva.Add(recipeFolder);    
            recipeFolderIcon.GetComponent<Button>().onClick.AddListener(() =>
            {
               for (int f = 0; f<RecipeFoldersOnCanva.Count; f++)
                {
                    RecipeFoldersOnCanva[f].SetActive(false);        
                }
               recipeFolder.SetActive(true);
            });


            recipeFolder.GetComponent<ReParent>().Content.GetComponent<RectTransform>().sizeDelta += new Vector2(0, (RecipeFolders[i].Recipes.Length * 90f)+25f);
            for (int j = 0; j < RecipeFolders[i].Recipes.Length; j++) 
            {
                GameObject RecipeTemp = Instantiate(RecipeTemplate, recipeFolder.GetComponent<ReParent>().recipesParent.transform);
                RecipeInfo RecInf = RecipeTemp.GetComponent<RecipeInfo>();
                RecInf.Recipe = RecipeFolders[i].Recipes[j];
                RecInf.name.text = RecInf.Recipe.Product.name;
                RecInf.image.sprite = RecInf.Recipe.Product.icon;

                int indexi = i;
                int indexj = j;
                RecInf.image.GetComponent<Button>().onClick.AddListener(() => {
                    Debug.Log("PREPARING ITEM " + RecInf.Recipe.Product.name);
                    EQManager.CraftItem(RecInf.Recipe);
                });

                for (int k = 0; k < RecipeFolders[i].Recipes[j].itemsForRecipe.Length; k++)
                {
                    GameObject ingredient = Instantiate(RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientTemplate, RecipeTemp.GetComponent<RecipeInfo>().RecipeIngredientsList.transform);
                    IngredientInfo IngInf = ingredient.GetComponent<IngredientInfo>();
                    IngInf.ingredientCount.text = "x" + RecInf.Recipe.itemsForRecipe[k].count;
                    IngInf.ingredientImage.sprite = RecInf.Recipe.itemsForRecipe[k].item.icon;
                    RecInf.ingredientsOnCanva.Add(ingredient.GetComponent<IngredientInfo>());
                    //RecipeTemp.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 70);
                }


                RecipesOnCanva.Add(RecipeTemp.GetComponent<RecipeInfo>());
            }
        }
        
    }

    public void CheckIngredients()
    {
        
        for(int j = 0; j< RecipesOnCanva.Count; j++)
        {
            NoIngredients = false;
            for(int i = 0; i< RecipesOnCanva[j].Recipe.itemsForRecipe.Length; i++ )
            {
                bool AreItemsInEQ = CheckForItems(RecipesOnCanva[j].Recipe.itemsForRecipe[i].item, RecipesOnCanva[j].Recipe.itemsForRecipe[i].count);
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
                RecipesOnCanva[j].image.GetComponent<Button>().interactable = true;
            }
            else
            {
                RecipesOnCanva[j].name.color = Color.red;
                RecipesOnCanva[j].image.GetComponent<Button>().interactable = false;
            }
        }
    }
}
