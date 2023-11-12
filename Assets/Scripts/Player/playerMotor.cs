using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
    CharacterController con;

    Vector3 movement;
    float yVel;

    float currSpeed;
    float maxSpeed;
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintMod = 1.5f;
    [SerializeField] float acceloration = 0.5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = 9.81f;

    float defaultHeight = 2f;
    [SerializeField] float crouchMod = 0.5f;
    bool isCrouching;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    bool isGrounded = true;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform GFX;
    [SerializeField] float rotSpeed = 5f;
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
        if (isGrounded && yVel < 0){
            yVel = -2f;
        }else{
            yVel -= gravity * Time.deltaTime;
        }

        //player jump
        if (Input.GetButton("Jump") && isGrounded && !isCrouching){
            yVel = jumpForce;
        }

        //player movement
        float xVel = Input.GetAxis("Horizontal");
        float zVel = Input.GetAxis("Vertical");

        //speed modifires
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching){
            if(currSpeed < speed * sprintMod){
                currSpeed += acceloration;
            }
            maxSpeed = speed * sprintMod;
        }else if(isCrouching){
            if(currSpeed > speed * crouchMod){
                currSpeed -= acceloration;
            }
            if(maxSpeed > speed * crouchMod){
                maxSpeed -= acceloration;
            }
        }else{
            if(currSpeed > speed){
                currSpeed -= acceloration;
            } else if(currSpeed < speed){
                currSpeed += acceloration;
            }
            if(maxSpeed > speed){
                maxSpeed -= acceloration;
            } else if(maxSpeed < speed){
                maxSpeed += acceloration;
            }
        }

        //crouch
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded){
            con.height = defaultHeight * crouchMod;
            isCrouching = true;
            con.center = new Vector3(0, 0.5f, 0);
        }else if(!Physics.CheckSphere(transform.position + Vector3.up * 1.5f, 0.2f, groundMask)){
            con.height = defaultHeight;
            isCrouching = false;
            con.center = new Vector3(0, 1, 0);
        }

        //apply movement
        movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * currSpeed;
        movement = Vector3.ClampMagnitude(Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0) * movement, maxSpeed);
        movement.y = yVel;
        con.Move(movement * Time.deltaTime);

        //rotate GFX
        Vector3 GFXmovement = new Vector3(movement.x, 0, movement.z);
        if(GFXmovement != Vector3.zero){
            GFX.rotation = Quaternion.Slerp(GFX.rotation, Quaternion.LookRotation(GFXmovement), rotSpeed * Time.deltaTime);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 1.5f, 0.2f);
    }

    //accesable functions
    public bool isMoving() => movement.magnitude > 0;
    public bool grounded() => isGrounded;
    public float getSpeed() => currSpeed;
    public float getSprintSpeed() => speed * sprintMod;
    public Vector3 flatMovment() => new Vector3(movement.x, 0, movement.z);
    public bool getCrouching() => isCrouching;
    public float getYVel() => yVel;
}
