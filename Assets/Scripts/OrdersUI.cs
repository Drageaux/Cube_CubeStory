using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    Orders orders;

    const int maxDisplayedIngredients = 5;
    OrderSlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        orders = Orders.instance;
        orders.onOrdersChangedCallback += UpdateUI;

        slots = GetComponentsInChildren<OrderSlot>();

        // test orders
        orders.Add(new Order(orders.menu[1]));
        orders.Add(new Order(orders.menu[0]));
        orders.Add(new Order(orders.menu[1]));
        orders.Add(new Order(orders.menu[1]));


        // test to see if can update ordering when removed 1 element
        //orders.Remove(orders.orders[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < orders.orders.Count)
            {
                slots[i].AddOrder(orders.orders[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
