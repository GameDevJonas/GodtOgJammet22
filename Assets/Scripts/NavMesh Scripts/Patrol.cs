using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

    public Transform[] points;                               //Array containing transforms for patrolling points
    private int destPoint = 0;
    private NavMeshAgent agent;                             //Reference to NavMesh agent
    public float waitTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();               //Fetches NavMesh agent component

        agent.autoBraking = false;                          //Turns off Autobraking

        
        GotoNextPoint();
    }

    void GotoNextPoint()                                    //Function that makes it so that you go to the next point in the array
    {
        if (points.Length == 0)
            return;
        agent.isStopped = false;
           

        agent.destination = points[destPoint].position;     //Tells the agent to go to the current destination
        destPoint = (destPoint + 1) % points.Length;        //Choose the next point in the array as the destination


    }
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !agent.isStopped)
            StartCoroutine(WaitForSeconds());
    }

    IEnumerator WaitForSeconds()
    {
        agent.isStopped = true;
        waitTime = Random.Range(1, 11);
        yield return new WaitForSeconds(waitTime);
        GotoNextPoint();
        yield break;
    }

}
