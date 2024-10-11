using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeFolder", menuName = "ScriptableObjects/RecipeFolder", order = 1)]
public class RecipeFolder : ScriptableObject
{
    public string FolderName;
    public Recipe[] Recipes;

}
