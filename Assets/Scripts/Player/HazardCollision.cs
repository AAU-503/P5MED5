using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour {

    bool isLava = false;
    float setTime;

    public ParticleSystem fire;
    public ParticleSystem plasma;
    public AudioSource fireSound;
    public AudioSource plasmaSound;
    public GameObject toast;

    void Start() {
        AudioSource[] audios = GetComponents<AudioSource>();
        fireSound = audios[1];
        plasmaSound = audios[2];
        

    }

    void Update() {
        if (fire.IsAlive() && Time.time > setTime) {
            fire.Stop();
        }
    }
		

    void FixedUpdate() {

        RaycastHit hit;
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, dwn, out hit)) {
            if (hit.collider.gameObject.tag == "Lava" && hit.distance < 0.5f && isLava == false) {
                isLava = true;
				ScoreManager.ChangeScore (+ScoreManager.lavaScore);
                Instantiate(toast, hit.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.lavaScore);                
                
                fire.Play();
                fireSound.Play();

                setTime = Time.time + 1.0f;

            } else if (hit.collider.gameObject.tag != "Lava") {
                isLava = false;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit)) {

			if (hit.collider.gameObject.tag == "Box" && hit.distance < 0.2f) {
                //print("box");
                hit.collider.gameObject.GetComponent<BoxBehavior>().OnBadCollision();
                Instantiate(toast, hit.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.boxFailScore);
            }

            if (hit.collider.gameObject.tag == "Explosive" && hit.distance < 0.2f) {
                hit.collider.gameObject.GetComponent<ExplosiveBehavior>().OnBadCollision();
                Instantiate(toast, hit.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.explosiveHitScore);
            }

        }
    }


    void OnTriggerEnter(Collider other) { // Ability to pick up coins adding to score in ScoreManager and attacking boxes

        print(other.gameObject);

        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            ScoreManager.ChangeScore(ScoreManager.coinsScore);
            Instantiate(toast, other.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.coinsScore);

        }

        if (other.gameObject.tag == "Enemy") {
            ScoreManager.ChangeScore(ScoreManager.enemyFailScore);
            Instantiate(toast, other.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.enemyFailScore);
                
        }

        if (other.gameObject.tag == "Bullet") {
            plasma.Play();
            plasmaSound.Play();
            other.gameObject.GetComponent<BulletBehavior>().Destroy();
            ScoreManager.ChangeScore(ScoreManager.bulletScore);
            Instantiate(toast, other.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.bulletScore);                
        }
    }

}
