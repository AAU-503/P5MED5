using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform player;

    public float speed = 0.1f; 
	public Vector3 startPos;
	public float xPos;
	public float shakeDuration;

	void Start () {
		startPos = transform.position;
	}
	
	void Update () {
		xPos += speed;

        //transform.Translate(speed, 0, 0, Space.World);

		Shake (0f);

	}

	void Shake(float shakeAmount) {

		transform.position = startPos + ((Random.insideUnitSphere) * shakeAmount) + new Vector3(xPos, 0, 0);

	}
}
