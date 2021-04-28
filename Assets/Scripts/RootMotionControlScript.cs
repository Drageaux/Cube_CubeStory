using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(Health), typeof(Inventory))]
public class RootMotionControlScript : MonoBehaviour
{
    InteractionManager interactionManager;
    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputController cinput;
    private Health healthScript;
    private Inventory inventory;
    private MysteryBoxCollector mysteryBoxCollector;
    private TrapPlacer trapPlacer;

    private Transform leftFoot;
    private Transform rightFoot;

    public float pickupTime = 1.6f;
    public float cookingTime = 4f; // in seconds
    public float mysteryBoxOpenTime = 4f;
    public float settingTrapTime = 3f;
    private float remainingTimer;

    public bool picking = false;
    public bool cooking = false;
    public bool openingMysteryBox = false;
    public bool settingTrap = false;

    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;
    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

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

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");


        healthScript = GetComponent<Health>();
        if (healthScript == null)
            Debug.Log("Health script could not be found");

        inventory = GetComponent<Inventory>();
        if (inventory == null)
            Debug.Log("Inventory could not be found");

        mysteryBoxCollector = GetComponent<MysteryBoxCollector>();
        if (mysteryBoxCollector == null)
            Debug.Log("MysteryBoxCollector could not be found");

        trapPlacer = GetComponent<TrapPlacer>();
        if (trapPlacer == null)
            Debug.Log("TrapPlacer could not be found");
    }


    // Use this for initialization
    void Start()
    {
        interactionManager = InteractionManager.instance;

        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("Root/J_Bip_C_Hips/J_Bip_L_UpperLeg/J_Bip_L_LowerLeg/J_Bip_L_Foot");
        rightFoot = this.transform.Find("Root/J_Bip_C_Hips/J_Bip_R_UpperLeg/J_Bip_R_LowerLeg/J_Bip_R_Foot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");
    }




    void Update()
    {
        if (!healthScript.Alive())
        {
            return;
        }
        if (settingTrap && Time.time > remainingTimer)
        {
            settingTrap = false;
            trapPlacer.PlaceTrap();
        }
        // Interact and Stop Animation
        if (interactionManager.CurrentTarget != null)
        {
            // Catching Animal
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dive Catch"))
            {
                if (interactionManager.CurrentTarget.type == InteractableType.AnimalIngredient)
                {
                    AnimalIngredient animal = (AnimalIngredient)interactionManager.CurrentTarget;
                    if (animal.caught)
                        inventory.PickUpIngredient(animal);
                }

            }
            if (cooking && Time.time > remainingTimer)
            {
                cooking = false;
                inventory.FinishCooking();
            }
            if (picking && Time.time > remainingTimer)
            {
                picking = false;
                if (interactionManager.CurrentTarget.type == InteractableType.Ingredient)
                {
                    inventory.PickUpIngredient((ItemPickup)interactionManager.CurrentTarget);
                }
            }
            if (openingMysteryBox && Time.time > remainingTimer)
            {
                openingMysteryBox = false;
                if (interactionManager.CurrentTarget.name == "Mystery Box")
                {
                    mysteryBoxCollector.CollectBox();
                }
            }
        } 

        //bool doMatchToButtonPress = false;

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground.
        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
        //work
        bool isGrounded = IsGrounded;// || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);


        // Play Animation
        if (cinput.Interact)
        {
            if (interactionManager.CurrentTarget != null)
            {
                this.RotateTowards(interactionManager.CurrentTarget.transform);
                if (interactionManager.CurrentTarget.type == InteractableType.Ingredient)
                {
                    //transform.LookAt(interactionManager.CurrentTarget.transform);
                    Debug.Log("Picking initiated");

                    picking = true;
                    remainingTimer = Time.time + pickupTime;
                }
                else if (interactionManager.CurrentTarget.type == InteractableType.Tool)
                {
                    if (interactionManager.CurrentTarget.name == "Pan")
                    {
                        if (inventory.HasEnoughIngredients())
                        {
                            Debug.Log("Action pressed");
                            //if (buttonDistance <= buttonCloseEnoughForMatchDistance)
                            //{
                            {
                                Debug.Log("Cooking initiated");

                                cooking = true;
                                remainingTimer = Time.time + cookingTime;
                            }
                        }
                        else
                        {
                            inventory.lackIngredient.SetActive(true);
                            StartCoroutine("WaitForSec");
                        }
                    } 
                    else if (interactionManager.CurrentTarget.name == "Mystery Box")
                    {
                        openingMysteryBox = true;
                        remainingTimer = Time.time + mysteryBoxOpenTime;
                    }
                } else if (interactionManager.CurrentTarget.type == InteractableType.AnimalIngredient) {
                    anim.Play("Dive Catch");
                }
            }
        }
        
        bool clickedTrap = cinput.Trap;
        if (clickedTrap)
        {
            settingTrap = true;
            remainingTimer = Time.time + settingTrapTime;
        }

        if (cinput.Moving)
        {
            CancelInteraction();
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
        anim.SetBool("usingTool", cooking || openingMysteryBox);
        anim.SetBool("picking", picking || settingTrap);
        //anim.SetBool("matchToButtonPress", doMatchToButtonPress);

    }

    private void CancelInteraction()
    {
        cooking = false;
        picking = false;
        openingMysteryBox = false;
        settingTrap = false;
        remainingTimer = Time.time;
    }

    private void RotateTowards(Transform target)
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                        this.transform.position.y,
                                        target.position.z);
        this.transform.LookAt(targetPostition);
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

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        inventory.lackIngredient.SetActive(false);
    }
}
