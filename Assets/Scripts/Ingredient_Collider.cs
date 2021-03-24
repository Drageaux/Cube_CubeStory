using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient_Collider : MonoBehaviour
{

    public GameObject SucessText;
    private bool touch = false;
    public GameObject ingredient;
    // public string[] ingredientList;
    List<GameObject> ingredientList = new List<GameObject>();
    public Text Storagetext;

    // Start is called before the first frame update
    void Start()
    {
        SucessText.SetActive(false);
       
           
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            touch = true;
            //  trigger = true;
            if (touch)
            {
                SucessText.SetActive(true);
                StartCoroutine("WaitForSec");
                Destroy(ingredient);
                ingredientList.Add(ingredient);
                Debug.Log("storage info");
                Debug.Log(ingredientList.Count);
                // for (int i = 0; i < ingredientList.Count; i++)
                // {
                //     Debug.Log("!!displaying ingredients!!");
                //     Storagetext.text += ingredientList[i].name;
                //     Storagetext.text += '\n';
                //     Debug.Log(ingredientList[i].name);
                // }

            }

        }



    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1.0f);
        SucessText.SetActive(false);
        Debug.Log("sucessText set false!!!");
        // trigger = false;


    }
    
}
