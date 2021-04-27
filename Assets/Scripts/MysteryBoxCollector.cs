using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxCollector : MonoBehaviour
{
    public Text eggText;
    public Text potatoText;

    private bool showEggText = false;
    private bool showPotatoText = false;
    private bool showGoldEggText = false;
    private bool showChickenText = false;

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
        transformX = eggText.rectTransform.position.x;
        transformY = eggText.rectTransform.position.y;
    }

    private void Update()
    {
        if (showEggText)
        {
            eggText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                eggText.rectTransform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showEggText = false;
                timer = 4.0;
                eggText.gameObject.SetActive(false);
                eggText.rectTransform.position = new Vector2(transformX, transformY);
                eggText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        }

        if (showPotatoText)
        {
            potatoText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                potatoText.rectTransform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showPotatoText = false;
                timer = 4.0;
                potatoText.gameObject.SetActive(false);
                potatoText.rectTransform.position = new Vector2(transformX, transformY);
                potatoText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                potatoText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                potatoText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        }

        if (showGoldEggText)
        {
            eggText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                eggText.rectTransform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showGoldEggText = false;
                timer = 4.0;
                eggText.gameObject.SetActive(false);
                eggText.rectTransform.position = new Vector2(transformX, transformY);
                eggText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
        }

        if (showChickenText)
        {
            eggText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                eggText.rectTransform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showChickenText = false;
                timer = 4.0;
                eggText.gameObject.SetActive(false);
                eggText.rectTransform.position = new Vector2(transformX, transformY);
                eggText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                eggText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
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
                eggText.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
                showEggText = true;
            }
            if (ingr.Equals("Potato"))
            {
                string plural = ingrQuantity > 1 ? "es" : "";
                inventory.potatoStorage.text = "+" + inventory.ingredientList[ingr];
                potatoText.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
                showPotatoText = true;
            }
            if (ingr.Equals("Gold Egg"))
            {
                string plural = ingrQuantity > 1 ? "s" : "";
                inventory.goldEggStorage.text = "+" + inventory.ingredientList[ingr];
                eggText.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
                showGoldEggText = true;
            }
            if (ingr.Equals("Chicken"))
            {
                string plural = ingrQuantity > 1 ? "s" : "";
                inventory.chickenStorage.text = "+" + inventory.ingredientList[ingr];
                eggText.text = "You found " + ingrQuantity + " " + ingr + plural + " in the mystery box!";
                showChickenText = true;
            }
        }
    }
}
