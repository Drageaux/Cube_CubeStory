using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    private Transform placeSpot;
    public GameObject trapPrefab;

    // Start is called before the first frame update
    void Start()
    {
        placeSpot = transform.Find("TrapPlaceSpot");
    }

    // Update is called once per frame
    public void PlaceTrap()
    {
        GameObject trap = Instantiate(trapPrefab, placeSpot);
        trap.transform.localPosition = Vector3.zero;
        trap.SetActive(true);
        trap.transform.SetParent(null);
    }
}
