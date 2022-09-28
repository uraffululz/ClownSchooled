using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid_BalloonScript : MonoBehaviour {
 
	public bool balloonsFull = false;
	public bool dogActive {get; private set;}
	public bool swordActive {get; private set;}
	public bool hatActive {get; private set;}

	[SerializeField] GameObject myDog;
	[SerializeField] GameObject mySword;
	[SerializeField] GameObject myHat;

	[SerializeField] Transform dogSpawn;
	[SerializeField] Transform swordSpawn;
	[SerializeField] Transform hatSpawn;



	void Awake () {
	
	}


	void Start() {
        
    }

   

    void Update() {
        
    }


	public void ActivateDog(Color color) {
		print("Dog activated");
		dogActive = true;
		myDog.GetComponent<MeshRenderer>().material.color = color;
		myDog.transform.position = dogSpawn.position;
		myDog.SetActive(true);

		CheckAvailableBalloons();
	}


	public void ActivateSword(Color color) {
		print("Sword activated");
		swordActive = true;
		mySword.GetComponent<MeshRenderer>().material.color = color;
		mySword.SetActive(true);

		CheckAvailableBalloons();
	}


	public void ActivateHat(Color color) {
		print("Hat activated");
		hatActive = true;
		myHat.GetComponent<MeshRenderer>().material.color = color;
		myHat.SetActive(true);

		CheckAvailableBalloons();
	}


	void CheckAvailableBalloons() {
		if (!dogActive || !swordActive || !hatActive) {
			balloonsFull = false;
		}
		else {
			balloonsFull = true;
		}
	}


}
