using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powderCollect : MonoBehaviour
{
    playerInventory pi;
    SFXController sfx;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") {
            pi.full.playerInfo.collected++;
            sfx.PlaySound(4);

            Destroy(gameObject);
        }
    }
}
