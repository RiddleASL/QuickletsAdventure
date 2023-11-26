using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    playerInventory pi;
    playerMotor pm;
    blocks b;

    public float interactionRange = 3f;
    public LayerMask mask;
    public LayerMask blockMask;
    
    //Box stuff
    public Transform boxcheckPos;
    public Vector3 boxcheckSize;

    GameObject lookingATBlock;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        b = GameObject.FindGameObjectWithTag("Global").GetComponent<blocks>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //* Block Placement
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactionRange, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange, mask) && pm.isAiming()){
            Debug.Log(1);
            if(transform.Find("blockPlace").childCount == 0 && pi.full.inventory.blocks[pi.full.playerInfo.selectedBlock].count > 0){
                Instantiate(b.blocksList[pi.full.playerInfo.selectedBlock], transform.Find("blockPlace").position, transform.Find("blockPlace").rotation, transform.Find("blockPlace"));
                int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
                transform.Find("blockPlace").GetChild(0).GetChild(0).GetChild(0).gameObject.layer = LayerIgnoreRaycast;
            } else if(transform.Find("blockPlace").childCount != 0 && pi.full.inventory.blocks[pi.full.playerInfo.selectedBlock].count == 0){
                Destroy(transform.Find("blockPlace").GetChild(0).gameObject);
            }

            //Child Access Stuff (I KNOW ITS UGLY BUT IT WORKS)
            Vector3 blockPlaceRot = new Vector3(transform.Find("blockPlace").transform.eulerAngles.x, pm.GFXTransform().eulerAngles.y, transform.Find("blockPlace").transform.eulerAngles.z);

            transform.Find("blockPlace").transform.position = hit.point;
            transform.Find("blockPlace").transform.eulerAngles = blockPlaceRot; 
            transform.Find("blockPlace").GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            transform.Find("blockPlace").GetChild(0).GetChild(0).GetChild(0).GetComponent<Collider>().isTrigger = true;
            bool isColliding = transform.Find("blockPlace").GetChild(0).GetChild(0).GetChild(0).GetComponent<blockInfo>().getIsColliding();

            if(Input.GetAxisRaw("Fire1") == 1 && transform.Find("blockPlace").childCount != 0 && pi.full.inventory.blocks[pi.full.playerInfo.selectedBlock].count > 0){
                if(!isColliding){
                    pi.blockCount(pi.full.playerInfo.selectedBlock, -1);
                    Instantiate(b.blocksList[pi.full.playerInfo.selectedBlock], transform.Find("blockPlace").position, transform.Find("blockPlace").rotation);
                    Debug.Log(1);
                }
            }
            
            //Collision visuals
            if(isColliding){
                transform.Find("blockPlace").GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.red);
            } else {
                transform.Find("blockPlace").GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.green);
            }
        } else {
            if(transform.Find("blockPlace").childCount != 0){
                Destroy(transform.Find("blockPlace").GetChild(0).gameObject);
            }
        }

        //* Block Pickup
        Debug.DrawRay(transform.position + (Vector3.up * .2f), pm.GFXTransform().forward * interactionRange/3, Color.green);
        // Physics.Raycast(transform.position + (Vector3.up * .2f), pm.GFXTransform().forward, out hit, interactionRange/3, blockMask);
        if(Physics.BoxCast(boxcheckPos.position, boxcheckSize, pm.GFXTransform().forward, out hit, pm.GFXTransform().rotation, interactionRange/3, blockMask)){
            // Debug.Log("block");
            if(lookingATBlock == null){
                lookingATBlock = hit.collider.gameObject;
            } else if(lookingATBlock != hit.collider.gameObject){
                lookingATBlock.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.black);
                lookingATBlock = hit.collider.gameObject;
            }

            //Change material outline color to Yellow
            if(Input.GetAxisRaw("Fire2") == 0){
                lookingATBlock.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.yellow);
            }

            //Pickup block
            if(Input.GetMouseButtonDown(0) && Input.GetAxisRaw("Fire2") == 0){
                pickupBlock(lookingATBlock);
                lookingATBlock = null;  
            }
        } else {
            //Change material outline color to Black
            lookingATBlock.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.black);
            lookingATBlock = null;
        }
    }

    public void pickupBlock(GameObject block){
        pi.blockCount(block.GetComponent<blockInfo>().block_id, 1);
        Destroy(block.transform.parent.gameObject.transform.parent.gameObject);
    }
}
