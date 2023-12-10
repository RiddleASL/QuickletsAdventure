using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour
{
    //Bones
    public Transform leftFoot;
    public Transform rightFoot;
    public float stepRadius = 0.2f;

    bool stepLeft = true;
    bool stepRight = true;

    //Sounds
    SFXController sfx;

    LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXController>();
        groundLayer = GetComponent<playerMotor>().getGroundLayer();
    }

    // Update is called once per frame
    void Update()
    {
        //footsteps
        if(Physics.CheckSphere(leftFoot.position, stepRadius, groundLayer) && !stepLeft)
        {
            stepLeft = true;
            sfx.PlaySound(0);
        } else if(!Physics.CheckSphere(leftFoot.position, stepRadius, groundLayer))
        {
            stepLeft = false;
        }

        if (Physics.CheckSphere(rightFoot.position, stepRadius, groundLayer) && !stepRight)
        {
            stepRight = true;
            sfx.PlaySound(0);
        }
        else if (!Physics.CheckSphere(rightFoot.position, stepRadius, groundLayer))
        {
            stepRight = false;
        }

        
    }
    
    public void jump(){
        sfx.PlaySound(1);
    }

    public void heal(){
        sfx.PlaySound(2);
    }

    public void takeDamage(){
        sfx.PlaySound(3);
    }
}
