using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag);
        if (collider.gameObject.CompareTag("wolf"))
        {
            Destroy(collider.gameObject);
            //Inventory
        }
    }
}
