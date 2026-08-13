using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breaking : MonoBehaviour
{
    public Rigidbody[] childrig;
    public GameObject break_object = null;
    public int force = 100;

    private void Awake()
    {
        childrig = break_object.GetComponentsInChildren<Rigidbody>();

    }


    public bool br = false;
    private void Update()
    {
        if (br)
        {

            break_object.SetActive(true);

            foreach (Rigidbody rbt in childrig)
                rbt.AddForce(Vector3.forward * force);

            Destroy(this.gameObject);
        }
    }
}