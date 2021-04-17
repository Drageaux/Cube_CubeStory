using UnityEngine;
public class ItemPickup : Interactable
{
    public Ingredient ingredient;
    public int quantity = 1;

    private void Awake()
    {
        name = ingredient.name;
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.OnFocused();
        }
    }

    private void OnDestroy()
    {
        interactionManager.onInteractableDefocused(this.ingredient.name);
    }
}
