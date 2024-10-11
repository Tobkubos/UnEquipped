using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfo : MonoBehaviour
{
    public Recipe Recipe;
    public TextMeshProUGUI name;
    public Image image;
    public GameObject RecipeIngredientsList;
    public GameObject RecipeIngredientTemplate;
    public List<IngredientInfo> ingredientsOnCanva = new List<IngredientInfo>();
}
