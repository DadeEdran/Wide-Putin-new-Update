using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterAnimation : MonoBehaviour
{

    Animator animator;

    float VelocityZ = 0.0f;
    float VelocityX = 0.0f;

    float RunVelocity=2f;
    float WalkVelocity=0.5f;

    int RefVelocityZ;
    int RefVelocityX;

    int RefJump;

    float dec=3.5f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        RefVelocityZ = Animator.StringToHash("Velocity Z");
        RefVelocityX = Animator.StringToHash("Velocity X");
        RefJump = Animator.StringToHash("Jump");

    }

    // Update is called once per frame
    void Update()
    {
        //-Define Key 
        bool ForwardPressed = Input.GetKey(KeyCode.W);
        bool RunPressed = Input.GetKey(KeyCode.LeftShift);
        bool RightPressed = Input.GetKey(KeyCode.A);
        bool LeftPressed = Input.GetKey(KeyCode.D);
        bool BackwardsPressed = Input.GetKey(KeyCode.S);
        bool JumpPressed = Input.GetKey(KeyCode.Space);
        //--Define Key 

        float C_V = RunPressed ? RunVelocity : WalkVelocity;

        if (JumpPressed)
        {
           
            animator.SetBool(RefJump,true);

        }
        if (CharacterEvents.sjump)
        {
            animator.SetBool(RefJump, false);
            CharacterEvents.sjump = false;
        }

        if (ForwardPressed)
        {
            if (VelocityZ< C_V )
            {
                VelocityZ += dec*Time.deltaTime;
            }
            else
            {
                if (VelocityZ > 0.6f && !RunPressed)
                {
                    VelocityZ -= dec * Time.deltaTime;
                }
                else
                {
                    VelocityZ = C_V;
                }
                
            }
            
        }


        if (BackwardsPressed)
        {
            if (VelocityZ > -C_V)
            {
                VelocityZ -= dec * Time.deltaTime;
            }
            else
            {
                if (VelocityZ < -0.6f && !RunPressed)
                {
                    VelocityZ += dec * Time.deltaTime;
                }
                else
                {
                    VelocityZ = -C_V;
                }
            }
        }




        if ((!ForwardPressed) && (!BackwardsPressed))
        {
            if (VelocityZ>0.1)
            {
                VelocityZ -= dec * Time.deltaTime;
            }
            else if(VelocityZ<-0.1)
            {
                VelocityZ += dec * Time.deltaTime;
            }
            else
            {
                VelocityZ = 0;
            }   
        }









        if (LeftPressed)
        {
            if (VelocityX < C_V)
            {
                VelocityX += dec * Time.deltaTime;
            }
            else
            {
                if (VelocityX > 0.6f && !RunPressed)
                {
                    VelocityX -= dec * Time.deltaTime;
                }
                else
                {
                    VelocityX = C_V;
                }

            }

        }


        if (RightPressed)
        {
            if (VelocityX > -C_V)
            {
                VelocityX -= dec * Time.deltaTime;
            }
            else
            {
                if (VelocityX < -0.6f && !RunPressed)
                {
                    VelocityX += dec * Time.deltaTime;
                }
                else
                {
                    VelocityX = -C_V;
                }
            }
        }




        if ((!LeftPressed) && (!RightPressed))
        {
            if (VelocityX > 0.1)
            {
                VelocityX -= dec * Time.deltaTime;
            }
            else if (VelocityX < -0.1)
            {
                VelocityX += dec * Time.deltaTime;
            }
            else
            {
                VelocityX = 0;
            }
        }


        animator.SetFloat(RefVelocityZ, VelocityZ);
        animator.SetFloat(RefVelocityX, VelocityX);



    }

}
