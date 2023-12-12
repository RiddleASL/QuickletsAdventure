using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;

    playerInventory pi;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    public Transform powders;
    public TextMeshProUGUI powderText;

    public GameObject nextLevel;

    public Transform itemSelect;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        Time.timeScale = 1;
    }

    

    void Update()
    {
        if(!PauseMenu.activeSelf)
        {
            musicSlider.value = pi.full.globalInfo.audio.music;
            sfxSlider.value = pi.full.globalInfo.audio.sfx;
            sensitivitySlider.value = pi.full.sensitivity;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        } else if(pi.full.playerInfo.health <= 0)
        {
            PauseMenu.SetActive(false);
        }

        if(powders.childCount > 0)
        {
            powderText.text = "POWDER TUBS: " + powders.childCount.ToString();
            nextLevel.SetActive(false);
        } else
        {
            powderText.text = "POWDER TUBS: 0";
            nextLevel.SetActive(true);
        }

        switch (pi.full.playerInfo.selectedBlock){
            case 0:
                itemSelect.GetChild(0).gameObject.SetActive(true);
                itemSelect.GetChild(1).gameObject.SetActive(false);
                break;
            case 1:
                itemSelect.GetChild(0).gameObject.SetActive(false);
                itemSelect.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Apply(){
        pi.full.globalInfo.audio.music = musicSlider.value;
        pi.full.globalInfo.audio.sfx = sfxSlider.value;
        pi.full.sensitivity = sensitivitySlider.value;
        pi.SaveGame();

        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
