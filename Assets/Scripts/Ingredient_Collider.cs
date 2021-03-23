using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient_Collider : MonoBehaviour
{

    public GameObject SucessText;
    private bool touch = false;
    public GameObject ingredient;


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
                Destroy(ingredient);
                StartCoroutine("WaitForSec");
            }

        }



    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        SucessText.SetActive(false);
        // trigger = false;


    }
    
}
