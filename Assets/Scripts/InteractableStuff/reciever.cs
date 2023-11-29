using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reciever : MonoBehaviour
{
    [SerializeField] string recieverType;
    public bool interact;

    void Update(){
        if(interact){
            switch(recieverType){
                case "ExtandableBridge":
                    extendableBridge();
                    break;
                default:
                    Debug.Log("No reciever type found");
                    break;
            }
        }
    }

    void extendableBridge(){
        Transform platform = transform.Find("Platform");
        platform.localPosition = Vector3.Lerp(platform.localPosition, Vector3.zero, 0.5f * Time.deltaTime);
        if(Vector3.Distance(platform.localPosition, Vector3.zero) < 0.1f){
            
        }
    }
}
