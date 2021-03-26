using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders : MonoBehaviour
{
    #region Singleton
    public static Orders instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Orders found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnOrdersChanged();
    public OnOrdersChanged onOrdersChangedCallback;


    public List<Recipe> menu = new List<Recipe>();

    public List<Order> orders = new List<Order>();
    public void Add(Order order)
    {
        orders.Add(order);
        if (onOrdersChangedCallback != null)
            onOrdersChangedCallback.Invoke();
    }

    public void Remove(Order order)
    {
        orders.Remove(order);
    }

    private void Start()
    {
        // test orders
        orders.Add(new Order(menu[1]));
        orders.Add(new Order(menu[1]));
        orders.Add(new Order(menu[0]));
        orders.Add(new Order(menu[1]));
        orders.Add(new Order(menu[0]));
    }

    private void Update()
    {
        foreach (Order o in orders)
        {
            if (!o.completed)
            {
                o.decreaseTimer();
            }
        }
    }

    public void FinishOrder()
    {
        foreach (Order o in orders)
        {
            if (!o.completed)
            {
                o.completed = true;
            }
            break;
        }
    }
}


[System.Serializable]
public class Order
{
    [HideInInspector]
    public string name = "Order";
    public Recipe dish;
    public float timeToComplete;
    public bool completed = false;

    public Order(Recipe dish, float time = 60f)
    {
        this.dish = dish;
        this.timeToComplete = time;
    }

    public float RemainingTime
    {
        get;
        private set;
    }

    public void decreaseTimer()
    {
        RemainingTime = timeToComplete - Time.time;
        //Debug.Log("remaining time: " + RemainingTime);
    }
}