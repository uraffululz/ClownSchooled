using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_BalloonControl : MonoBehaviour {

	[SerializeField] Balloon_Counter counterScript;

	PlayerMove moveSript;
	[SerializeField] UIManager UIMan;
	[SerializeField] ScoreManager scoreMan;
 
	[SerializeField] GameObject balloonGO;
	[SerializeField] Mesh balloonBaseMesh;

	bool carryingBalloon;
	//[SerializeField] GameObject balloonParent;
	public Color currentColorOption;
	public int currentShapeIndex;
	[SerializeField] Mesh currentShapeOption;
	[SerializeField] bool currentBalloonProcessed = false;
	public enum shapeChoice {none, dog, sword, hat}
	shapeChoice currentShapeChoice = shapeChoice.none;

	public bool nearBalloonBin = false;
	public bool nearProcessStation = false;
	public bool nearCounter = false;



	private void Awake () {
		moveSript = GetComponent<PlayerMove>();
		currentColorOption = Color.clear;
	}


	void Start() {
        
    }
   

    void Update() {
        
    }


	public void OnProcessBalloon(InputValue processing) {
		if (processing.isPressed) {
			if (!carryingBalloon && nearBalloonBin) {
				PickupBalloon();
			}
			else if (carryingBalloon && nearProcessStation && !currentBalloonProcessed) {
				UIMan.OpenShapeSelectUI();
				moveSript.canMove = false;
			}
			else if (carryingBalloon && nearCounter && currentBalloonProcessed && counterScript.kidAtCounter) {
				DeliverBalloon();
			}
			else {
				ResetBalloon();
			}

		}
	}


	public void SelectBalloonColor(Color newColor) {
		currentColorOption = newColor;
	}


	public void PickupBalloon() {
		carryingBalloon = true;
		balloonGO.SetActive(true);

		balloonGO.GetComponent<MeshRenderer>().material.color = currentColorOption;
	}


	public void SelectBalloonShape(int shapeIndex) {
		currentShapeChoice = shapeChoice.none;

		switch (shapeIndex) {
			case 0:
				currentShapeChoice = shapeChoice.dog;
				break;
			case 1:
				currentShapeChoice = shapeChoice.sword;
				break;
			case 2:
				currentShapeChoice = shapeChoice.hat;
				break;
			default:
				break;
		}

		currentShapeOption = counterScript.shapeChoices[shapeIndex];
		UIMan.CloseShapeSelectUI();
		TwistBalloon();
	}


	void TwistBalloon() {
		balloonGO.GetComponent<MeshFilter>().mesh = currentShapeOption;
		currentBalloonProcessed = true;
		moveSript.canMove = true;

		//print("You twisted a " + currentShapeOption.name + " balloon");
	}


	void DeliverBalloon() {
		DetermineBalloonValue();
		GiveBalloon();
		ResetBalloon();
	}


	void DetermineBalloonValue() {
		///Compare balloon criteria with the requests of the kid at the counter
		int score = 0;
		bool colorMatches = false;
		bool shapeMatches = false;

		if (balloonGO.GetComponent<MeshRenderer>().material.color == counterScript.chosenColor) {
			colorMatches = true;
			score += 10;
		}
		else {
			Debug.Log("Balloon colors mismatch: " + balloonGO.GetComponent<MeshRenderer>().material.color.ToString() + " || " + counterScript.chosenColor.ToString());
		}

//TODO Make this comparison more exact (comparing vertexCount for now, because balloonGO's mesh is counted as an "instance", and is therefore not comparable to the original mesh)
		if (balloonGO.GetComponent<MeshFilter>().mesh.vertexCount == counterScript.chosenShape.vertexCount) {
			shapeMatches = true;
			score += 10;
		}
		else {
			Debug.Log("Balloon shape mismatch: " + balloonGO.GetComponent<MeshFilter>().mesh.ToString() + " || " +  counterScript.chosenShape.ToString());
		}

		print("You earned " + score + " points!");
		scoreMan.AlterScore(score);

		Kid_Move kidMoveScript = counterScript.currentKidBalloonScript.GetComponent<Kid_Move>();

		if (colorMatches && shapeMatches) {
			kidMoveScript.attention = Kid_Move.attentionState.satisfied;
		}
		else {
			kidMoveScript.attention = Kid_Move.attentionState.dissatisfied;
		}
	}


	void GiveBalloon() {
		Kid_BalloonScript kidScript = counterScript.currentKidBalloonScript;
		Color passColor = balloonGO.GetComponent<MeshRenderer>().material.color;

		switch (currentShapeChoice) {
			case shapeChoice.none:
				break;
			case shapeChoice.dog:
				kidScript.ActivateDog(passColor);
				break;
			case shapeChoice.sword:
				kidScript.ActivateSword(passColor);
				break;
			case shapeChoice.hat:
				kidScript.ActivateHat(passColor);
				break;
			default:
				break;
		}
	}


	public void ResetBalloon () {
		carryingBalloon = false;
		balloonGO.SetActive(false);
		balloonGO.GetComponent<MeshFilter>().mesh = balloonBaseMesh;
		currentBalloonProcessed = false;
		currentColorOption = Color.clear;
		currentShapeOption = null;
		currentShapeChoice = shapeChoice.none;
	}
}
