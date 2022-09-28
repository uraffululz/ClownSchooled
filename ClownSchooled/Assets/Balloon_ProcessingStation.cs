using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_ProcessingStation : MonoBehaviour {
 
	GameObject player;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   

    void Update() {
        
    }


	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			player.GetComponent<Player_BalloonControl>().nearProcessStation = true;
		}
	}


	void OnTriggerExit (Collider other) {
		if (other.gameObject == player) {
			player.GetComponent<Player_BalloonControl>().nearProcessStation = false;
		}
	}
}
