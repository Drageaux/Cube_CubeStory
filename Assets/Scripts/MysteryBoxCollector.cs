using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxCollector : MonoBehaviour
{
    public Text potatoStorage;
    public Text eggStorage;
    public Text eggText;
    public Text potatoText;
    private bool showEggText = false;
    private bool showPotatoText = false;

    private Inventory inventory;
    private InteractionManager interactionManager;

    private double timer = 3.0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
        interactionManager = InteractionManager.instance;
    }

    private void Update()
    {
        if (showEggText)
        {
            eggText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                eggText.transform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showEggText = false;
                timer = 4.0;
                eggText.gameObject.SetActive(false);
                eggText.transform.position = new Vector3(543.1f, 219.5f, 0);
            }
        }

        if (showPotatoText)
        {
            potatoText.gameObject.SetActive(true);
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                potatoText.transform.Translate(0, 20 * Time.deltaTime, 0);
            }
            else
            {
                showPotatoText = false;
                timer = 4.0;
                potatoText.gameObject.SetActive(false);
                potatoText.transform.position = new Vector3(543.1f, 219.5f, 0);
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
        }
    }
}
