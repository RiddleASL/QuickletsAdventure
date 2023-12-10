using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCol : MonoBehaviour
{
    public GameObject parentObj;
    playerMotor pm;
    private void Start() {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player" && !pm.grounded()){
            parentObj.GetComponent<Slime>().death();
            pm.stomp();
            gameObject.SetActive(false);
        }
    }
}
