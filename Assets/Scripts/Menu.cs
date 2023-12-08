using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject About;
    public GameObject Controls;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        MainMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(true);
    }

    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        Settings.SetActive(false);
    }

        public void AboutButton()
    {
        About.SetActive(true);
        MainMenu.SetActive(false);
    }

        public void MainMenuAboutButton()
    {
        MainMenu.SetActive(true);
        About.SetActive(false);
    }

            public void ControlsBack()
    {
        About.SetActive(true);
        Controls.SetActive(false);
    }

             public void ControlsEnter()
    {
        Controls.SetActive(true);
        About.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}