using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;

    public void QuitToMenu(){
        SceneManager.LoadScene(0);
    }
}
