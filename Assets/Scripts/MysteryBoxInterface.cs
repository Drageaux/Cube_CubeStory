using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxInterface : Interactable
{
    private string[] ingredients = { "Egg", "Potato" };
    MysteryBoxRandomItem mysteryBoxRandomItem;
    TriggerMysteryBox triggerMysteryBox;

    protected override void Start()
    {
        base.Start();
        SphereCollider pickUpCollider = GetComponent<SphereCollider>();
        pickUpCollider.radius = radius;

        mysteryBoxRandomItem = gameObject.GetComponent<MysteryBoxRandomItem>();
        triggerMysteryBox = gameObject.GetComponent<TriggerMysteryBox>();
    }

    public override void Interact()
    {
        this.hasInteracted = true;
        this.OnDefocused();
        gameObject.SetActive(false);
    }

    public string getRandomItem()
    {
        return mysteryBoxRandomItem.getRandomItem(ingredients);
    }

    public int getRandomQuantity()
    {
        return mysteryBoxRandomItem.getRandomIndex(1, 4);
    }

    public bool isActive()
    {
        return triggerMysteryBox.isActive();
    }
}
