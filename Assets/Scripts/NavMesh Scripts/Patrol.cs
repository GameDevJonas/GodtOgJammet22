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

    public Animator myAnimator;                            //Reference to animator component of child (Jonas)

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();    //Gets FIRST Animator component in children

        agent = GetComponent<NavMeshAgent>();               //Fetches NavMesh agent component

        agent.autoBraking = false;                          //Turns off Autobraking

        Shuffle();

        GotoNextPoint();
    }

    public void Shuffle()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform tempGO;
            int rnd = Random.Range(0, points.Length);
            tempGO = points[rnd];
            points[rnd] = points[i];
            points[i] = tempGO;
        }
    }

    void GotoNextPoint()                                    //Function that makes it so that you go to the next point in the array
    {
        if (points.Length == 0)
            return;
        myAnimator.SetBool("IsMoving", true);
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
        myAnimator.SetBool("IsMoving", false);
        agent.isStopped = true;
        waitTime = Random.Range(1, 11);
        yield return new WaitForSeconds(waitTime);
        GotoNextPoint();
        yield break;
    }

    public void StopMovement()
    {
        myAnimator.SetBool("IsMoving", false);
        agent.isStopped = true;
        agent.updateRotation = false;
        transform.LookAt(GameObject.Find("Player").transform.position);
        StopAllCoroutines();
    }

    public void StartMovement()
    {
        agent.updateRotation = true;
        GotoNextPoint();
    }
}
