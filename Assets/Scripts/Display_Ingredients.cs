using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Ingredients : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject DisplayText;
    private bool open = false;
    void Start()
    {
        DisplayText.SetActive(false);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            open = true;
            //  trigger = true;
            if (open)
            {
                DisplayText.SetActive(true);
                StartCoroutine("WaitForSec");
            }

        }

     


    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        DisplayText.SetActive(false);
        // trigger = false;


    }


    // Update is called once per frame
    void Update()
    {

    }
}



    

    
    

