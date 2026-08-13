using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerswiching : MonoBehaviour
{
    public static bool Helicopter = false;
    public CameraMovement C_M;
    public CameraControllerHelicopter C_M_H;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) Helicopter = !Helicopter;

        if (Helicopter)
        {
            C_M.enabled = false;
            C_M_H.enabled = true;
        }
        else
        {
            C_M.enabled = true;
            C_M_H.enabled = false;
        }

    }
}
