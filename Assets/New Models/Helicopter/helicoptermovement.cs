using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicoptermovement : MonoBehaviour
{
    //Select CharacterPlayer
    public CharacterController CharacterController;

    public float SpeedCopter = 15f;
    //Gravity You Can Change It.

    public float Speed = 2f;
    //Defualt Speed

    public float SpeedRun = 2f;
    //Defualt SpeedRun

    public Transform Ground_Chacker;
    // Get Location Of Object For Chack Ground

    public LayerMask GroundMask;
    // Layer of Ground 

    bool IsGround;
    //Chacker For Ground

    Vector3 Vector_Player;
    //Location Of player

    public float jumpheight = 1f;
    //Defualt heightJump

    public float GroundDistance = 0.4f;



    // Start is called before the first frame update
    void Start()
    {
        //CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controllerswiching.Helicopter) return;
        

            var CameraForeWardDirection = Camera.main.transform.forward;
            Debug.DrawRay(Camera.main.transform.position, CameraForeWardDirection * 10, Color.red);
            var DirectiontoMoveIn = Vector3.Scale(CameraForeWardDirection, (Vector3.right + Vector3.forward));
            Debug.DrawRay(Camera.main.transform.position, DirectiontoMoveIn * 10, Color.blue);
            transform.forward = DirectiontoMoveIn;

            //IsGround = Physics.CheckSphere(Ground_Chacker.position, GroundDistance, GroundMask);
            //if (IsGround && Vector_Player.y < 0)
            //{
            //    Vector_Player.y = -2f;
            //}

            float X = Input.GetAxis("Horizontal");
            float Z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * X + transform.forward * Z;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharacterController.Move(move * Speed * Time.deltaTime * SpeedRun);
            }
            else
            {
                CharacterController.Move(move * Speed * Time.deltaTime);
            }

            //if (IsGround && CharacterEvents.SetJump) 
            //{
            // Vector_Player.y = Mathf.Sqrt(jumpheight * -2f * Gravity);
            //}
            //else
            //{
            //CharacterEvents.SetJump = false;
            //}



            //if (IsGround && Input.GetKey(KeyCode.Space))
            //{
            //    Vector_Player.y = Mathf.Sqrt(jumpheight * -2f * Gravity);
            //}
            if (Input.GetKey(KeyCode.Space))
            {
                Vector_Player.y = SpeedCopter * Time.deltaTime;
                CharacterController.Move(Vector_Player * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                Vector_Player.y = -SpeedCopter * Time.deltaTime;
                CharacterController.Move(Vector_Player * Time.deltaTime);
            }



        



    }
}
