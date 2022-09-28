using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
 
	Vector3 colCenter;
	Vector3 colSize;

	[SerializeField] Transform[] queuePositions;


	void Awake () {
		BoxCollider col = GetComponent<BoxCollider>();
		colCenter = transform.position;
		colSize = new Vector3(col.size.x, col.size.z, col.size.y);
	}


	void Start() {
        
    }

   

    void Update() {
        
    }


	private void OnDrawGizmos () {
		Gizmos.DrawWireCube(colCenter, colSize);
	}


}
