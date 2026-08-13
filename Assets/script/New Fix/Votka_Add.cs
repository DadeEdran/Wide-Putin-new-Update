using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Votka_Add : MonoBehaviour
{
    public int addvotka = 1;
    public string TagName = "Player";
    PlayerInfo PlayerI;
    [SerializeField] private GameObject Player = null;


    public bool Help = false;
    public string title = "";
    public string description = "";
    public int set = 0;
    public tutorial_controller t_c = null;
    // Start is called before the first frame update
    void Start()
    {
        PlayerI = Player.GetComponent<PlayerInfo>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            PlayerI.Add_Mana = addvotka;
            gameObject.SetActive(false);
            if (Help)
            {
                t_c.title = title;
                t_c.description = description;
                t_c.set_tutorial(set);
                Time.timeScale = 0f;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
