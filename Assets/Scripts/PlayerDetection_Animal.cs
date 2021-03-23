using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetection_Animal : MonoBehaviour
{
    public GameObject player;
    private float distance = 0;
    private Animator anim;
    private Text Storagetext;
    Ingredient_Collider col_script;
    private bool added=false;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(gameObject.transform.position,player.transform.position);
        col_script = GameObject.Find("Cube").GetComponent<Ingredient_Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        
        if (distance < 5)
        {
            //close to chicken, chicken will run away
            //anim.speed = 10;
            anim.Play("Run_RM");
            if(distance < 2)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
                movementDirection.Normalize();

                transform.Translate(movementDirection * anim.speed * Time.deltaTime, Space.World);
                anim.Play("RunAngry_RM");
            }
        }
        else
        {
            //far from chicken, chicken will wandaring
            // anim.speed = 1;
            anim.Play("Walk_RM");
        }


        if (distance < 1.5)
        {
           Debug.Log("chicken_collected");
            anim.Play("GetHit");
            StartCoroutine("WaitForSec");
            // Destroy(this.gameObject);
            if (added == false)
            {
                col_script.Storagetext.text += this.name;
                col_script.Storagetext.text += '\n';
                added = true;
            }
        }

        
   


    }

    //touched chicken, chicken collected
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("chicken_collected");
    //    Destroy(this.gameObject);
    //}
   
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);


    }
}
