using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;
    public float lifetime = 5.0f;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if ( lifetime < 0)
        {
            Explode();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
