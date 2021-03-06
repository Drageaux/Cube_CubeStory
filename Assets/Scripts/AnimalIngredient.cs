using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalIngredient: ItemPickup
{
    public Ingredient ingredient;
    public bool caught = false;

    protected override void Start()
    {
        base.Start();
        name = ingredient.name;
        type = InteractableType.AnimalIngredient;
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            try
            {
                if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dive Catch"))
                {
                    print("Caught");
                    // TODO: Update Inventory
                    caught = true;
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
