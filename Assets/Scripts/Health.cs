using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    private float health = 100f;
    private Animator anim;
    private AudioSource audioData;

    private void Awake()
    {
        health = 100f;
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();

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

    public void GetHit(float dmgDone)
    {
        anim.ResetTrigger("hurt");
        if (dmgDone > 0)
        {
            health -= dmgDone;
            anim.SetTrigger("hurt");
            audioData.Play(0);
        }
    }

    public bool Alive()
    {
        return health > 0;
    }
}
