using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    //Volume controls
    playerInventory pi;

    public GameObject[] sfxClips;
    public GameObject[] musicClips;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        musicClips[0].GetComponent<AudioSource>().volume = pi.full.globalInfo.audio.music * pi.full.globalInfo.audio.master;
    }

    public void PlaySound(int index)
    {
        GameObject sfx = Instantiate(sfxClips[index], player.position, Quaternion.identity);
        sfx.GetComponent<AudioSource>().volume = pi.full.globalInfo.audio.sfx * pi.full.globalInfo.audio.master;
    }
}
