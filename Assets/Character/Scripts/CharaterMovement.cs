using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform ground_ck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    bool isground;
    public float jumpheight = 3f;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isground = Physics.CheckSphere(ground_ck.position, grounddistance, groundmask);
        if (isground && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * X + transform.forward * Z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * Time.deltaTime * 2);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        
        if (isground && CharacterEvents.SetJump) //Input.GetButtonDown("Jump") &&
        {
         velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }
        else
        {
            CharacterEvents.SetJump = false;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // Quaternion newdir = Quaternion.LookRotation(move);
        //transform.rotation = newdir;


    }


        


}
