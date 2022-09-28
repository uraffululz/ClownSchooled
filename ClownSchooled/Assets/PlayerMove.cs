using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

	Rigidbody rb;
 
	public bool canMove;
	Vector2 moveDirection;
	[SerializeField] float moveForce;
	[SerializeField] float decelSpeed;


	void Awake () {
		rb = GetComponent<Rigidbody>();
	}


	void Start() {
        
    }

   

    void FixedUpdate() {
		if (canMove && moveDirection != Vector2.zero) {
			Vector3 currentMove = new Vector3(moveDirection.x, 0, moveDirection.y);
			rb.AddForce(currentMove * moveForce);
			transform.LookAt(transform.position + currentMove);
		}
		else {
			rb.AddForce(-rb.velocity * decelSpeed);
		}
		
        
    }


	public void OnMove(InputValue moveInput) {
		moveDirection = moveInput.Get<Vector2>();

		//if (!moveInput.isPressed) {
		//	moveDirection = Vector2.zero;
		//}
	}


}
