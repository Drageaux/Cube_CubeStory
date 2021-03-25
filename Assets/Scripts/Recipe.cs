using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Food/Recipe")]
public class Recipe : ScriptableObject
{
    new public string name = "New Recipe";
    public Sprite icon = null;
    public bool isDefaultRecipe = false;

    public List<RequiredIngredient> requiredIngredients = new List<RequiredIngredient>();

    public void Cook()
    {

    }
}

[System.Serializable]
public class RequiredIngredient
{
    public Ingredient ingredient;
    public int quantity;
}