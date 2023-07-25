using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentManager : Attraction {

	[SerializeField] GameObject sceneMan;
 
	[SerializeField] GameObject[] presentPositions;
	[SerializeField] List<GameObject> presents;


    void Start() {
        
    }

   

    void Update() {
        
    }


	public override void AttendAttraction () {
		GivePoints();
		DropPresent();
	}


	public override void GivePoints () {
		sceneMan.GetComponent<ScoreManager>().AlterScore(myPoints);

	}


	void DropPresent() {

	}
}
