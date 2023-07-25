using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeManager : Attraction {
 
	//Transform cakeSlot;

	[SerializeField] GameObject[] cakeOptions;
	GameObject cake;
	public List<GameObject> piecesRemaining;

	[SerializeField] GameObject sceneMan;

	


	void Awake () {
		//cakeSlot = transform;
	}


	void Start() {
        SpawnCake();
    }

   
    void Update() {
        
    }


	void SpawnCake() {
		cake = Instantiate(cakeOptions[Random.Range(0, cakeOptions.Length)], transform.position, Quaternion.identity, transform);
		piecesRemaining = new List<GameObject>();
		int pieceCount = cake.transform.childCount;
		for (int i = 0; i < pieceCount; i++) {
			piecesRemaining.Add(cake.transform.GetChild(i).gameObject);
		}

		print("Cake has " + piecesRemaining.Count + " pieces remaining.");
	}

	public void EatCake() {
		UIManager uiMan = sceneMan.GetComponent<UIManager>();

		cake.transform.GetChild(piecesRemaining.Count - 1).gameObject.SetActive(false);
		piecesRemaining.RemoveAt(piecesRemaining.Count - 1);

		print("Cake has " + piecesRemaining.Count + " pieces remaining.");

		if (piecesRemaining.Count <= 0) {
			///GAME OVER
			uiMan.GameOver();
			Time.timeScale = 0;
			print("PARTY OVER. The cake is all gone.");
		}
	}


	public override void AttendAttraction () {
		///What does the cake actually do? Healing? Stamina? Joy?
		GivePoints();
		EatCake();
	}


	public override void GivePoints () {
		sceneMan.GetComponent<ScoreManager>().AlterScore(myPoints);
	}
}
