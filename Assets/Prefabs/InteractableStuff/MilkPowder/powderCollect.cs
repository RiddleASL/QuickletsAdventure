using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powderCollect : MonoBehaviour
{
    playerInventory pi;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") {
            pi.full.playerInfo.collected++;
            Destroy(gameObject);
        }
    }
}
