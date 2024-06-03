using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform waypoint;


    void Start()
    {
        float kSpeed = Random.Range(0.8f, 1.8f);
        agent.speed = 10 * kSpeed;
        agent.angularSpeed = 180 * kSpeed;
    }


    void Update()
    {
        if (agent) agent.SetDestination(waypoint.position);


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Defense"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }
    }
}
