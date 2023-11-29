using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milkJug : MonoBehaviour
{
    public Transform defaultPos;
    public Transform GFX;
    public float dist = 0.5f;
    public float rotSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GFX.position = defaultPos.position + Mathf.Abs(Mathf.Sin(Time.time * 2)) * Vector3.up * dist;
        GFX.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            playerMotor pm = other.gameObject.GetComponent<playerMotor>();
            if(pm.pi.full.playerInfo.health < pm.maxHealth){
                pm.pi.full.playerInfo.health++;
                Destroy(gameObject);
            }
        }
    }
}
