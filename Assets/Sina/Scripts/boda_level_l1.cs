using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boda_level_l1 : MonoBehaviour
{
    public Player player = null;
    public bool en=false;
    public BoxCollider boxc;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    // Update is called once per frame
    void Update()
    {


        if(player.quiz[1]==1 && player.quiz[2]==3 || player.quiz[1] == 1 && player.quiz[2] == 1)
        {
            for (int i = 0; i < player.Boda_Enemies.Length; i++)
            {
                player.Boda_Enemies[i].gameObject.SetActive(false);
            }
            boxc.enabled = true;
            return;
        }
        else
        {
            if (en == false)
            {
                int c = 0;
                int i = 0;
                for (i = 0; i < player.Boda_Enemies.Length; i++)
                {
                    if (player.Boda_Enemies[i].enemydead)
                    {
                        c++;
                    }

                }
                //Debug.Log(c);
                if (c == i)
                {
                    en = true;
                }
                c = 0;
                i = 0; ;
            }
            else
            {
                boxc.enabled = true;
            }
        }
        
        

    }






}
