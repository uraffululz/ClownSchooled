using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kid_Move : MonoBehaviour {

	NavMeshAgent nav;
	//[SerializeField] Vector3 nextDestination;
	Kid_BalloonScript balloonScript;

	public enum attentionState {idle, wandering, inLine, atCounter, exitingQueue, satisfied, dissatisfied}
	public attentionState attention;
	[SerializeField] float timeUntilQueue;
	[SerializeField] float currentTimeUntilQueue;

	[SerializeField] Transform[] chosenQueue;
	[SerializeField] int currentQueuePos;
	[SerializeField] float distToQueuePos;

	[SerializeField] float arrivedDist;

	[Header("Wait Time Variables")]
	[SerializeField] int waitTime;
	[SerializeField] float currentWaitTime;
	[SerializeField] float satisfiedWaitTimeMin;
	[SerializeField] float satisfiedWaitTimeMax;
	[SerializeField] float dissatisfiedWaitTimeMin;
	[SerializeField] float dissatisfiedWaitTimeMax;



	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		balloonScript = GetComponent<Kid_BalloonScript>();

		attention = attentionState.idle;
		currentTimeUntilQueue = timeUntilQueue;
		currentWaitTime = waitTime;
	}


	void Start() {
        
    }

   

    void Update() {
		if (!balloonScript.balloonsFull && currentTimeUntilQueue > 0) {
			currentTimeUntilQueue -= Time.deltaTime;
		}

		switch (attention) {
			case attentionState.idle:
				RaycastHit navHit;
				Vector3 navRayPos = new Vector3(Random.Range(-7, 7), 5, Random.Range(-4, 4));

				if (Physics.Raycast(navRayPos, -Vector3.up, out navHit, 10f) && NavMesh.CalculatePath(transform.position, navHit.point, -1, nav.path) ) {
					nav.SetDestination(navHit.point);
					attention = attentionState.wandering;
				}
				else {
					Debug.Log("I don't know where to go!");
				}
				break;
			case attentionState.wandering:
				if (currentTimeUntilQueue <= 0) {
					//nav.ResetPath();
					currentQueuePos = chosenQueue.Length - 1;
					if (!balloonScript.balloonsFull && chosenQueue[currentQueuePos].childCount == 0) {
						nav.areaMask += 1 << NavMesh.GetAreaFromName("Queue");
						UpdateQueuePos(chosenQueue[currentQueuePos]);
						attention = attentionState.inLine;
					}
					else {
						//print("The line is full. I'll have to wait");
						currentTimeUntilQueue = timeUntilQueue;
						attention = attentionState.idle;
					}
				}
				else {
					if (Vector3.Distance(transform.position, nav.destination) <= arrivedDist) {
						attention = attentionState.idle;
					}
					else {
						transform.LookAt(new Vector3(nav.destination.x, transform.position.y, nav.destination.z));
					}
				}
				break;
			case attentionState.inLine:
				distToQueuePos = Vector3.Distance(transform.position, chosenQueue[currentQueuePos].position);

				if (currentQueuePos == 1 && Vector3.Distance(transform.position, chosenQueue[currentQueuePos].position) <= arrivedDist) {
					//balloonScript.RequestBalloon();
					currentWaitTime = waitTime;
					attention = attentionState.atCounter;
				}
				else if (currentQueuePos > 1 && Vector3.Distance(transform.position, chosenQueue[currentQueuePos].position) <= arrivedDist) {
						nav.areaMask = 1 << NavMesh.GetAreaFromName("Queue");

					if (chosenQueue[currentQueuePos - 1].childCount == 0) {
						currentQueuePos--;
						UpdateQueuePos(chosenQueue[currentQueuePos]);
						//print("Moving to next queue position");
					}
				}
				break;
			case attentionState.atCounter:
				//print("There is a KID at the COUNTER");
				if (currentWaitTime > 0) {
					currentWaitTime -= Time.deltaTime;
				}
				else {
					//print("The kid at the counter waited too long!");
					attention = attentionState.dissatisfied;
				}
				break;
			case attentionState.exitingQueue:
				if (Vector3.Distance(transform.position, nav.destination) <= arrivedDist) {
					ExitQueue();
				}
				break;
			case attentionState.satisfied:
				currentTimeUntilQueue = Random.Range(satisfiedWaitTimeMin, satisfiedWaitTimeMax);
				nav.areaMask += 1 << NavMesh.GetAreaFromName("Walkable");
				nav.SetDestination(chosenQueue[0].position);
				transform.parent = null;
				attention = attentionState.exitingQueue;
				break;
			case attentionState.dissatisfied:
				currentTimeUntilQueue = Random.Range(dissatisfiedWaitTimeMin, dissatisfiedWaitTimeMax);
				nav.areaMask += 1 << NavMesh.GetAreaFromName("Walkable");
				nav.SetDestination(chosenQueue[0].position);
				transform.parent = null;
				attention = attentionState.exitingQueue;
				break;
			default:
				Debug.Log("Something wrong here?");
				break;
		}
	}


	void UpdateQueuePos(Transform newPos) {
		//yield return new WaitForSeconds(.5f);
		//print(newPos.name);
		//currentQueuePos = newPos;
		nav.SetDestination(newPos.position);
		transform.parent = newPos;
	}


	void ExitQueue() {
		//print("Left the queue");
		nav.areaMask = 1 << NavMesh.GetAreaFromName("Walkable");
		attention = attentionState.idle;

		///Request a new balloon color/shape
		//balloonScript.RequestBalloon();
	}


}
