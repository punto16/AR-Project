using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shoot : MonoBehaviour
{

    public Transform arCamera;
    public GameObject projectile;

    public float distanceFromCamera = 5.0f;
    public float shootVelocity = 60.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 bulletInstantiatePos = arCamera.position  + arCamera.forward * distanceFromCamera;
            GameObject bullet = Instantiate(projectile, bulletInstantiatePos, arCamera.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = arCamera.forward * shootVelocity;
        }
    }
}
