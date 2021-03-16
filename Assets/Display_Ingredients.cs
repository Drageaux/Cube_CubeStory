using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Ingredients : MonoBehaviour
{

    public GameObject DisplayText;
    private bool open = false;

    // Start is called before the first frame update
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

        //EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
        //Destroy(this.gameObject);



    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        DisplayText.SetActive(false);
        // trigger = false;


    }
}



    

    
    

