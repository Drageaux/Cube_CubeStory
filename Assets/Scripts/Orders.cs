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


    public List<Recipe> orders = new List<Recipe>();

    public void Add(Recipe order)
    {
        if (!order.isDefaultOrder)
        {
            orders.Add(order);
        }
    }
}
