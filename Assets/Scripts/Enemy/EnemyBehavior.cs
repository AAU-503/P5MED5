﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    //speed can be adjusted to make them harder or easier

    public ParticleSystem explosion;
    public GameObject Bullet;
    public Vector3 startPos;

    float x;
    float y;

    float x_speed;
    float y_speed;

    public float offsetPos = 5.0f;

    private bool shot = false;
    public bool isMovingX = false;
    public bool isMovingY = true;
    private bool isDestroyed = false;

    private float horizontalOffset;
    private float verticalOffset;

    public AudioSource explosionSound;

    private float startTime;

    void Start() {

        explosion = GetComponentInChildren<ParticleSystem>();

        startPos = transform.position;
        horizontalOffset = 3.0f;
        verticalOffset = 3.0f;

        y = Random.Range(0.0f, 1.0f);
        x_speed = Random.Range(1.0f, 3.0f);
        y_speed = Random.Range(1.0f, 3.0f);

        AudioSource[] audios = GetComponents<AudioSource>();
        explosionSound = audios[0];

    //FloatingTextController.Initialize();
    startTime = Time.time;
        angle = Random.Range(0, 360);

    }

    float angle;
    float speed = 60;
    float toDegrees = Mathf.PI/180;
    float maxUpAndDown = 1.5f;

    // Update is called once per frame
    void Update() {

        angle += speed * Time.deltaTime;

        if (angle > 360) {
            angle -= 360;
        }

        transform.position = startPos + new Vector3 (0, maxUpAndDown * Mathf.Sin(angle * toDegrees) + maxUpAndDown);

        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos && GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + 10 > transform.position.x) {
            if (GameObject.FindGameObjectsWithTag("Bullet").Length < 2) {
                if (Mathf.Sin(angle * toDegrees) > 0.95) {
                    isMovingY = false;
                    shoot();
                } else if (Mathf.Sin(angle * toDegrees) < -0.95) {
                    isMovingY = true;
                    shoot();
                }
            }
        }

        if (isDestroyed) {

            for (int i = 0; i < GetComponentsInChildren<Renderer>()[1].GetComponentsInChildren<Renderer>().Length; i++) {
                GetComponentsInChildren<Renderer>()[1].GetComponentsInChildren<Renderer>()[i].enabled = false;

            }

            if (!explosion.IsAlive()) {
                Destroy(gameObject);
                FloatingTextController.CreatePopupText(ScoreManager.enemyKillScore.ToString(), transform);

            }

        } else {
            //Movements();
        }
    }

    public void Movements() {
        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos) {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
        }

        if (y < 1.0f && isMovingY) {
            y += (1.05f - y) * y_speed * Time.deltaTime;
        } else if (y >= 1.0f & isMovingY) {
            isMovingY = !isMovingY;

                if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos && GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + 10 > transform.position.x) {
                    if (GameObject.FindGameObjectsWithTag("Bullet").Length < 2)
                        shoot();
                }
        }

        if (y > 0.0f && !isMovingY) {
            y -= (y + 0.05f) * y_speed * Time.deltaTime;
        } else if (y <= 0.0f) {
            isMovingY = !isMovingY;

            if (transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos && GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + 10 > transform.position.x) {
                if (GameObject.FindGameObjectsWithTag("Bullet").Length < 2)
                    shoot();
            }
        }


        //if (x < 1.0f && isMovingX) {
        //    x += (1.05f - x) * x_speed * Time.deltaTime;
        //} else if (x >= 1.0f) {
        //    isMovingX = !isMovingX;

        //}

        //if (x > 0.0f && !isMovingX) {
        //    x -= (x - -0.05f) * x_speed * Time.deltaTime;
        //} else if (x <= 0.0f) {
        //    isMovingX = !isMovingX;
        //}

        //transform.position = new Vector3(startPos.x + Mathf.Lerp(0, horizontalOffset, x), startPos.y + Mathf.Lerp(0, verticalOffset, y), startPos.z);


    }


    public void shoot(){
        if (!shot) {
            Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<BulletBehavior>().Init(gameObject);
            shot = true;
        }
    }

    public void Attacked() {

        if (!isDestroyed) {
            GetComponent<BoxCollider>().enabled = false;
            explosion.Play();
            explosionSound.Play();
            CameraController.setShake();
            isDestroyed = true;
        }
    }


 
}

