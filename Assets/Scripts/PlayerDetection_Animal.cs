using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection_Animal : MonoBehaviour
{
    public GameObject player;
    private float distance = 0;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(gameObject.transform.position,player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        
        if (distance < 5)
        {
            //close to chicken, chicken will run away
            anim.speed = 10;

        }
        else
        {
            //far from chicken, chicken will wandaring
            anim.speed = 1;
        }


        if (distance < 1)
        {
            Debug.Log("chicken_collected");
            Destroy(this.gameObject);
        }

        
   


    }

    //touched chicken, chicken collected
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("chicken_collected");
    //    Destroy(this.gameObject);
    //}
}
