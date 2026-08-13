using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    //public Collider MainCollider;
    public Collider[] AllColliders;
    public GameObject gameobject;
    public bool iskinematic=false;

    private void Awake()
    {
        //MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
    }
    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
        {
            col.enabled = isRagdoll;
            col.GetComponent<Rigidbody>().isKinematic = iskinematic;
            col.GetComponent<Rigidbody>().velocity = Vector3.down*10;
        }
            
        //MainCollider.enabled = !isRagdoll;
        //GetComponent<Rigidbody>().useGravity = !isRagdoll;
        gameobject.GetComponent<Animator>().enabled = !isRagdoll;
        
    }
}