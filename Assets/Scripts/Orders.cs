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

    private int count;
    private AudioSource audioData;

    public void Add(Order order)
    {
        orders.Add(order);
        if (onOrdersChangedCallback != null)
            onOrdersChangedCallback.Invoke();
    }

    public void Remove(Order order)
    {
        orders.Remove(order);
        onOrdersChangedCallback.Invoke();
    }

    public void RemoveAt(int index)
    {
        orders.RemoveAt(index);
        onOrdersChangedCallback.Invoke();
    }

    private void Start()
    {
        this.count = 0;
        audioData = GetComponent<AudioSource>();
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
        // currently complete the 1st incomplete order
        RemoveAt(0);
        audioData.Play(0);
        //foreach (Order o in orders)
        //{
        //    if (!o.completed)
        //    {
        //        o.completed = true;
        //        onOrdersChangedCallback.Invoke();
        //        this.count++;
        //        break;
        //    }
        //}
    }

    public bool IsCompleted()
    {
        return this.orders.Count == 0;
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