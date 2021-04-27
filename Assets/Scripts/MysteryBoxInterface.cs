using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxInterface : Interactable
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
        name = "Mystery Box";
        type = InteractableType.Tool;

        mysteryBoxRandomItem = gameObject.GetComponent<MysteryBoxRandomItem>();
        triggerMysteryBox = gameObject.GetComponent<TriggerMysteryBox>();

        RandomItem = mysteryBoxRandomItem.getRandomItem(ingredients); 
        RandomItemQuantity = mysteryBoxRandomItem.getRandomIndex(1, 4);
    }

    public void Interact(Inventory inventory)
    {
        string ingr = RandomItem;
        int ingrQuantity = RandomItemQuantity;

        if (!inventory.ingredientList.ContainsKey(ingr))
        {
            inventory.ingredientList.Add(ingr, ingrQuantity);
        }
        else
        {
            inventory.ingredientList[ingr] += ingrQuantity;
        }

        this.hasInteracted = true;
        this.OnDefocused();
        gameObject.SetActive(false);
    }

    public bool isActive()
    {
        return triggerMysteryBox.isActive();
    }
}
