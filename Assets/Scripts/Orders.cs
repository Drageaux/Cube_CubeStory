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
}


[System.Serializable]
public class Order
{
    [HideInInspector]
    public string name = "Order";
    public Recipe dish;
    public float timeToComplete = 60f;
    public bool completed = false;

    public float RemainingTime
    {
        get;
        private set;
    }

    public void decreaseTimer()
    {
        RemainingTime = timeToComplete - Time.time;
        Debug.Log("remaining time: " + RemainingTime);
    }
}