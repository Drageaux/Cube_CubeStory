using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMysteryBox : MonoBehaviour
{
    private bool isRotate = false;
    private double maxHeight = 1.5;

    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
            if (transform.position.y < maxHeight)
            {
                transform.Translate(0, 2 * Time.deltaTime, 0);
            }
        }
        else
        { 
            if (transform.position.y > 0.8)
            {
                transform.Translate(0, -2 * Time.deltaTime, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            isRotate = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            isRotate = false;
        }
    }

    // checks to see if the player is in close proximity to the mystery box
    public bool isActive()
    {
        return isRotate;
    }
}
