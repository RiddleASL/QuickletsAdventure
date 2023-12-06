using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    [SerializeField] float timeToDestruct = 1f;

    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, timeToDestruct);
    }
}
