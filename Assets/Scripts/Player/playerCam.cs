using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour
{
    playerMotor pm;

    public float sensitivity = 100f;
    
    float xRot = 0f;
    float yRot = 0f;

    public float maxUp = 90f;
    public float maxDown = -90f;

    GameObject gfx;

    public Transform defaultPos;
    public Transform aimPos;
    // Start is called before the first frame update
    void Start()
    {
        //lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gfx = GameObject.FindGameObjectWithTag("Player").transform.Find("GFX").gameObject;
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Camera Pos
        if(pm.isAiming() && pm.isAlive()){
            // Camera.main.transform.position = aimPos.position;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, aimPos.position, 0.1f);
            gfx.transform.localRotation = Quaternion.Euler(0, yRot, 0f);
        } else {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, defaultPos.position, 0.1f);
        }

        //Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //rotate camera
        if(pm.isAlive()){
            yRot += mouseX;
            xRot -= mouseY;
        }

        //clamp camera rotation
        xRot = Mathf.Clamp(xRot, maxDown, maxUp);

        //apply rotation
        transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}
