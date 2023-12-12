using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class checkpointFlag : MonoBehaviour
{
    playerInventory pi;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive){
            transform.Find("Flag").GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
        } else {
            transform.Find("Flag").GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !isActive){
            pi.SaveGame();
            isActive = true;
        }
    }
}
