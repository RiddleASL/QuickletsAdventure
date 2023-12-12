using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevel : MonoBehaviour
{
    public int level;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(level);
        }
    }
}
