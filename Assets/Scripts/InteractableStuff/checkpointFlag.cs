using System.Collections;
using System.Collections.Generic;
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
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !isActive){
            pi.full.playerInfo.safePosition.x = other.gameObject.transform.position.x;
            pi.full.playerInfo.safePosition.y = other.gameObject.transform.position.y;
            pi.full.playerInfo.safePosition.z = other.gameObject.transform.position.z;
            pi.full.playerInfo.safePosition.y += 1;
            pi.full.globalInfo.checkPointPos.x = transform.position.x;
            pi.full.globalInfo.checkPointPos.y = transform.position.y;
            pi.full.globalInfo.checkPointPos.z = transform.position.z;
            pi.SaveGame();
            isActive = true;
        }
    }
}
