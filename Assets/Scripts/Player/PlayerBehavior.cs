using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

	public CameraController cameraController;
	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;
	public float jumpSpeed = 10.0f;
    public float airTime = 2f;
	public float gravity = 20.0f;
    public float gravityForce = 3.0f;

	private bool attacked = true;
    private float forceY = 0;
    private float invertGravity;

    // Use this for initialization
    void Start ()
	{
		cameraController = GameObject.Find ("Main Camera").GetComponent<CameraController> ();
		controller = GetComponent<CharacterController> ();
        invertGravity = gravity * airTime;
    }

	// Update is called once per frame
	void Update ()
	{
		
		Jump ();
		Movement ();
        forceY -= gravity * Time.deltaTime * gravityForce;
        moveDirection.y = forceY;
        controller.Move(moveDirection * Time.deltaTime);
    }

	void Movement ()
	{
		transform.Translate (CameraController.speed * Time.deltaTime, 0, 0);
	}

	void Jump ()
	{
		if (controller.isGrounded) {
            forceY = 0;
            invertGravity = gravity * airTime;
			if (Input.GetKeyDown (KeyCode.Space)) {
				forceY = jumpSpeed;
			}

		}
        if(Input.GetKey(KeyCode.Space) && forceY != 0)
        {
            invertGravity -= Time.deltaTime;
            forceY += invertGravity * Time.deltaTime;
        }
       
	}
}