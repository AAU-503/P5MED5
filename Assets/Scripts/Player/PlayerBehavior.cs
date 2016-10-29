using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

	public CameraController cameraController;
	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;
	public float jumpSpeed = 20.0f;
	public float gravity = 2.0f;

	private bool attacked = true;

	// Use this for initialization
	void Start ()
	{
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		controller = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update ()
	{
		
		Jump ();
		Movement ();
	}

	void OnTriggerEnter (Collider other)
	{ // Ability to pick up coins adding to score in ScoreManager and attacking boxes
        
		print (other.gameObject);
        
		if (other.gameObject.CompareTag ("Coin")) {
			Destroy (other.gameObject);
			ScoreManager.ChangeScore (ScoreManager.coinsScore);
		}

		if (other.gameObject.tag == "Enemy") {
			ScoreManager.ChangeScore (ScoreManager.enemyFailScore);
		}
	}

	void Movement ()
	{
		transform.Translate (CameraController.speed, 0, 0);
	}

	void Jump ()
	{
		if (controller.isGrounded) {

			if (Input.GetKeyDown (KeyCode.Space)) {
				moveDirection.y = jumpSpeed;
			}

		} else {
			moveDirection.y -= gravity;
		}

		controller.Move (moveDirection * Time.fixedDeltaTime);
	}

}