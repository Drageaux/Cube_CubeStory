using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> ingredientList;
    public List<Dictionary<string, int>> orders;
    public Dictionary<string, int> order1;

    // Start is called before the first frame update
    void Start()
    {
        this.ingredientList = new Dictionary<string, int>();
        this.order1 = new Dictionary<string, int>();
        this.order1.Add("Potato", 1);
        this.order1.Add("Egg", 1);
        orders = new List<Dictionary<string, int>>();
        orders.Add(this.order1);
    }

    // Update is called once per frame
    void Update()
    {
        // if pan close enough
        // if space is hit
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(!(orders.Count > 0)){
               return;
            }
            Dictionary<string, int> ordercurr = orders[0];
            bool iscompleted = true;
            foreach (KeyValuePair<string, int> entry in ordercurr)
            {
                int quantity = entry.Value;
                if (ingredientList.ContainsKey(entry.Key))
                {
                    if (!(ingredientList[entry.Key] >= quantity))
                    {
                        iscompleted = false;
                        break;
                    }
                } else {
                    iscompleted = false;
                    break;
                }
            }
            print(iscompleted);


            if (iscompleted)
            {
                foreach (KeyValuePair<string, int> entry in ordercurr)
                {
                    int quantity = entry.Value;
                    ingredientList[entry.Key] -= quantity;
                    if (ingredientList[entry.Key] <= 0)
                    {
                        ingredientList.Remove(entry.Key);
                    }
                }
                orders.RemoveAt(0);
                print("worked");
                //anim cook
            }
            else
            {
                print("not enough ingredients");
                // UI message to User
            }
        }
    }


    private void OnTriggerEnter(Collider c)
    {
        string name = c.gameObject.tag;
        switch (name)
        {
            case "Potato":
                if (!this.ingredientList.ContainsKey("Potato"))
                {
                    this.ingredientList.Add("Potato", 1);
                }
                else
                {
                    this.ingredientList[name]++;

                }
                Destroy(c.gameObject);
                break;
            case "Egg":
                if (!this.ingredientList.ContainsKey("Egg"))
                {
                    this.ingredientList.Add("Egg", 1);
                }
                else
                {
                    this.ingredientList[name]++;
                }
                Destroy(c.gameObject);
                break;
        }
         foreach (KeyValuePair<string, int> entry in ingredientList)
                {
                    print(entry.Key);
                    print(entry.Value);
                }

    }
}
