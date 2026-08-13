using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriger : MonoBehaviour
{
    public string TagName = "Player";
    public Animator animator;
    public GameObject particle;
    public Transform particlepposition;
    public bool p_chk=false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            animator.SetBool("event",true);
            if (!p_chk)
            {
                GameObject impactGO = Instantiate(particle, particlepposition);
                Destroy(impactGO, 10f);
                p_chk = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag(TagName))
        //{
            //animator.SetBool("event", false);
        //}
    }
}
