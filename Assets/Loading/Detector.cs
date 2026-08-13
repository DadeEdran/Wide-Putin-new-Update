using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public string TagName = "Player";
    [SerializeField] private GameObject KeyShowUi=null;
    [SerializeField] public string Name_The_Next_Level_Play="Main_Menu";
    public int level = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            Level_Loader_Script.enterCK = true;
            Level_Loader_Script.NameLevel = Name_The_Next_Level_Play;
            Level_Loader_Script.level = level;
            KeyShowUi.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        { 
            Level_Loader_Script.enterCK = false;
            KeyShowUi.SetActive(false);
        }
    }
}
