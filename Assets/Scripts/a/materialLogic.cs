using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialLogic : MonoBehaviour
{
    [HideInInspector] public bool stillIntersecting;
    [HideInInspector] public Vector3 closestPoint;
    [HideInInspector] public float size;
    [HideInInspector] public Transform player;
    Material mat;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stillIntersecting = true;
    }

    void Update()
    {
        if(stillIntersecting){
            mat.SetVector("_spherePos", closestPoint + Vector3.up);
            mat.SetFloat("_Opacity", 1f);
            mat.SetFloat("_size", Mathf.Lerp(mat.GetFloat("_size"), Mathf.Clamp(Vector3.Distance(closestPoint, player.position), .5f , 6f), .1f));
        } else{
            if(mat.GetFloat("_size") > 0){
                mat.SetFloat("_size", mat.GetFloat("_size") - .2f);
            }
            if(mat.GetFloat("_size") <= 0){
                mat.SetFloat("_Opacity", 0f);
                Destroy(GetComponent<materialLogic>());
            }
        }
    }
}
