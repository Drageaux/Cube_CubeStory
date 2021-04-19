using UnityEngine;
public class ItemPickup : Interactable
{
    public int quantity = 1;

    public override void Interact()
    {
        this.hasInteracted = true;
        interactionManager.onInteractableDefocused.Invoke(this);
        gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
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

}
