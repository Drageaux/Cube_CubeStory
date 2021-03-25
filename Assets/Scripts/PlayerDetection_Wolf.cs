using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerDetection_Wolf : MonoBehaviour
{
    
    public GameObject player;
    private const float damage = 40f;
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
        chase,
        idle
    }
    public AIState aistate;

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attackTimer = Time.time;
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
        anim.ResetTrigger("caught");
        if (Time.time < attackTimer)
        {
            aistate = AIState.idle;
        }
        else if (player.GetComponent<Health>().Alive())
        {
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

            /* AI State conditions */
            //in wolf eyesight, wolf chase
            if (distance < minDistance && angle < minAngle / 2)
            {
                //in wolf eyesight but if attacking on cooldown, don't move
                if (Time.time > attackTimer)
                {
                    aistate = AIState.chase;
                } 
                else
                {
                    aistate = AIState.idle;
                }
            }
            else
            {
                aistate = AIState.wander;
                anim.SetBool("detected", false);
            }

            /* Attack condition */
            //wolf caught
            if (distance < 1.2f)
            {
                if(Time.time > attackTimer)
                {
                    Debug.Log("Wolf Caught Player");
                    anim.SetTrigger("caught");
                    player.GetComponent<Health>().GetHit(damage);
                    attackTimer = Time.time + hitRate;
                } 
            }
        } 
        else
        {
            anim.SetBool("detected", false);
            aistate = AIState.wander;
        }

        /* AI Switch states */
        switch (aistate)
        {
            case AIState.wander:
                agent.isStopped = false;
                if (agent.remainingDistance-agent.stoppingDistance<=0 && agent.pathPending==false)
                {
                    setNextWayPoint();
                }
                break;
            case AIState.chase:
                chasePlayer();
                break;
            case AIState.idle:
                Debug.Log("wait");
                agent.isStopped = true;
                gameObject.transform.Translate(directionOfCharacter * 0f, Space.World);
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
