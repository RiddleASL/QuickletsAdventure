using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    //Volume Floats
    public float globalVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    //Volume Sliders
    public Slider SFXSlider;
    public Slider MusicSlider;
    
    public GameObject[] sfxClips;
    public GameObject[] musicClips;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        musicClips[0].GetComponent<AudioSource>().volume = MusicSlider.value * globalVolume;
    }

    public void PlaySound(int index){
        GameObject sfx = Instantiate(sfxClips[index], player.position, Quaternion.identity);
        sfx.GetComponent<AudioSource>().volume = SFXSlider.value * globalVolume;
    }
}
