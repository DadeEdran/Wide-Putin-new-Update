using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redroomkey : MonoBehaviour
{

    public string TagName = "Player";
    public Player player = null;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            player.keyforredroom = true;
            Destroy(this.gameObject);
        }

    }


}
