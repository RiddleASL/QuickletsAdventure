using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFXBlink : MonoBehaviour
{
    Color hit = new Color(1,1,1);
    public List<GameObject> children;

    playerMotor pm;
    // Start is called before the first frame update
    void Start()
    {   
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.isInvincible()){
            hit.g = Mathf.Sin(Time.time * 10) * 0.5f + 0.5f;
            hit.b = Mathf.Sin(Time.time * 10) * 0.5f + 0.5f;
            foreach(GameObject child in children){
                child.GetComponent<Renderer>().material.color = hit;
            }
        } else {
            foreach(GameObject child in children){
                child.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            }
        }
    }
}
