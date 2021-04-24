using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalIngredient: ItemPickup
{
    public Ingredient ingredient;

    protected override void Start()
    {
        base.Start();
        name = ingredient.name;
        type = InteractableType.AnimalIngredient;
    }

    private void OnDestroy()
    {
        this.OnDefocused();
    }
}
