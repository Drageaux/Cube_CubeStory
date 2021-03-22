using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection_Wolf : MonoBehaviour
{
    
    public GameObject player;

    float minDistance = 5f;//if change, change FanShapedArea.cs as well
    float minAngle = 90f;//if change, change FanShapedArea.cs as well

    private Vector3 wolfPos=Vector3.zero;
    private Vector3 playerPos = Vector3.zero;
    private Vector3 directionOfCharacter;

    private Animator anim;
   // private int randomLookAround;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        wolfPos = gameObject.transform.position;
        playerPos = player.transform.position;

        float distance = Vector3.Distance(wolfPos, playerPos);

        //the vector of player and wolf
        Vector3 srcLocalVect = playerPos - wolfPos;
        srcLocalVect.y = 0;

        //get a point in the right front of the wolf
        Vector3 forwardLocalPos = gameObject.transform.forward * 1 + wolfPos;

        //get the vector of the right front
        Vector3 forwardLocalVect = forwardLocalPos - wolfPos;
        forwardLocalVect.y = 0;

        //calculate the angle of player and wolf
        float angle = Vector3.Angle(srcLocalVect, forwardLocalVect);

        if (distance < minDistance && angle < minAngle / 2)
        {
            Debug.Log("In Wolf EyeSight");
            directionOfCharacter = player.transform.position - gameObject.transform.position;
            directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards
            gameObject.transform.Translate(directionOfCharacter * anim.speed, Space.World);
            anim.Play("IdleAggressive");
            WaitForSec();
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        anim.Play("Run_RM");


    }
}
