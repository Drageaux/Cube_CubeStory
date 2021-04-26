using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class displaySelection : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    Inventory invertory_script;
    public GameObject player;
    public GameObject chicken;
    PlayerDetectionAnimal chicken_script;

    private void Awake()
    {
        if (GetComponent<CanvasGroup>() != null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        else
        {
            Debug.LogError("component is null");
        }
        invertory_script = player.GetComponent<Inventory>();
        chicken_script = chicken.GetComponent<PlayerDetectionAnimal>();
    }

    // Update is called once per frame
    void Update()
    {
        //checking whether have mystery
        if (invertory_script.ingredientList.ContainsKey("superIngredient") && invertory_script.ingredientList["superIngredient"] >0) {
            Debug.Log("contain super");
            if (Input.GetKeyUp(KeyCode.Q)&& chicken_script.collect==true)
            {
                popUp pop = chicken.GetComponent<popUp>();
                pop.PopUp();

            }
        }
        else
        {
            Debug.Log("no super");
        }

    }
}
