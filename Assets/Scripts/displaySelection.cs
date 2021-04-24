using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class displaySelection : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    Inventory invertory_script;
    public GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
        //checking whether have mystery
        if (invertory_script.ingredientList.ContainsKey("superIngredient") && invertory_script.ingredientList["superIngredient"] >0) {
            Debug.Log("contain super");
            if (Input.GetKeyUp(KeyCode.K))
            {

                if (canvasGroup.interactable)
                {
                    Time.timeScale = 1f;
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                }
                else
                {
                    Time.timeScale = 0f;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                }
            }
        }
        else
        {
            Debug.Log("no super");
        }

    }
}
