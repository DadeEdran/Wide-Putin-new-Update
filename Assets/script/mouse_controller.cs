using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_controller : MonoBehaviour
{
    public float mouse_sensitivity = 100f;
    public Transform Player_body;
    float xRotation=0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") *mouse_sensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        Player_body.Rotate(Vector3.up*mouseX);


    }
}
