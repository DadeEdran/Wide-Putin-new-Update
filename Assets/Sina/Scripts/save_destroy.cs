using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_destroy : MonoBehaviour
{
    Player player = null;
    public int id1;
    public int id2;
    public int value1=1;
    public int value2=1;
    public bool and = false;
    public bool or = false;
    public bool onetime = false;

    public bool set_animation = false;
    public Animator animator;
    public string animation_name="event";
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onetime)
            return;

        if (and)
        {
            if (player.quiz[id1]==value1 && player.quiz[id2]==value2)
            {
                
                onetime = true;
                if (set_animation)
                {
                    animator.SetBool("event", true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        if (or)
        {
            if (player.quiz[id1] == value1 || player.quiz[id2] == value2)
            {
                onetime = true;
                if (set_animation)
                {
                    animator.SetBool("event", true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

    }
}
