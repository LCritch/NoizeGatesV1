using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
   
    public GameObject SingleSpike;
    public GameObject DoubleSpike;
    public GameObject Player;



	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.GetComponent<ScoreManager>().isAlive = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
