using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camIntersection : MonoBehaviour
{
    Transform player;
    [SerializeField] LayerMask checkLayer;
    GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Linecast(transform.position, player.position, out RaycastHit hit, checkLayer)){
            if(gm == null){
                gm = hit.transform.gameObject;
            } else if(gm.name != hit.transform.gameObject.name){
                gm.GetComponent<materialLogic>().stillIntersecting = false;
                gm = hit.transform.gameObject;
            }
            if(!gm.TryGetComponent<materialLogic>(out materialLogic mat)){
                gm.AddComponent<materialLogic>();
            }
            gm.GetComponent<materialLogic>().closestPoint = hit.point;
        } else{
            gm.GetComponent<materialLogic>().stillIntersecting = false;
            gm = null;
        }
    }
}
