using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Balloon_Dog : MonoBehaviour {
	 
	NavMeshAgent nav;

	[SerializeField] Transform parentKid;


	void OnEnable () {
		//parentKid = transform.root;
		transform.parent = null;
	}


	void OnDisable () {
		//transform.parent = parentKid;
	}


	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		
	}


	void Start() {
        
    }

   

    void Update() {
        nav.SetDestination(parentKid.position);
		//transform.LookAt(new Vector3(parentKid.transform.position.x, transform.position.y, parentKid.transform.position.z));
    }


}
