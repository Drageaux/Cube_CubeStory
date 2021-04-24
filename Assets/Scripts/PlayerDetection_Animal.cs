using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerDetection_Animal : MonoBehaviour
{
    public GameObject player;
    public GameObject goldEgg;
    private float distance = 0;
   // private float egg_distance = 0;
    private Animator anim;
    private Animator player_anim;
    private Text Storagetext;
    public Text chickenStorage;
    public Text s_ingStorage;
    Inventory invertory_script;
    private bool added = false;
    private float catchTimer = 15.0f;

    private Vector3 chickenPos = Vector3.zero;
    private Vector3 playerPos = Vector3.zero;
    private Vector3 eggPos = Vector3.zero;
    private Vector3 directionOfCharacter;
    private int numOfChicken;

    private NavMeshAgent agent;
    public GameObject[] waypoint;
    int curentWaypoint = -1;
    public AIState aistate;
    private bool chickenRun=false;

    public enum AIState
    {
        wander,//wander among waypoints
        lay,
        runAngry,
        run,
        getHit
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        player_anim = player.GetComponent<Animator>();


    }
    // Start is called before the first frame update
    void Start()
    {
        // invertory_script = GameObject.Find("Cube").GetComponent<Inventory>();
        invertory_script = player.GetComponent<Inventory>();
        goldEgg.SetActive(false);
        numOfChicken = 0;
        aistate = AIState.wander;
        setNextWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        chickenPos = gameObject.transform.position;
        playerPos = player.transform.position;
        eggPos = goldEgg.transform.position;
        distance = Vector3.Distance(chickenPos, playerPos);
        //  egg_distance= Vector3.Distance(playerPos, eggPos);

        if (aistate != AIState.lay|| chickenRun)
        {
            goldEgg.SetActive(false);
            chickenRun = false;
            if (agent.isStopped == true)
            {
                agent.isStopped = false;
            }
            if (Time.time > catchTimer && distance >= 5)
            {
                aistate = AIState.lay;
            }
            else if (distance < 1.0f)
            {
                Debug.Log("chicken_collected");
                Debug.Log("distance less than 1");
                aistate = AIState.getHit;
                StartCoroutine("WaitForSec");
                //agent.enabled = false;
                //Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
                //if (invertory_script.ingredientList != null)
                //{
                //    if (!invertory_script.ingredientList.ContainsKey("Chicken"))
                //    {
                //        invertory_script.ingredientList.Add("Chicken", 1);
                //    }
                //    else
                //    {
                //        invertory_script.ingredientList["Chicken"]++;

                //    }

                //    chickenStorage.text = "+" + invertory_script.ingredientList["Chicken"];
                //}
                //else
                //{
                //    Debug.Log("can't find ingredient list");
                //}
                updateInventory("Chicken");

            }
            else if (distance < 2.0f)
            {
                Debug.Log("distance less than 2");
                aistate = AIState.runAngry;

            }
            else if (distance < 5.0f)
            {
                //close to chicken, chicken will run away
                //anim.speed = 10;
                Debug.Log("distance less than 5");
                aistate = AIState.run;
            }
            else
            {
                //far from chicken, chicken will wandaring
                // anim.speed = 1;
                aistate = AIState.wander;

            }
        }
        else
        {
            if (distance < 5.0f) {
                Debug.Log("isCrouching" + player_anim.GetBool("crouching"));
                if (player_anim.GetBool("crouching") ==false)
                {
                    catchTimer = Time.time + 20;
                    agent.isStopped = false;
                    chickenRun = true;
                }

                else
                {
                    Debug.Log("crouching");
                    //chickenRun = false;
                    // if (distance < 1.0f&& Input.GetKeyUp(KeyCode.E))
                    if (distance < 1.0f)
                    {
                        Debug.Log("get gold egg and chicken");
                        agent.enabled = false;
                        Destroy(this.gameObject);

                        updateInventory("Chicken");
                        updateInventory("superIngredient");

                    }
                    else
                    {
                        catchTimer = Time.time + 20;
                    }
                   
                }
            }
            else
            {
                aistate = AIState.lay;
            }

        }


        /* AI Switch states */
        switch (aistate)
        {
            case AIState.wander:
                anim.Play("Walk_RM");
                if (agent.remainingDistance - agent.stoppingDistance <= 0 && agent.pathPending == false)
                {
                    setNextWayPoint();
                }
                break;
            case AIState.runAngry:
                anim.Play("RunAngry_RM");
                if (agent.remainingDistance - agent.stoppingDistance <= 0 && agent.pathPending == false)
                {
                    setNextWayPoint();
                }
                break;
            case AIState.run:
                anim.Play("Run_RM");
                if (agent.remainingDistance - agent.stoppingDistance <= 0 && agent.pathPending == false)
                {
                    setNextWayPoint();
                }
                break;
            case AIState.lay:
                Debug.Log("Lay Egg");
                try
                {
                    if (this.gameObject.activeSelf == true)
                    {
                        agent.enabled = true;
                        agent.isStopped = true;
                    }
                    anim.Play("IdleLay");
                    goldEgg.SetActive(true);
                }
                catch{
                    Debug.Log("isStopped");
                }
                break;
            default:
                break;
        }


    }



    void setNextWayPoint()
    {
        curentWaypoint++;
        if (waypoint.Length == 0)
        {
            Debug.LogError("waypoints has no object");
            return;
        }
        if (curentWaypoint == waypoint.Length)
        {
            curentWaypoint = 0;
        }
        //Debug.Log(curentWaypoint);
        agent.SetDestination(waypoint[curentWaypoint].transform.position);
    }

    //touched chicken, chicken collected
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("chicken_collected");
    //    Destroy(this.gameObject);
    //}

    public void updateInventory(string key)
    {
        if (invertory_script.ingredientList != null)
        {
            if (!invertory_script.ingredientList.ContainsKey(key))
            {
                invertory_script.ingredientList.Add(key, 1);
            }
            else
            {
                invertory_script.ingredientList[key]++;

            }
            if (key.Equals("Chicken")) {
                chickenStorage.text = "+" + invertory_script.ingredientList[key];
            }
            else
            {
                s_ingStorage.text = "+" + invertory_script.ingredientList[key];
            }
        }
        else
        {
            Debug.Log("can't find ingredient list");
        }
    }

   
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.5f);
        numOfChicken++;
        agent.enabled = false;
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }
}


