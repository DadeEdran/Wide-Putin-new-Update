using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public string TagName = "Player";
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            FindObjectOfType<Player>().key_enter = true;
            Destroy(this.gameObject);
        }

    }
}
