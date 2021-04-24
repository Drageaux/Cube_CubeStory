using UnityEngine;
public class ItemPickup : Interactable
{
    public int quantity = 1;

    public override void Interact()
    {
        this.hasInteracted = true;
        this.OnDefocused();
        gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
    }
}
