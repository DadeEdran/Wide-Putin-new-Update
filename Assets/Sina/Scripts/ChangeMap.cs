using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour
{
    [System.Serializable]
    public class MultiDimensionalInt
    {
        public GameObject GO;
    }
    public MultiDimensionalInt[] AllGO;
    public int Enable=0;
    public bool ck = false;
    private void OnTriggerStay(Collider other)
    {
        if (ck == true)
            return;

        for (int i = 0; i < AllGO.Length; i++)
        {
            if (i == Enable)
            {
                AllGO[i].GO.SetActive(true);
            }
            else
            {
                AllGO[i].GO.SetActive(false);
            }
            
        }
        ck = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ck = false;
    }
}
