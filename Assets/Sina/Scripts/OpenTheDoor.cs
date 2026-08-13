using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class OpenTheDoor : MonoBehaviour
{
    public string TagName = "Player";
    [SerializeField] private GameObject KeyShowUi = null;
    public Animator animator;
    public bool p_chk = false;
    private StarterAssetsInputs starterAssetsInput;

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    private void OnTriggerStay(Collider other)
    {
        KeyShowUi.SetActive(true);
        if (other.CompareTag(TagName))
        {
            if (starterAssetsInput.E)
            {
                starterAssetsInput.E = false;
                Debug.Log("this shit should run one time");
                if (p_chk == false)
                {
                    animator.SetBool("Open", true);
                    p_chk = true;
                }
                else
                {
                    animator.SetBool("Open", false);
                    p_chk = false;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        KeyShowUi.SetActive(false);

    }


}
