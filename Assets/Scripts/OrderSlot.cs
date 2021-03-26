using UnityEngine;
using UnityEngine.UI;

public class OrderSlot : MonoBehaviour
{
    public Image icon;
    public Image doneIcon;
    public GameObject requiredIngredientsUI;
    public Image firstIngredientIcon;
    public Text firstIngredientQuantityText;

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
        doneIcon.enabled = newOrder.completed;
        
        requiredIngredientsUI.SetActive(true);
        firstIngredientIcon.sprite = newOrder.dish.requiredIngredients[0].ingredient.icon;
        firstIngredientIcon.enabled = true;
        firstIngredientQuantityText.text = "x" + newOrder.dish.requiredIngredients[0].quantity;
    }

    public void ClearSlot()
    {
        order = null;

        icon.sprite = null;
        icon.enabled = false;
        doneIcon.enabled = false;

        requiredIngredientsUI.SetActive(false);
    }

}
