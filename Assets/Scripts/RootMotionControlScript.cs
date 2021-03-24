﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;

    private Transform leftFoot;
    private Transform rightFoot;

    public float cookingTime = 4f; // in seconds
    private float remainingTimer;

    public bool cooking;
    public GameObject cookingStandingSpot;
    //public float buttonCloseEnoughForMatchDistance = 2f;
    public float cookingCloseEnoughDistance = 0.22f;
    public float cookingCloseEnoughAngle = 5f;
    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;
    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;
    public GameObject cookingObject;

    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;


    private int groundContactCount = 0;

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void Awake()
    {

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponentInParent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }


    // Use this for initialization
    void Start()
    {
        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("J_Bip_C_Hips/J_Bip_L_UpperLeg/J_Bip_L_LowerLeg/J_Bip_L_Foot");
        rightFoot = this.transform.Find("J_Bip_C_Hips/J_Bip_R_UpperLeg/J_Bip_R_LowerLeg/J_Bip_R_Foot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

    }




    void Update()
    {
        if (Time.time > remainingTimer)
        {
            //Debug.Log("timer " + Time.time);
            cooking = false;
        }
        //bool doMatchToButtonPress = false;

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground.
        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
        //work
        bool isGrounded = IsGrounded;// || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);


        float buttonDistance = float.MaxValue;
        float buttonAngleDegrees = float.MaxValue;

        if (cookingStandingSpot != null)
        {
            buttonDistance = Vector3.Distance(transform.position, cookingStandingSpot.transform.position);
            buttonAngleDegrees = Quaternion.Angle(transform.rotation, cookingStandingSpot.transform.rotation);
            //Debug.Log("distance to cook " + buttonDistance);
            //Debug.Log("angle to cook " + buttonAngleDegrees);
        } 
        if (cinput.Action)
        {
            Debug.Log("Action pressed");

            //if (buttonDistance <= buttonCloseEnoughForMatchDistance)
            //{
                if (buttonDistance <= cookingCloseEnoughDistance &&
                    buttonAngleDegrees <= cookingCloseEnoughAngle)
                {
                    Debug.Log("Cooking initiated");
                
            Debug.Log("timer " + Time.time);
                    cooking = true;
                    remainingTimer = Time.time + cookingTime;
                }
                else
                {
                    cooking = false;
                    //Debug.Log("match to button initiated");
                    //doMatchToButtonPress = true;
                }

            //}
        }
        if (buttonDistance > cookingCloseEnoughDistance ||
                    buttonAngleDegrees > cookingCloseEnoughAngle){
            cooking = false;
        }

            //// get info about current animation
            //var animState = anim.GetCurrentAnimatorStateInfo(0);
            //// If the transition to button press has been initiated then we want
            //// to correct the character position to the correct place
            //if (animState.IsName("MatchToButtonPress")
            //&& !anim.IsInTransition(0) && !anim.isMatchingTarget)
            //{
            //    if (buttonPressStandingSpot != null)
            //    {
            //        Debug.Log("Target matching correction started");

            //        initalMatchTargetsAnimTime = animState.normalizedTime;

            //        var t = buttonPressStandingSpot.transform;
            //        anim.MatchTarget(t.position, t.rotation, AvatarTarget.Root,
            //        new MatchTargetWeightMask(new Vector3(1f, 0f, 1f),
            //        1f),
            //        initalMatchTargetsAnimTime,
            //        exitMatchTargetsAnimTime);
            //    }
            //}

        anim.speed = this.animationSpeed;
        anim.SetFloat("velx", Mathf.Abs(cinput.Turn));
        anim.SetFloat("vely", Mathf.Abs(cinput.Forward));
        anim.SetBool("crouching", cinput.Crouch);
        anim.SetBool("running", cinput.Run);
        anim.SetBool("isFalling", !isGrounded);
        anim.SetBool("cooking", cooking);
        //anim.SetBool("matchToButtonPress", doMatchToButtonPress);

    }


    //This is a physics callback
    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {

            ++groundContactCount;

            // Generate an event that might play a sound, generate a particle effect, etc.
            //EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = IsGrounded;// || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (isGrounded)
        {
            //use root motion as is if on the ground		
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower
        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, this.rootMovementSpeed);
        newRootRotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, this.rootTurnSpeed);

        this.transform.position = newRootPosition;
        this.transform.rotation = newRootRotation;

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);

            if (astate.IsName("ButtonPress"))
            {
                float buttonWeight = anim.GetFloat("buttonClose");

                // Set the look target position, if one has been assigned
                if (cookingObject != null)
                {
                    anim.SetLookAtWeight(buttonWeight);
                    anim.SetLookAtPosition(cookingObject.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, buttonWeight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand,
                    cookingObject.transform.position);

                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }


}
