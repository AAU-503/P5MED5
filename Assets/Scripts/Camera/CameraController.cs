using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Transform player;

	public static float speed = 8f;
	public static float xAdd;

	private static float setTime;
	private static bool isShaking;

	static public float shakeDuration = 0.2f;
	public float shakeAmount = 0.2f;
	private Vector3 startPosition;
	private Vector3 shakeVector;

    bool realtime = true;
    int state = 1;
    float sTime;


	void Start () {
		startPosition = transform.position;
        Time.timeScale = 1f;

    }

    void Update (){
		
		xAdd += speed * Time.deltaTime;

		transform.position = startPosition + Movement() + Shake();

            StartCoroutine("SlowTime");

    }

    IEnumerator SlowTime() {
        if (Input.GetKeyDown(KeyCode.E)) {
            realtime = false;
            state = 1; 
        }

        switch (state) {
            case 1:

                if (realtime == false && Time.timeScale > 0.2f) {
                    Time.timeScale -= (3f) * Time.deltaTime;
                } else {
                    sTime = Time.unscaledTime + 3;
                    state = 2;
                }

                break;
             
            case 2:

                if (Time.unscaledTime > sTime) {
                    if (realtime == false && Time.timeScale < 1.0f) {
                        Time.timeScale += (1 - Time.timeScale) * Time.deltaTime;
                    } else {
                        state = 0;
                    }
                }

                break;
    }

        yield return null;
    }



    Vector3 Movement() {
		return new Vector3 (xAdd, 0, 0);
	}

	Vector3 Shake ()
	{

		if (isShaking) { 

			if (Time.time < setTime) {
				
				shakeVector = ((Random.insideUnitSphere) * shakeAmount);
			} else {
				
				isShaking = false;
			}
		}

		return shakeVector;
	}

	public static void setShake ()
	{
		setTime = Time.time + shakeDuration;
		isShaking = true; 
	}
}
