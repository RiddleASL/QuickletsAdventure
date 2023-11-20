using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    playerMotor pm;
    [SerializeField] GameObject marker;
    playerInventory pi;

    public int currSelected = 0;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playerMotor>();
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //Flat camera forward
        Vector3 ca = Camera.main.transform.forward;

        if(Input.GetAxis("Fire2") > 0){
            RaycastHit hit;
            Debug.DrawRay(transform.position + Vector3.up, transform.position + ca, Color.red);
            if(Physics.Raycast(transform.position + Vector3.up, transform.position + ca, out hit, 10f)){
                Debug.Log("hit");
                if(marker.transform.childCount == 0){
                    Instantiate(pi.getInv().blocks[currSelected].block, marker.transform);
                }
                marker.transform.position = hit.point + marker.transform.GetChild(0).GetComponent<Collider>().bounds.extents;
            }else{
                // marker.SetActive(false);
            }
        }
    }
}
