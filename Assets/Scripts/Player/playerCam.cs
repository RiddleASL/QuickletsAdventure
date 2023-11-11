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
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, maxDown, maxUp);

        yRot += mouseX;

        transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}
