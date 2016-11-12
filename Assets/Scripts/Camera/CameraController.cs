using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Transform player;

	public static float speed = 0.1f;
	public static float xAdd;

	private static float setTime;
	private static bool isShaking;
    private static float startTime;

	static public float shakeDuration = 0.2f;
	public float shakeAmount = 0.2f;
	private Vector3 startPosition;
	private Vector3 shakeVector;


	void Start () {
        startTime = Time.time;
		startPosition = transform.position;
	}

	void Update (){
		
		xAdd += speed;

		transform.position = startPosition + Movement() + Shake();

	}

	Vector3 Movement() {
		return new Vector3 (xAdd, 0, 0);
	}

	Vector3 Shake ()
	{

		if (isShaking) { 

			if ((Time.time-startTime) < setTime) {
				
				shakeVector = ((Random.insideUnitSphere) * shakeAmount);
			} else {
				
				isShaking = false;
			}
		}

		return shakeVector;
	}

	public static void setShake ()
	{
		setTime = (Time.time - startTime) + shakeDuration;
		isShaking = true; 
	}
}
