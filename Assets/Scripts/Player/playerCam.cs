using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour
{
    public float sensitivity = 100f;
    
    float xRot = 0f;
    float yRot = 0f;

    public float maxUp = 90f;
    public float maxDown = -90f;
    // Start is called before the first frame update
    void Start()
    {
        //lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
