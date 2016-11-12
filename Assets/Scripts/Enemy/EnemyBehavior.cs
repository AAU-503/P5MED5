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
    private bool isMovingUp;
    public bool isDestroyed = false;

    void Start() {
        FloatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update() {
        if (isMovingX) {
            transform.Translate(new Vector3(-speedX * Time.deltaTime, Mathf.Cos(Time.time * 10) / 100, 0));
        }
        if (isMovingY)
        {
            if (isMovingUp)
            {
                transform.Translate(new Vector3(0, +speedY * Time.deltaTime, 0));
            }
            if (!isMovingUp)
            {
                transform.Translate(new Vector3(0, -speedY * Time.deltaTime, 0));
            }
            if (!isMovingX && !isMovingY)
            {
                transform.Translate(new Vector3(0, Mathf.Cos(Time.time * 10) / 100, 0));
            }
        }
        if (isDestroyed)
        {
            GetComponent<AudioSource>().Play(); 
            Destroy(this.gameObject);
            FloatingTextController.CreatePopupText(ScoreManager.enemyKillScore.ToString(), transform);
        }
        if (isMovingUp && transform.position.y >= flyHeight)
        {
            isMovingUp = false;
        }else if (!isMovingUp && transform.position.y <= revertHeight)
        {
            isMovingUp = true;
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

