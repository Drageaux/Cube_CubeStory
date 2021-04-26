using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxInterface : ItemPickup
{
    private string[] ingredients = { "Egg", "Potato" };
    MysteryBoxRandomItem mysteryBoxRandomItem;
    TriggerMysteryBox triggerMysteryBox;
    public string RandomItem
    {
        get;
        private set;
    }

    public int RandomItemQuantity
    {
        get;
        private set;
    }

    protected override void Start()
    {
        base.Start();
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;
        type = InteractableType.MysteryBox;

        mysteryBoxRandomItem = gameObject.GetComponent<MysteryBoxRandomItem>();
        triggerMysteryBox = gameObject.GetComponent<TriggerMysteryBox>();

        RandomItem = mysteryBoxRandomItem.getRandomItem(ingredients); 
        RandomItemQuantity = mysteryBoxRandomItem.getRandomIndex(1, 4);
    }

    public void OpenBox()
    {
    }

    public override void Interact()
    {
        this.hasInteracted = true;
        this.OnDefocused();
        gameObject.SetActive(false);
    }

    public bool isActive()
    {
        return triggerMysteryBox.isActive();
    }
}
