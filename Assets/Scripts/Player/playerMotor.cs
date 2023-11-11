using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
    CharacterController con;

    Vector3 movement;
    float yVel;

    float currSpeed;
    public float speed = 5f;
    public float sprintMod = 1.5f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    bool isGrounded;
    public LayerMask groundMask;

    public Transform GFX;
    public float rotSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        con = GetComponent<CharacterController>();
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        
        //gravity
        if (isGrounded && yVel < 0)
        {
            yVel = -2f;
        }
        else
        {
            yVel -= gravity * Time.deltaTime;
        }

        //player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVel = jumpForce;
        }

        //player movement
        float xVel = Input.GetAxis("Horizontal");
        float zVel = Input.GetAxis("Vertical");

        //sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = speed * sprintMod;
        }
        else
        {
            currSpeed = speed;
        }

        //apply movement
        movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * currSpeed;
        movement = Vector3.ClampMagnitude(Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0) * movement, speed);
        movement.y = yVel;
        con.Move(movement * Time.deltaTime);

        //rotate GFX
        Vector3 GFXmovement = new Vector3(movement.x, 0, movement.z);
        if(GFXmovement != Vector3.zero)
        {
            GFX.rotation = Quaternion.Slerp(GFX.rotation, Quaternion.LookRotation(GFXmovement), rotSpeed * Time.deltaTime);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
