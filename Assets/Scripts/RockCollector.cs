using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RockCollector : MonoBehaviour
{

    Transform holdSpot;
    public Rigidbody rockPrefab;
    public Rigidbody currRock;
    public bool hasBall = false;

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        holdSpot = this.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/RockHoldSpot");
        if (holdSpot == null)
            Debug.LogError("Rock hold spot not found");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ReceiveRock();
    }

    // Update is called once per frame
    void Update()
    {
        if (currRock != null && Input.GetKeyDown(KeyCode.E))
            anim.SetBool("throw", true);
        else 
            anim.SetBool("throw", false);
    }

    void ReceiveRock()
    {
        hasBall = true;
        currRock = Instantiate<Rigidbody>(rockPrefab, holdSpot);
        currRock.transform.localPosition = Vector3.zero;
        currRock.isKinematic = true;
    }

    void ThrowRock()
    {
        // step 32 Milestone3_Spring2021.pdf
        currRock.transform.SetParent(null);
        currRock.isKinematic = false;
        // velocity zeroing is necessary due to a Unity bug
        currRock.velocity = Vector3.zero;
        currRock.angularVelocity = Vector3.zero;

        // apply force
        print(transform.forward);
        currRock.AddForce(transform.forward * 10);
        
        // to be picked up later
        this.currRock = null;
    }
}
