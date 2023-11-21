using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Animator anim;
    GameObject player;
    playerMotor pm;

    void Start() {
        anim = GetComponent<Animator>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //reset bools
        resetBool();

        //animation controls
        if(pm.flatMovment().magnitude > 0){
            anim.SetBool("isMoving", true);
        }
        if(pm.getCrouching()){
            anim.SetBool("isCrouching", true);
        }
        if(pm.grounded()){
            anim.SetBool("isGrounded", true);
        }
        anim.SetFloat("moveBlend", pm.getSpeed() / pm.getSprintSpeed());
        anim.SetFloat("yVel", pm.getYVel());
    }

    void resetBool() {
        anim.SetBool("isGrounded", false);
        anim.SetBool("isMoving", false);
        anim.SetBool("isCrouching", false);
    }
}
