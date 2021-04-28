using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject trapped;
    public Transform trappedSpot;

    // Start is called before the first frame update
    void Start()
    {
        trappedSpot = transform.Find("TrappedMeatSpot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("wolf"))
        {
            print("killed wolf");
            trapped = collider.gameObject.GetComponent<WolfDieSpawnMeat>().MeatSpawner();
            trapped.SetActive(true);
            trapped.transform.position = trappedSpot.position;
            trapped.transform.SetParent(null);
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
