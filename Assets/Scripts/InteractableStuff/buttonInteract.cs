using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInteract : MonoBehaviour
{
    [SerializeField] GameObject interactObj;
    bool isInteracting;

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")){
            if(Input.GetKey(KeyCode.F) && !isInteracting){
                isInteracting = true;
                interactObj.GetComponent<reciever>().interact = true;
            }
        }
    }
}
