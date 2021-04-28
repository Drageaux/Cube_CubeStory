using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RockCollector : MonoBehaviour
{

    Transform holdSpot;
    public Rigidbody rockPrefab;
    public Rigidbody currRock;

    private Animator anim;
    private CharacterInputController cinput;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cinput = GetComponent<CharacterInputController>();
        holdSpot = this.transform.Find("Root/J_Bip_C_Hips/J_Bip_C_Spine/J_Bip_C_Chest/J_Bip_C_UpperChest/J_Bip_R_Shoulder/J_Bip_R_UpperArm/J_Bip_R_LowerArm/J_Bip_R_Hand/RockHoldSpot");
        if (holdSpot == null)
            Debug.LogError("Rock hold spot not found");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (cinput.Throw)
        //{
        //    print("Throw");
        //    this.ReceiveRock();
        //    anim.SetBool("throw", true);
        //}
        //else
        //{
        //    anim.SetBool("throw", false);
        //}
    }

    void ReceiveRock()
    {
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
        Vector3 forceApplied = new Vector3(0, Mathf.Sin(45 * Mathf.Deg2Rad) * 10, 0);
        currRock.AddForce(transform.forward * 20 + forceApplied, ForceMode.VelocityChange);

        // to be picked up later
        this.currRock = null;
    }

    private void AddForceAtAngle(Rigidbody rb, float force, float angle)
    {
        Vector3 forceApplied = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad) * force, 0);

        rb.AddForce(transform.forward * 20 + forceApplied, ForceMode.VelocityChange);
    }
}
