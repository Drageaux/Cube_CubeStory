using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.tag);
        if (collision.collider.gameObject.CompareTag("Terrain"))
        {
            print("ground touched");
        }
    }
}
