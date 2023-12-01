using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    public float speed;
    float currSpeed;
    public float f;

    public float detectionRange = 15f;
    public GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < detectionRange && agent.enabled){
            //Speed
            currSpeed = Mathf.Sin(Time.time * f) * speed;
            currSpeed = Mathf.Abs(currSpeed);
            agent.speed = currSpeed + .1f;

            //Destination
            agent.SetDestination(player.transform.position);
        }
    }

    public void death(){
        agent.enabled = false;
        transform.Find("GFX").gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 10);
        deathEffect.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !other.gameObject.GetComponent<playerMotor>().isInvincible()){
            other.gameObject.GetComponent<playerMotor>().takeDamage();
            Debug.Log("Hit");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
