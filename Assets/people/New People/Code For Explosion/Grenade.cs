using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3;
    public float force = 700f;
    float countdown;
    bool hasExploded = false;
    public ParticleSystem bigbang;
    public float radius = 5;
    public Transform ts;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && hasExploded== false)
        {
            Explode();
            hasExploded = true;

        }
    }

    void Explode()
    {
        Instantiate(bigbang, new Vector3(-60, 4, -118),Quaternion.identity);
        bigbang.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
        foreach (Collider  nearbyobject in colliders)
        {
            Rigidbody rb = nearbyobject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);


            }
        }
    }
}
