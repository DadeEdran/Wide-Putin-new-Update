using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Trigger : MonoBehaviour
{
    public string TagName = "Player";
    public Animator animator;
    public bool p_chk = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            animator.SetBool("event", true);
            if (!p_chk)
            {
                p_chk = true;
            }

        }
    }
}
