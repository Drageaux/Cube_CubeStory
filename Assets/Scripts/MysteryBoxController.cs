using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxController : MonoBehaviour
{
    public Text eggText;
    public Text potatoText;

    public Text potatoStorage;
    public Text eggStorage;

    private GameObject mysteryBox;
    private MysteryBoxInterface mysteryBoxInterface;
    private Inventory inventory;

    private bool showEggText = false;
    private bool showPotatoText = false;
    private double timer = 3.0;
    // Start is called before the first frame update
    void Start()
    {
        mysteryBox = null;
        mysteryBoxInterface = null;
        inventory = gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (mysteryBoxInterface != null && Input.GetKeyDown(KeyCode.Space))
        {
            if (mysteryBoxInterface.isActive()) 
            {
                string randomItem = mysteryBoxInterface.getRandomItem();
                int randomItemQuantity = mysteryBoxInterface.getRandomQuantity();
                if (!inventory.ingredientList.ContainsKey(randomItem))
                {
                    inventory.ingredientList.Add(randomItem, randomItemQuantity);
                }
                else
                {
                    inventory.ingredientList[randomItem] += randomItemQuantity;

                }
                Destroy(mysteryBox.gameObject);
                if (randomItem.Equals("Egg"))
                {
                    eggStorage.text = "+" + inventory.ingredientList[randomItem];
                    eggText.text = "You found " + randomItemQuantity + " " + randomItem + " in the mystery box!";
                    showEggText = true;
                }
                if (randomItem.Equals("Potato"))
                {
                    potatoStorage.text = "+" + inventory.ingredientList[randomItem];
                    potatoText.text = "You found " + randomItemQuantity + " " + randomItem + " in the mystery box!";
                    showPotatoText = true;
                }
            }
        }

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

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("MysteryBox"))
        {
            mysteryBox = c.gameObject;
            mysteryBoxInterface = mysteryBox.GetComponent<MysteryBoxInterface>();
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("MysteryBox"))
        {
            mysteryBox = null;
            mysteryBoxInterface = null;
        }
    }
}
