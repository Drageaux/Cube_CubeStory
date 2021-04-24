using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Inventory invertory_script;
    public Text potatoStorage;
    public Text eggStorage;
    public Text s_ingStorage;
    public CanvasGroup canvasGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        invertory_script = player.GetComponent<Inventory>();
    }

    public void updatePotato()
    {
        if (invertory_script.ingredientList != null)
        {
            if (!invertory_script.ingredientList.ContainsKey("Potato"))
            {
                invertory_script.ingredientList.Add("Potato", 1);
            }
            else
            {
                invertory_script.ingredientList["Potato"]++;

            }
            potatoStorage.text = "+" + invertory_script.ingredientList["Potato"];
            //remove mystery ingredient
            if (invertory_script.ingredientList.ContainsKey("superIngredient")) {
                invertory_script.ingredientList["superIngredient"]--;
                s_ingStorage.text = "+" + invertory_script.ingredientList["superIngredient"];
            }
            else
            {
                Debug.Log("no mystery ingredient");
            }
        }
        else
        {
            Debug.Log("can't find ingredient list");
        }

        Time.timeScale = 1f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    public void updateEgg()
    {
        if (invertory_script.ingredientList != null)
        {
            if (!invertory_script.ingredientList.ContainsKey("Egg"))
            {
                invertory_script.ingredientList.Add("Egg", 1);
            }
            else
            {
                invertory_script.ingredientList["Egg"]++;

            }
            eggStorage.text = "+" + invertory_script.ingredientList["Egg"];
            //remove mystery ingredient
            if (invertory_script.ingredientList.ContainsKey("superIngredient"))
            {
                invertory_script.ingredientList["superIngredient"]--;
                s_ingStorage.text = "+" + invertory_script.ingredientList["superIngredient"];
            }
            else
            {
                Debug.Log("no mystery ingredient");
            }
        }
        else
        {
            Debug.Log("can't find ingredient list");
        }
        Time.timeScale = 1f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }
}
