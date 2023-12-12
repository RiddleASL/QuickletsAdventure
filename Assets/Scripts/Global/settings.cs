using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    playerInventory pi;

    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();

        musicSlider.value = pi.full.globalInfo.audio.music;
        sfxSlider.value = pi.full.globalInfo.audio.sfx;
        sensitivitySlider.value = pi.full.sensitivity;
    }

    public void Apply(){
        pi.full.globalInfo.audio.music = musicSlider.value;
        pi.full.globalInfo.audio.sfx = sfxSlider.value;
        pi.full.sensitivity = sensitivitySlider.value;
        pi.SaveGame();
    }
}
