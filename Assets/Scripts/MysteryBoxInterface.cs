using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxInterface : MonoBehaviour
{
    private string[] ingredients = { "Egg", "Potato" };
    MysteryBoxRandomItem mysteryBoxRandomItem;
    TriggerMysteryBox triggerMysteryBox;
    // Start is called before the first frame update
    void Start()
    {
        mysteryBoxRandomItem = gameObject.GetComponent<MysteryBoxRandomItem>();
        triggerMysteryBox = gameObject.GetComponent<TriggerMysteryBox>();
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
