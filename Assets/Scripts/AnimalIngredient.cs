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

    private void Interact(Dictionary<string, int> ingredientList)
    {
        this.Interact(); // run the parent code's Interact
        this.UpdateInventory(ingredientList);
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
                    this.Interact(other.GetComponent<Inventory>().ingredientList);
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

    private void UpdateInventory(Dictionary<string, int> ingredientList)
    {
        if (ingredientList != null)
        {
            if (!ingredientList.ContainsKey(ingredient.name))
            {
                ingredientList.Add(ingredient.name, 1);
            }
            else
            {
                ingredientList[ingredient.name]++;

            }
            if (ingredient.name.Equals("Chicken"))
            {
                //chickenStorage.text = "+" + ingredientList[ingredient.name];
            }
            else
            {
                //s_ingStorage.text = "+" + ingredientList[ingredient.name];
            }
        }
        else
        {
            Debug.Log("can't find ingredient list");
        }
    }
}
