using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public float globalVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

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
        musicClips[0].GetComponent<AudioSource>().volume = musicVolume * globalVolume;
    }

    public void PlaySound(int index){
        GameObject sfx = Instantiate(sfxClips[index], player.position, Quaternion.identity);
        sfx.GetComponent<AudioSource>().volume = sfxVolume * globalVolume;
    }
}
