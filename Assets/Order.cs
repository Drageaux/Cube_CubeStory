using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Food/Order")]
public class Order : ScriptableObject
{
    new public string name = "New Order";
    public Sprite icon = null;
}
