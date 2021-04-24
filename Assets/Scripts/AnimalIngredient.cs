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
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
    }

    protected void OnCollisionEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            try
            {
                if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dive Catch"))
                {
                    print("Caught");
                } 
                else
                {
                    print("Need to press E");
                }
            } 
            catch
            {
                Debug.LogError("Cannot get Player's current animation state info");
            }
        }
    }

    private void OnDestroy()
    {
        this.OnDefocused();
    }
}
