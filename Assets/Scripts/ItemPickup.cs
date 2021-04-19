using UnityEngine;
public class ItemPickup : Interactable
{
    public Ingredient ingredient;
    public int quantity = 1;

    private void Awake()
    {
        name = ingredient.name;
        type = InteractableType.Ingredient;
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.OnFocused();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.OnDefocused();
        }
    }

    private void OnDestroy()
    {
        interactionManager.onInteractableDefocused(this);
    }
}
