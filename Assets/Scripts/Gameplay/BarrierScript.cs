using UnityEngine;
using System.Collections;

public class BarrierScript : MonoBehaviour {

    public GameObject BarrierRef;
    public GameObject PlayerCube;
    public BoxCollider2D ColliderRef;

    public bool isDestroyed;

	// Use this for initialization
	void Start () {

	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player is currently dashing, if true change the anim state to broken barrier
        if(other.tag == "Player" && other.GetComponent<PlayerController>().isDashing)
        {
            other.GetComponent<ScoreManager>().BarrierDestroyed = true;
            ColliderRef.enabled = false;
            
        }

        else if(other.tag == "Player")
        {
            PlayerCube.GetComponent<ScoreManager>().isAlive = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        other.GetComponent<ScoreManager>().BarrierDestroyed = false;
        ColliderRef.enabled = true;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
