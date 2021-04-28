using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> ingredientList;
    public Text potatoStorage;
    public Text eggStorage;
    public Text chickenStorage;
    public Text wolfMeatStorage;
    public Text superIngredientStorage;
    public GameObject lackIngredient;
    public GameObject storagePanel;

    private CharacterInputController cinput;
    private Orders ordersSystem;
    private Order cookingOrder;
    private InteractionManager interactionManager;


    private void Awake()
    {

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }

    // Start is called before the first frame update
    void Start()
    {
        ordersSystem = Orders.instance;
        interactionManager = InteractionManager.instance;
        this.ingredientList = new Dictionary<string, int>();
        lackIngredient.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if s it hit
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    if (storagePanel.activeSelf== false)
        //    {
        //        storagePanel.SetActive(true);
        //    }
        //    else
        //    {
        //        storagePanel.SetActive(false);
        //    }
        //}
      //  potatoStorage.text = "+" + this.ingredientList["Potato"];
      //  eggStorage.text = "+" + this.ingredientList["Egg"];
    }


    public void PickUpIngredient(ItemPickup pickup)
    {
        if (pickup.type != InteractableType.Ingredient && pickup.type != InteractableType.AnimalIngredient)
            return;
        string ingrName = pickup.name;

        if (!this.ingredientList.ContainsKey(ingrName))
        {
            this.ingredientList.Add(ingrName, pickup.quantity);
        }
        else
        {
            this.ingredientList[ingrName] += pickup.quantity;
        }
        switch (ingrName)
        {
            case "Potato":
                potatoStorage.text = "+" + this.ingredientList[ingrName];
                break;
            case "Egg":
                eggStorage.text = "+" + this.ingredientList[ingrName];
                break;
            case "Chicken":
                chickenStorage.text = "+" + this.ingredientList[ingrName];
                break;
            case "Wolf Meat":
                wolfMeatStorage.text = "+" + this.ingredientList[ingrName];
                break;
            case "Gold Egg":
                superIngredientStorage.text = "+" + this.ingredientList[ingrName];
                popUp pop = GetComponent<popUp>();
                pop.PopUp();
                Debug.Log("pop works");
                break;
        }
        foreach (KeyValuePair<string, int> entry in ingredientList)
        {
            print(entry.Key);
            print(entry.Value);
        }
        pickup.Interact();
    }

    public bool HasEnoughIngredients()
    {
        if (ordersSystem.orders.Count > 0)
        {
            cookingOrder = null;
            foreach (Order o in ordersSystem.orders)
            {
                // cook if not completed or not failed to do on time
                //if (!o.completed && o.RemainingTime > 0)
                //{
                //Debug.Log("checking for order: " + o.dish + " (completed: " + o.completed + ")");
                cookingOrder = o;
                break;
                //}
            }
            Debug.Log(ordersSystem.orders);
            bool hasEnough = true;
            if (cookingOrder != null)
            {
                Debug.Log("current order: " + cookingOrder.dish);
                foreach (RequiredIngredient req in cookingOrder.dish.requiredIngredients)
                {
                    if (ingredientList.ContainsKey(req.ingredient.name))
                    {
                        if (!(ingredientList[req.ingredient.name] >= req.quantity))
                        {
                            hasEnough = false;
                            break;
                        }
                    }
                    else
                    {
                        hasEnough = false;
                        break;
                    }
                }
            }
            else
            {
                hasEnough = false;
            }
            Debug.Log("Have enough ingredients? " + hasEnough);
            return hasEnough;
        }

        return false;
    }


    public void FinishCooking()
    {
        if (cookingOrder != null)
        {
            foreach (RequiredIngredient req in cookingOrder.dish.requiredIngredients)
            {
                int quantity = req.quantity;
                string ingredientName = req.ingredient.name;
                ingredientList[ingredientName] -= quantity;
                if (ingredientName == "Potato")
                {
                    potatoStorage.text = "+" + ingredientList[ingredientName];
                }
                else if (ingredientName == "Egg")
                {
                    eggStorage.text = "+" + ingredientList[ingredientName];
                }
                else if (ingredientName == "Wolf Meat")
                {
                    wolfMeatStorage.text = "+" + ingredientList[ingredientName];
                } 
                else if (ingredientName == "Chicken")
                {
                    chickenStorage.text = "+" + ingredientList[ingredientName];
                }
                if (ingredientList[ingredientName] <= 0)
                {
                    ingredientList.Remove(ingredientName);
                }
            }
            ordersSystem.FinishOrder();
            cookingOrder = null;
            print("worked");
        }
    }

   
}
