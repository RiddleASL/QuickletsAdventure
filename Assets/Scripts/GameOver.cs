using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverOverlay;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(1);
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
