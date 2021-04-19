using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : ItemPickup {
    public Ingredient ingredient;

    private void Start()
    {
        interactionManager = InteractionManager.instance;
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
        name = ingredient.name;
        type = InteractableType.Ingredient;
    }
}
