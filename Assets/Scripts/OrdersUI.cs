using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    Orders orders;

    // Start is called before the first frame update
    void Start()
    {
        orders = Orders.instance;
        orders.onOrdersChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");
    }
}
