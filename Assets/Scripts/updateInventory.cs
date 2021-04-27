using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Inventory invertory_script;
    public Text chickenStorage;
    public Text eggStorage;
    public Text s_ingStorage;
  //  public CanvasGroup canvasGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        invertory_script = player.GetComponent<Inventory>();
    }

    public void updateChicken()
    {
        if (invertory_script.ingredientList != null)
        {
            if (!invertory_script.ingredientList.ContainsKey("Chicken"))
            {
                invertory_script.ingredientList.Add("Chicken", 1);
            }
            else
            {
                invertory_script.ingredientList["Chicken"]++;

            }
            chickenStorage.text = "+" + invertory_script.ingredientList["Chicken"];
            //remove mystery ingredient
            if (invertory_script.ingredientList.ContainsKey("Gold Egg")) {
                invertory_script.ingredientList["Gold Egg"]--;
                s_ingStorage.text = "+" + invertory_script.ingredientList["Gold Egg"];
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

        popUp pop = GameObject.FindGameObjectWithTag("Player").GetComponent<popUp>();
        pop.close();
    }

    public void updateEgg()
    {
        if (invertory_script.ingredientList != null)
        {
            if (!invertory_script.ingredientList.ContainsKey("Egg"))
            {
                invertory_script.ingredientList.Add("Egg", 3);
            }
            else
            {
                invertory_script.ingredientList["Egg"]=+3;

            }
            eggStorage.text = "+" + invertory_script.ingredientList["Egg"];
            //remove mystery ingredient
            if (invertory_script.ingredientList.ContainsKey("Gold Egg"))
            {
                invertory_script.ingredientList["Gold Egg"]--;
                s_ingStorage.text = "+" + invertory_script.ingredientList["Gold Egg"];
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
        popUp pop = GameObject.FindGameObjectWithTag("Player").GetComponent<popUp>();
        pop.close();

    }

    public void closeUI()
    {
        popUp pop = GameObject.FindGameObjectWithTag("Player").GetComponent<popUp>();
        pop.close();
    }
}
