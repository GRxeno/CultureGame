using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIscript : MonoBehaviour
{

    public Transform player;

    NavMeshAgent agent;
    public Animator ani;
    public GameObject aim_point;

    public float walk_speed;
    public float run_speed;
    public static bool canWalk;


    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        canWalk = false;

    }

    void Update()
    {
        if (canWalk)
        {

            float dist = Vector3.Distance(transform.position, player.position);
            //Debug.Log("Distance between Player and NPC: " + dist);

            // WALK
            if (dist > 5.0f && dist < 15.0f)
            {

                agent.speed = walk_speed;
                agent.SetDestination(player.position);
                ani.SetInteger("arms", 1);
                ani.SetInteger("legs", 1);
            }
            // RUN
            if (dist > 15.0f)
            {
                agent.speed = run_speed;
                agent.SetDestination(player.position);
                ani.SetInteger("arms", 2);
                ani.SetInteger("legs", 2);
            }
            // STOP
            if (dist < 5.0f)
            {
                agent.speed = 0;
                ani.SetInteger("arms", 5);
                ani.SetInteger("legs", 5);
            }
        }
        else
        {
            if(agent.speed != 0)
            {
                agent.speed = 0;
                ani.SetInteger("arms", 5);
                ani.SetInteger("legs", 5);
            }
        }
    }
}
