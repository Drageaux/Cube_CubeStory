using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Food/Order")]
public class Recipe : ScriptableObject
{
    new public string name = "New Order";
    public Sprite icon = null;
    public bool isDefaultOrder = false;

    [System.Serializable]
    public class RequiredIngredient
    {
        public Ingredient ingredient;
        public int quantity;
    }

    public List<RequiredIngredient> requiredIngredients = new List<RequiredIngredient>();

    public void Cook()
    {

    }
}

