using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Counter : MonoBehaviour {
 
	GameObject player;
	Player_BalloonControl balloonControl;

	[SerializeField] UIManager UIMan;

	public bool kidAtCounter = false;
	public Kid_BalloonScript currentKidBalloonScript {get; private set;}
	Color[] colorChoices = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
	public Color chosenColor;
	public Mesh[] shapeChoices;
	public Mesh chosenShape;


	void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
		balloonControl = player.GetComponent<Player_BalloonControl>();
    }

   

    void Update() {
        
    }

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			balloonControl.nearCounter = true;
		}
		else if (other.gameObject.CompareTag("Kid")) {
			kidAtCounter = true;
			currentKidBalloonScript = other.GetComponent<Kid_BalloonScript>();
			SetBalloonRequest();
			UIMan.OpenCounterUI(chosenColor, chosenShape.ToString());
			//balloonControl.GetBalloonCriteria(currentKidBalloonScript);
		}
	}


	void OnTriggerExit (Collider other) {
		if (other.gameObject == player) {
			balloonControl.nearCounter = false;
		}
		else if (other.gameObject.CompareTag("Kid")) {
			kidAtCounter = false;
			currentKidBalloonScript = null;
			UIMan.CloseCounterUI();
		}
	}


	public void SetBalloonRequest() {
		///if (currentKidBalloonScript != null) {
		List<Mesh> currentShapeChoices = new List<Mesh>();
		
		if (!currentKidBalloonScript.dogActive) {
			currentShapeChoices.Add(shapeChoices[0]);
		}
		if (!currentKidBalloonScript.swordActive) {
			currentShapeChoices.Add(shapeChoices[1]);

		}
		if (!currentKidBalloonScript.hatActive) {
			currentShapeChoices.Add(shapeChoices[2]);

		}

		chosenColor = colorChoices[Random.Range(0, colorChoices.Length)];
		chosenShape = shapeChoices[Random.Range(0, shapeChoices.Length)];
		//balloonControl.SetBalloonCriteria(currentKidBalloonScript);
		///}
		///else {
		///	Debug.Log("No Balloon Requested");
		///}
	}

}
