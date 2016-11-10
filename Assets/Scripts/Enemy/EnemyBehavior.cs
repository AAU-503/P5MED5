using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    //speed can be adjusted to make them harder or easier
    public float speedY = 2;
    public float speedX = 1;
    public float flyHeight = 2;
    public float revertHeight = -1.5f;

    public bool isMovingX;
    public bool isMovingY;
    private bool isMovingUp = true;
    public bool isDestroyed = false;

    public Vector3 startPos;
    public float horizontalOffset;
    public float verticalOffset;

    float x;
    float y;


    void Start() {
        startPos = transform.position;
        horizontalOffset = 3.0f;
        verticalOffset = 3.0f;

        y = Random.Range(0.0f, 1.0f);        
            print(y);

    }

    // Update is called once per frame
    void Update() {


        if (y < 1.0f && isMovingUp) {
            y += (1.05f - y) * 2.5f * Time.deltaTime;
        } else if (y >= 1.0f) {
            isMovingUp = !isMovingUp;
        }

        if (y > 0.0f && !isMovingUp) {
            y -= (y - -0.05f) * 3f * Time.deltaTime;
        } else if (y <= 0.0f) {
            isMovingUp = !isMovingUp;
        }

        if (x < 1.0f && isMovingX) {
            x += (1.05f - x) * 2.5f * Time.deltaTime;
        } else if (x >= 1.0f) {
            isMovingX = !isMovingX;
        }

        if (x > 0.0f && !isMovingX) {
            x -= (x - -0.05f) * 3f * Time.deltaTime;
        } else if (x <= 0.0f) {
            isMovingX = !isMovingX;
        }

        transform.position = new Vector3(startPos.x + Mathf.Lerp(0, horizontalOffset, x), startPos.y + Mathf.Lerp(0, verticalOffset, y), startPos.z);

        if (isDestroyed)
        {
            GetComponent<AudioSource>().Play(); 
            Destroy(this.gameObject);
        }

    }

    public void Attacked() {
		print("hello world");

		if (!isDestroyed) {
			isDestroyed = true;
		}
    }

    public void OnBadCollision() {


    }
}

