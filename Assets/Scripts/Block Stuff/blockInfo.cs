using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockInfo : MonoBehaviour
{
    public string blockName;
    public int block_id;
    public bool isColliding = false;

    private void OnTriggerStay(Collider other) {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other) {
        isColliding = false;
    }

    public bool getIsColliding() => isColliding;
}
