using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform waypoint;
    public float movementSpeed = 4;
    public float rotationSpeed = 6;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LookAt();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, movementSpeed * Time.deltaTime);

        var distance = Vector3.Distance(transform.position, waypoint.position);
    }

    private void LookAt()
    {
        var dir = waypoint.position - transform.position;
        var rootTarget = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
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
