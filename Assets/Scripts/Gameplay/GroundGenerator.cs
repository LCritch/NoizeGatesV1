using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour {

	public GameObject GroundPrefab;
	public GameObject NewTileTrigger;
	public Transform TileSpawnPoint;

	// Use this for initialization
	void Start () {
		
		

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag == "Player" )
		{
			Instantiate(GroundPrefab, TileSpawnPoint.position,TileSpawnPoint.rotation);
		}
		

		
	}
	
	// Fixedupdate to generate the room tiles
	void FixedUpdate () {

	}


}
