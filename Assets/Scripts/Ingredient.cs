using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Food/Ingredient")]
public class Ingredient : ScriptableObject
{
    new public string name = "New Ingredient";
    public Sprite icon = null;
    public bool isDefaultIngredient = false;
}
