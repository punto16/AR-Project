using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform arCamera;
    public GameObject projectile;

    public float distanceFromCamera = 5.0f;
    public float shootVelocity = 120.0f;
    public float shootInterval = 0.5f;
    public float overHeatCooldown = 5.0f;

    public uint maxBullets = 100;
    private uint bulletCounter = 0;

    private bool overHeat = false;
    private bool isShooting = false;
    private bool weaponRecharging = false;

    public void Update()
    {
        if(overHeat)
        {
            
        }
    }

    public void OnPointerDown()
    {
        if (!overHeat && !isShooting)
        {
            Debug.Log("Start Shooting");
            isShooting = true;
            if(bulletCounter > 0 && weaponRecharging)
            {
                Debug.Log("Stop Recharging");
                weaponRecharging = false;
                StopCoroutine(WeaponRecharge());
            }
            StartCoroutine(ShootContinuously());
        }
        
    }

    public void OnPointerUp()
    {
        if (!overHeat && isShooting)
        {
            Debug.Log("Stop Shooting");
            isShooting = false;
            weaponRecharging = true;
            StopCoroutine(ShootContinuously());
            StartCoroutine(WeaponRecharge());         
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (isShooting && !overHeat)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootInterval);
        }
    }
    
    private IEnumerator WeaponOverheat()
    {
        yield return new WaitForSeconds(overHeatCooldown);
        Debug.Log("End overheat");
        bulletCounter = 0;
        overHeat = false;
        StopCoroutine(WeaponOverheat());
    }

    private IEnumerator WeaponRecharge()
    {
        Debug.Log("Start Recharging");
        while (bulletCounter > 0 && weaponRecharging)
        {
            bulletCounter--;
            if (bulletCounter == 0)
            {
                Debug.Log("Stop Recharging");
                weaponRecharging = false;
                StopCoroutine(WeaponRecharge());
            }

            yield return new WaitForSeconds(shootInterval*2f);
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

        bulletCounter++;

        if (bulletCounter >= maxBullets)
        {
            overHeat = true;
            isShooting = false;
            StopCoroutine(ShootContinuously());
            StartCoroutine(WeaponOverheat());
        }
    }
}
