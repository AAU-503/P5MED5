using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

	public CameraController cameraController;
	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;
	public static float jumpSpeed = 15;
    public static float airTime = 2f;
	public static float gravity = 65.0f;
    public static float gravityForce = 3.0f;
    private bool attacked = true;
    private float forceY = 0;
    private float invertGravity;
    private bool jumpQueued = false;
    private bool reset = true;


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

        if (transform.position.y < -1.0) {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }

	void Movement ()
	{
		transform.Translate (CameraController.speed * Time.deltaTime, 0, 0);
	}


    IEnumerator SwitchJumpQueued(float delayTime) {
        jumpQueued = true;
        yield return new WaitForSeconds(delayTime);
        if (!Input.GetKey(KeyCode.Space)) {
            jumpQueued = false;
        }
    }

    void Jump ()
	{
		if (controller.isGrounded) {
            forceY = 0;
            invertGravity = gravity * airTime;

            if (Input.GetKeyDown (KeyCode.Space) || jumpQueued) {
                
                forceY = jumpSpeed;
                jumpQueued = false;
                reset = false;

            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                reset = true;
            }

        }
        if (!controller.isGrounded)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (reset) {
                    StartCoroutine(SwitchJumpQueued(0.1f));
                }
                reset = true;
            }

            if (Input.GetKey(KeyCode.Space) && reset == true) {
                jumpQueued = true;
            }
        }


            if (Input.GetKey(KeyCode.Space) && forceY != 0)
        {
            invertGravity -= Time.deltaTime;
            forceY += invertGravity * Time.deltaTime;
        }
       
	}
}