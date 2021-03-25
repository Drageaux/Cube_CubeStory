using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerDetection_Wolf : MonoBehaviour
{
    
    public GameObject player;
    private const float damage = 50f;
    private const float hitRate = 2f;
    private float attackTimer;

    float minDistance = 10f;//if change, change FanShapedArea.cs as well
    float minAngle = 90f;//if change, change FanShapedArea.cs as well

    private Vector3 wolfPos=Vector3.zero;
    private Vector3 playerPos = Vector3.zero;
    private Vector3 directionOfCharacter;

    private Animator anim;
    private NavMeshAgent agent;
    public GameObject[] waypoint;
    int curentWaypoint = -1;

    public enum AIState
    {
        wander,//wander among waypoints
        chase
    }
    public AIState aistate;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        aistate = AIState.wander;
        setNextWayPoint();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > attackTimer)
        {
            anim.SetBool("caught", true);
        }
        wolfPos = gameObject.transform.position;
          playerPos = player.transform.position;
          float distance = Vector3.Distance(wolfPos, playerPos);

          //calculate the angle of wolf and player
          Vector3 srcLocalVect = playerPos - wolfPos;
          srcLocalVect.y = 0;
          Vector3 forwardLocalPos = gameObject.transform.forward * 1 + wolfPos;
          Vector3 forwardLocalVect = forwardLocalPos - wolfPos;
          forwardLocalVect.y = 0;
          float angle = Vector3.Angle(srcLocalVect, forwardLocalVect);

          //in wolf eyesight, wolf chase
          if (distance < minDistance && angle < minAngle / 2)
          {
              aistate = AIState.chase;
              
          }
          else
          {
                aistate = AIState.wander;
                anim.SetBool("detected", false);
          }

          //wolf caught
          if (distance <1f && angle < minAngle / 2 && Time.time > attackTimer)
          {
              Debug.Log("Wolf Caught Player");
              anim.SetBool("caught", true);
              player.GetComponent<Health>().GetHit(damage);
              attackTimer = Time.time + hitRate;
          }
          else
          {
              anim.SetBool("caught", false);
          }

        
        switch (aistate)
        {
            case AIState.wander:
                if(agent.remainingDistance-agent.stoppingDistance<=0 && agent.pathPending==false)
                {
                    setNextWayPoint();
                }
                break;
            case AIState.chase:
                chasePlayer();
                break;
            default:
                break;
        }
    }

    void chasePlayer()
    { 
        directionOfCharacter = playerPos - wolfPos;
        directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards

        //facing player
        Quaternion q = Quaternion.LookRotation(playerPos - wolfPos);
        transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, q, 3 * Time.deltaTime);
        gameObject.transform.Translate(directionOfCharacter * 0.02f, Space.World);
        //anim.Play("IdleAggressive");
        anim.SetBool("detected", true);
        //WaitForSec();
    }

    void setNextWayPoint()
    {
        curentWaypoint++;
        if(waypoint.Length==0)
        {
            Debug.LogError("waypoints has no object");
            return;
        }
        if(curentWaypoint==waypoint.Length)
        {
            curentWaypoint = 0;
        }
        //Debug.Log(curentWaypoint);
        agent.SetDestination(waypoint[curentWaypoint].transform.position);
    }
    /* IEnumerator WaitForSec()
     {
         yield return new WaitForSeconds(1);
         anim.Play("Run_RM");
     }*/


}
