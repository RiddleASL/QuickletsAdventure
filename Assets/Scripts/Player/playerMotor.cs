using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
    CharacterController con;
    [HideInInspector] public playerInventory pi;
    blocks b;
    playerSounds ps;

    Vector3 movement;
    float yVel;
    public int maxHealth = 3;

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
    [SerializeField] Vector3 groundVolume;
    bool isGrounded = true;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask blockMask;

    [SerializeField] Transform GFX;
    [SerializeField] float rotSpeed = 5f;

    [SerializeField] float invincibilityTime = 1.0f;
    float invincibilityTimer = 0f;
    bool invincible = false;

    bool aiming;
    bool interacting;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        b = GameObject.FindGameObjectWithTag("Global").GetComponent<blocks>();

        con = GetComponent<CharacterController>();
        currSpeed = speed;
        maxSpeed = speed;
        ps = GetComponent<playerSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is on the ground
        isGrounded = Physics.CheckBox(groundCheck.position, groundVolume, groundCheck.rotation, groundMask);

        //aiming
        if(Input.GetAxisRaw("Fire2") == 1 && !isCrouching && isAlive()){
            aiming = true;
        }
        else
        {
            aiming = false;
        }

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
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching && isAlive()){
            yVel = jumpForce;
            ps.jump();
        }

        //speed modifires
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            if (currSpeed < speed * sprintMod)
            {
                currSpeed += acceloration;
            }
            maxSpeed = speed * sprintMod;
        }
        else if (isCrouching)
        {
            if (currSpeed > speed * crouchMod)
            {
                currSpeed -= acceloration;
            }
            if (maxSpeed > speed * crouchMod)
            {
                maxSpeed -= acceloration;
            }
        }
        else
        {
            if (currSpeed > speed)
            {
                currSpeed -= acceloration;
            }
            else if (currSpeed < speed)
            {
                currSpeed += acceloration;
            }
            if (maxSpeed > speed)
            {
                maxSpeed -= acceloration;
            }
            else if (maxSpeed < speed)
            {
                maxSpeed += acceloration;
            }
        }

        //crouch
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            con.height = defaultHeight * crouchMod;
            isCrouching = true;
            con.center = new Vector3(0, 0.5f, 0);
        }
        else if (!Physics.CheckSphere(transform.position + Vector3.up * 1.5f, 0.2f, groundMask))
        {
            con.height = defaultHeight;
            isCrouching = false;
            con.center = new Vector3(0, 1, 0);
        }

        //apply movement
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * currSpeed;
        movement = Vector3.ClampMagnitude(Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movement, maxSpeed);
        movement.y = yVel;
        if(!isAlive()){
            movement = Vector3.zero;
        }
        con.Move(movement * Time.deltaTime);

        //rotate GFX
        Vector3 GFXmovement = new Vector3(movement.x, 0, movement.z);
        if (GFXmovement != Vector3.zero && !aiming)
        {
            GFX.rotation = Quaternion.Slerp(GFX.rotation, Quaternion.LookRotation(GFXmovement), rotSpeed * Time.deltaTime);
        }

        //Change Selected Block
        if(Input.GetKeyDown(KeyCode.E) && isAlive()){
            if(pi.full.playerInfo.selectedBlock < pi.full.inventory.blocks.Count - 1){
                pi.full.playerInfo.selectedBlock++;
            }
            else
            {
                pi.full.playerInfo.selectedBlock = 0;
            }
        } else if(Input.GetKeyDown(KeyCode.Q) && isAlive()){
            if(pi.full.playerInfo.selectedBlock > 0){
                pi.full.playerInfo.selectedBlock--;
            }
            else
            {
                pi.full.playerInfo.selectedBlock = pi.full.inventory.blocks.Count - 1;
            }
        }

        //Invincibility Timer
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
            invincible = true;
        }
        else
        {
            invincible = false;
        }

    }


    public void heal()
    {
        //heal player
        if (pi.full.playerInfo.health < maxHealth)
        {
            pi.full.playerInfo.health += 1;
            ps.heal();
            Debug.Log("heal");
        }
    }

    public void takeDamage()
    {
        //take damage
        pi.full.playerInfo.health -= 1;
        invincibilityTimer = invincibilityTime;
        ps.takeDamage();
    }

    public void stomp(){
        //! !!!STOMPH!!!
        yVel = jumpForce/1.5f;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundVolume);
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
    public bool isAiming() => aiming;
    public Transform GFXTransform() => GFX;
    public bool standingOnBlock() => Physics.CheckBox(groundCheck.position, groundVolume, groundCheck.rotation, blockMask);
    public bool isInvincible() => invincible;
    public LayerMask getGroundLayer() => groundMask;
    public bool isAlive() => pi.full.playerInfo.health > 0;
}
