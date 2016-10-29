using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour {

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    public float jumpSpeed = 20.0f;
    public float gravity = 2.0f;

    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        if (controller.isGrounded) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpSpeed;
            }

        } else {
            moveDirection.y -= gravity;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }
}
