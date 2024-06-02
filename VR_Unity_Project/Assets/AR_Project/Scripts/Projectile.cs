using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Reduce health
            Destroy(collision.gameObject);
        }
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
