using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxCollector : MonoBehaviour
{
    public Text text;

    private bool showText = false;

    private Inventory inventory;
    private InteractionManager interactionManager;

    private float transformX;
    private float transformY;

    private double timer = 3.0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
        interactionManager = InteractionManager.instance;
        transformX = text.rectTransform.position.x;
        transformY = text.rectTransform.position.y;
    }

    private void Update()
    {
        if (showText)
        {
            text.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                text.rectTransform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showText = false;
                timer = 4.0;
                text.gameObject.SetActive(false);
                text.rectTransform.position = new Vector2(transformX, transformY);
                text.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                text.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        } 
    }

    public void CollectBox()
    {
        MysteryBoxInterface mysteryBox = (MysteryBoxInterface) interactionManager.CurrentTarget;
        if (mysteryBox != null && mysteryBox.name == "Mystery Box")
        {
            mysteryBox.Interact(inventory); // this must occur before referencing random item and randomitemquantity
            string ingr = mysteryBox.RandomItem;
            int ingrQuantity = mysteryBox.RandomItemQuantity;
            
            if (ingr.Equals("Egg"))
            {
                string plural = ingrQuantity > 1 ? "s" : "";
                inventory.eggStorage.text = "+" + inventory.ingredientList[ingr];
                text.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
            }
            if (ingr.Equals("Potato"))
            {
                string plural = ingrQuantity > 1 ? "es" : "";
                inventory.potatoStorage.text = "+" + inventory.ingredientList[ingr];
                text.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
            }
            if (ingr.Equals("Chicken"))
            {
                string plural = ingrQuantity > 1 ? "s" : "";
                inventory.chickenStorage.text = "+" + inventory.ingredientList[ingr];
                text.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
            }
            if (ingr.Equals("Gold Egg"))
            {
                string plural = ingrQuantity > 1 ? "s" : "";
                inventory.superIngredientStorage.text = "+" + inventory.ingredientList[ingr];
                text.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
            }
            showText = true;
        }
    }
}
