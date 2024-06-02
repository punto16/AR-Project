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

    public float shootInterval = 0.1f;

    private bool isShooting = false;

    public void OnPointerDown()
    {
        isShooting = true;
        StartCoroutine(ShootContinuously());
    }

    public void OnPointerUp()
    {
        isShooting = false;
        StopCoroutine(ShootContinuously());
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void ShootBullet()
    {
        Vector3 bulletInstantiatePos = arCamera.position + arCamera.forward * distanceFromCamera;
        GameObject bullet = Instantiate(projectile, bulletInstantiatePos, arCamera.rotation) as GameObject;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = arCamera.forward * shootVelocity;
        }
    }
}
