using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortDrop : MonoBehaviour
{
    public string TagName = "Player";
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            FindObjectOfType<Player>().Dec_Short();
            Destroy(this.gameObject);
        }
        
    }
}
