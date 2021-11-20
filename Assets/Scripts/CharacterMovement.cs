using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // FIRST PERSON CONTROLLER VARIABLES
    public Transform body;
    public Transform camera;

    public float sens=600f;
    public float speed=10f;
    public float jumpForce=1f;
    public float gravity=-9.81f*.5f;

    Rigidbody rb;
    float xRot=0f;
    float verticalVelocity;
    bool grounded;

    void Start(){
        rb=GetComponent<Rigidbody>();

        Cursor.lockState=CursorLockMode.Locked;
    }

    void Update(){
        Look();
        Movement();
    }

    // Function that controls and converts mouse movement to in game head rotations and pivoting
    void Look(){
        float mouseX=Input.GetAxis("Mouse X")*sens*Time.deltaTime;
        float mouseY=Input.GetAxis("Mouse Y")*sens*Time.deltaTime;
        xRot-=mouseY;
        xRot=Mathf.Clamp(xRot,-90f,90f);
        camera.transform.localRotation=Quaternion.Euler(xRot,0f,0f);
        body.Rotate(Vector3.up*mouseX);
    }

    // Function for user to move in the 4 main directions
    void Movement(){
        rb.AddForce(Vector3.down*9.81f,ForceMode.Force);
        float hori=Input.GetAxis("Horizontal");
        float vert=Input.GetAxis("Vertical");
        Vector3 move=transform.right*hori+transform.forward*vert;
        move*=Time.deltaTime*speed;
        move*=1000;
        rb.AddForce(move,ForceMode.Force);
        if(hori==0&&vert==0)rb.velocity=new Vector3(0,rb.velocity.y,0);
        if(rb.velocity.magnitude>10f)rb.velocity=rb.velocity.normalized*10f;
    }
}
