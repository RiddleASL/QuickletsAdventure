using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powderCollect : MonoBehaviour
{
    playerInventory pi;
    SFXController sfx;

    public Transform defaultPos;
    public Transform GFX;
    public float dist = 0.5f;
    public float rotSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Global").GetComponent<playerInventory>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXController>();
        GFX.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        GFX.position = defaultPos.position + Mathf.Abs(Mathf.Sin(Time.time * 2)) * Vector3.up * dist;
        GFX.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") {
            pi.full.playerInfo.collected++;
            sfx.PlaySound(4);

            Destroy(gameObject);
        }
    }
}
