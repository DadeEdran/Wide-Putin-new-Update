using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SaveBoda : MonoBehaviour
{
    public string TagName = "Player";
    private bool save = false;
    [SerializeField] private GameObject KeyShowUi = null;
    public bool saveForsave = false;
    private StarterAssetsInputs starterAssetsInput;



    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();


    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(true);
            if (!save)
            {
                if (starterAssetsInput.E)
                {
                    other.GetComponent<Player>().SavePlayer();
                    save = true;
                    saveForsave = true;
                    FindObjectOfType<AudioManager>().Play("Boda1");

                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(false);
            save = false;
        }
    }

}
