using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    //speed can be adjusted to make them harder or easier
    public GameObject Bullet;

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

    float x_speed;
    float y_speed;


    public ParticleSystem explosion;
    public float offsetPos = 3.0f;

    void Start() {

        explosion = GetComponentInChildren<ParticleSystem>();
		
        startPos = transform.position;
        horizontalOffset = 3.0f;
        verticalOffset = 3.0f;

        x = Random.Range(0.0f, 1.0f);
        y = Random.Range(0.0f, 1.0f);
        x_speed = Random.Range(1.0f, 3.0f);
        y_speed = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update() {

        if (isDestroyed) {

            for (int i = 0; i < GetComponentsInChildren<Renderer>()[1].GetComponentsInChildren<Renderer>().Length; i++) {
                GetComponentsInChildren<Renderer>()[1].GetComponentsInChildren<Renderer>()[i].enabled = false;
            }

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<AudioSource>().Play();


            if (!explosion.IsAlive()) {
                Destroy(gameObject);
            }
        } else {
            Movements();
			}
    }

    public void Movements() {
		if(transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos){
			transform.LookAt(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
		}
        
        if (y < 1.0f && isMovingUp) {
            y += (1.05f - y) * y_speed * Time.deltaTime;
        } else if (y >= 1.0f) {
            isMovingUp = !isMovingUp;

            if(transform.position.x > GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + offsetPos) {
                if (GameObject.FindGameObjectsWithTag("Bullet").Length < 2)
                shoot();
            }
        }

        if (y > 0.0f && !isMovingUp) {
            y -= (y - -0.05f) * y_speed * Time.deltaTime;
        } else if (y <= 0.0f) {
            isMovingUp = !isMovingUp;
        }

        if (x < 1.0f && isMovingX) {
            x += (1.05f - x) * x_speed * Time.deltaTime;
        } else if (x >= 1.0f) {
            isMovingX = !isMovingX;
        }

        if (x > 0.0f && !isMovingX) {
            x -= (x - -0.05f) * x_speed * Time.deltaTime;
        } else if (x <= 0.0f) {
            isMovingX = !isMovingX;
        }

        transform.position = new Vector3(startPos.x + Mathf.Lerp(0, horizontalOffset, x), startPos.y + Mathf.Lerp(0, verticalOffset, y), startPos.z);
    }

    public void shoot(){
        Instantiate(Bullet, transform.position, Quaternion.identity);
    }

    public void Attacked() {

        if (!isDestroyed) {
            GetComponent<AudioSource>().Play();
            explosion.Play();
            CameraController.setShake();
            isDestroyed = true;
        }

    }

    public void OnBadCollision() {

    }
}

