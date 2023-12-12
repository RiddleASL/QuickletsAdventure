using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveScripts : MonoBehaviour
{
    playerInventory pi;
    playerMotor pm;

    public float saveInterval = 30f;
    float currTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //* Autosave 
        currTime += Time.deltaTime;
        if(currTime >= saveInterval && pm.grounded() && !pm.standingOnBlock()){
            currTime = 0f;
            Debug.Log("Autosaving...");
        }
    }
}
