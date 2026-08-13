using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
    static public bool jump = false;
    static public bool sjump = false;
    Animator animator;
    int RefJump;


    static public bool SetsJump
    {
        get { return sjump; }   // get method
        set { sjump = value; }  // set method
    }

    static public bool SetJump
    {
        get { return jump; }   // get method
        set { jump = value; }  // set method
    }
    void Start()
    {
        RefJump = Animator.StringToHash("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void JumpEvent()
    {
        jump = true;
    }
    public void StopJump()
    {
        jump = false;
        sjump = true;
    }
}
