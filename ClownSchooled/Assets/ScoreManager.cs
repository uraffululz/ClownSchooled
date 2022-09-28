using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	[SerializeField] UIManager UIMan;
 
	int totalScore;



    void Start() {
        AlterScore(0);
    }

   

    public void AlterScore(int amount) {
        totalScore += amount;
		UIMan.UpdateScoreUI(totalScore);
    }


}
