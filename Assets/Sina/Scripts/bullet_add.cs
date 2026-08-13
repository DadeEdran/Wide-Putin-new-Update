using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class bullet_add : MonoBehaviour
{
    public int addGun1 = 1;
    public string TagName = "Player";

    public bool Help = false;
    public string title = "";
    public string description = "";
    public int set = 0;
    public Player player = null;
    public tutorial_controller t_c = null;
    [SerializeField] private GameObject KeyShowUi = null;

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
            if (starterAssetsInput.E)
            {
                starterAssetsInput.E = false;
                KeyShowUi.SetActive(false);
                player.add_Magazine(addGun1);
                FindObjectOfType<AudioManager>().Play("Reload");
                if (Help)
                {
                    t_c.title = title;
                    t_c.description = description;
                    t_c.set_tutorial(set);
                    Time.timeScale = 0f;
                }
                gameObject.SetActive(false);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
