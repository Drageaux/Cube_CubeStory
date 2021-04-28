using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDieSpawnMeat : MonoBehaviour
{
    public GameObject wolfMeatPrefab;
    public GameObject MeatSpawner()
    {
        return Instantiate(wolfMeatPrefab, transform.position, transform.rotation);
    }
}
