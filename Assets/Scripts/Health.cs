using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    private float health = 100f;
    private Animator anim;
    private AudioSource audioData;

    public TextMeshProUGUI healthText;
    private readonly string HEALTH_TEXT = "Health: ";

    private void Awake()
    {
        health = 100f;
        this.healthText.text = HEALTH_TEXT + ((int) health);
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
            this.healthText.text = HEALTH_TEXT + 0;
            //Destroy(GetComponent<CharacterInputController>());
        } 
    }

    public void GetHit(float dmgDone)
    {
        anim.ResetTrigger("hurt");
        if (dmgDone > 0)
        {
            health -= dmgDone;
            this.healthText.text = HEALTH_TEXT + ((int) health);
            anim.SetTrigger("hurt");
            audioData.Play(0);
        }
    }

    public bool Alive()
    {
        return health > 0;
    }
}
