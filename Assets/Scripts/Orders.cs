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



    [System.Serializable]
    public class Order
    {
        [HideInInspector]
        public string name = "Order";
        public Recipe dish;
        public float remainingTime;
    }
    public List<Order> orders = new List<Order>();

    public void Add(Order order)
    {
        orders.Add(order);
    }
}
