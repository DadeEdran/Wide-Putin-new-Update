using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Colider_Checker : MonoBehaviour
{
    
    public Player player = null;

    bool self = false;
    public BoxCollider boxcolider = null;
    public bool enable = false;

    public int q1, q2;

    public int f1, f2;


    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
            return;
        else
        {
            if( (player.quiz[1] == 1 && player.quiz[2] == 3) || player.quiz[2] == 1 && player.quiz[3] == 1)
            {
                boxcolider.enabled = true;
                enable = true;
            }
        }
        
    }
}
