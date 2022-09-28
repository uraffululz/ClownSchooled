using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_ColorSelector : MonoBehaviour {

	GameObject player;
	Player_BalloonControl balloonScript;
 
	[SerializeField] Color myColor;
	Material myMat;


	private void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		balloonScript = player.GetComponent<Player_BalloonControl>();

		myMat = GetComponent<MeshRenderer>().material;
		myMat.color = myColor;
	}


	void Start() {
        
    }

   
    void Update() {
        
    }


	private void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			//balloonScript.SelectBalloonColor(myColor);
			balloonScript.currentColorOption = myColor;
			balloonScript.nearBalloonBin = true;
		}
	}


	private void OnTriggerExit (Collider other) {
		if (other.gameObject == player) {
			//balloonScript.SelectBalloonColor(Color.clear);
			balloonScript.currentColorOption = Color.clear;
			balloonScript.nearBalloonBin = false;
		}
	}
}
