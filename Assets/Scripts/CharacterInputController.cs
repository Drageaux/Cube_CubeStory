using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{

    //public string Name = "George P Burdell";
    public Transform cam;

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;

    private Health healthScript;

    public Vector3 Direction
    {
        get;
        private set;
    }

    public bool Moving
    {
        get;
        private set;
    }

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Trap
    {
        get;
        private set;
    }

    public bool Interact
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    public bool Crouch
    {
        get;
        private set;
    }

    public bool Run
    {
        get;
        private set;
    }


    float turnSmoothVelocity;

    private void Start()
    {
        healthScript = GetComponent<Health>();
        if (healthScript == null)
            Debug.Log("Health script could not be found");
    }


    void Update()
    {
        if (!healthScript.Alive())
        {
            return;
        }

        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
        this.Direction = new Vector3(h, 0f, v).normalized;

        if (this.Direction.magnitude >= 0.05f)
        {
            float targetAngle = Mathf.Atan2(this.Direction.x, this.Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity , 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
        //if (Input.GetKey(KeyCode.Q))
        //    h = -0.5f;
        //else if (Input.GetKey(KeyCode.E))
        //    h = 0.5f;

        this.Run = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.Run = true;
        } 

        if (Input.GetKeyUp(KeyCode.C))
        {
            this.Crouch = !this.Crouch;
        }

        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //    forwardSpeedLimit = 0.1f;
        //else if (Input.GetKeyUp(KeyCode.Alpha2))
        //    forwardSpeedLimit = 0.2f;
        //else if (Input.GetKeyUp(KeyCode.Alpha3))
        //    forwardSpeedLimit = 0.3f;
        //else if (Input.GetKeyUp(KeyCode.Alpha4))
        //    forwardSpeedLimit = 0.4f;
        //else if (Input.GetKeyUp(KeyCode.Alpha5))
        //    forwardSpeedLimit = 0.5f;
        //else if (Input.GetKeyUp(KeyCode.Alpha6))
        //    forwardSpeedLimit = 0.6f;
        //else if (Input.GetKeyUp(KeyCode.Alpha7))
        //    forwardSpeedLimit = 0.7f;
        //else if (Input.GetKeyUp(KeyCode.Alpha8))
        //    forwardSpeedLimit = 0.8f;
        //else if (Input.GetKeyUp(KeyCode.Alpha9))
        //    forwardSpeedLimit = 0.9f;
        //else if (Input.GetKeyUp(KeyCode.Alpha0))
        //    forwardSpeedLimit = 1.0f;
        //END ANALOG ON KEYBOARD DEMO CODE  


        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;

        Interact = Input.GetKeyDown(KeyCode.E);
        // TODO: prevent this when clicking on UI; may have to remove click button
        Trap = Input.GetKeyUp(KeyCode.F);
        Jump = Input.GetButtonDown("Jump");

        Moving = Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.W) 
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D);
    }
}
