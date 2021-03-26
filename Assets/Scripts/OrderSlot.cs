using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{
    public Image icon;
    public Image done;

    Order order;

    public void Awake()
    {
        GameObject iconObj = transform.GetChild(0).gameObject;
        icon = iconObj.GetComponent<Image>();
        
    }

    public void AddOrder(Order newOrder)
    {
        order = newOrder;

        icon.sprite = order.dish.icon;
        icon.enabled = true;
        done.enabled = newOrder.completed;
    }

    public void ClearSlot()
    {
        order = null;

        icon.sprite = null;
        icon.enabled = false;
    }

}
