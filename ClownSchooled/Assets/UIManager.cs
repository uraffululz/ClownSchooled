using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
 
	[SerializeField] Text scoreText;
	[SerializeField] GameObject shapeSelectBG;

	[SerializeField] GameObject counterBG;
	[SerializeField] Image counterColor;
	[SerializeField] Text counterShape;


	[SerializeField] Image gameOverUI;


    void Start() {
        
    }

   

    void Update() {
        
    }


	public void UpdateScoreUI(int newScore) {
		scoreText.text = "Score: " + newScore.ToString();
	}


	public void OpenShapeSelectUI() {
		shapeSelectBG.SetActive(true);
	}


	public void CloseShapeSelectUI() {
		shapeSelectBG.SetActive(false);
	}


	public void OpenCounterUI(Color reqColor, string reqShape) {
		counterBG.SetActive(true);
		counterColor.color = reqColor;
		counterShape.text = reqShape;
	}

	public void CloseCounterUI() {
		counterBG.SetActive(false);
	}

	public void GameOver() {
		gameOverUI.gameObject.SetActive(true);
	}

	public void Restart() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}
