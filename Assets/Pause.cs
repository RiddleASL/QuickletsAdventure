using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;

    void Update()
    {
        void PauseGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        void ResumeGame()   {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
