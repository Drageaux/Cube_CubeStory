using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    private float health = 100f;
    private Animator anim;

    private void Awake()
    {
        health = 100f;
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");
    }

    private void Update()
    {
        // game over
        if (health <= 0)
        {
            anim.SetBool("dead", true);
            //Destroy(GetComponent<CharacterInputController>());
        } 
    }

    private void FixedUpdate()
    {
        anim.ResetTrigger("hurt");
    }

    public void GetHit(float dmgDone)
    {
        if (dmgDone > 0)
        {
            health -= dmgDone;
            anim.SetTrigger("hurt");
        }
    }

    public bool Alive()
    {
        return health > 0;
    }
}
