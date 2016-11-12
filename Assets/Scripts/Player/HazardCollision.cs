using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour {

    bool isLava = false;
    float setTime;

    public ParticleSystem fire;
    public ParticleSystem plasma; 

	void Start() {
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

                GetComponentInChildren<ParticleSystem>().Play();
                setTime = Time.time + 1.0f;

            } else if (hit.collider.gameObject.tag != "Lava") {
                isLava = false;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit)) {

			if (hit.collider.gameObject.tag == "Box" && hit.distance < 0.2f) {
                //print("box");
                hit.collider.gameObject.GetComponent<BoxBehavior>().OnBadCollision();
            }

            if (hit.collider.gameObject.tag == "Explosive" && hit.distance < 0.2f) {
                hit.collider.gameObject.GetComponent<ExplosiveBehavior>().OnBadCollision();
            }

            if (hit.collider.gameObject.tag == "Enemy" && hit.distance < 0.2f) {
                //right now it just prints enemy to the debug log, I don't what script I need to change stuff in for it to subtract score
                Debug.Log("enemy", gameObject);
            }
        }
    }


    void OnTriggerEnter(Collider other) { // Ability to pick up coins adding to score in ScoreManager and attacking boxes

        print(other.gameObject);

        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            ScoreManager.ChangeScore(ScoreManager.coinsScore);
        }

        if (other.gameObject.tag == "Enemy") {
            ScoreManager.ChangeScore(ScoreManager.enemyFailScore);
        }

        if (other.gameObject.tag == "Bullet") {
            plasma.Play();
            other.gameObject.GetComponent<BulletBehavior>().Destroy();
            ScoreManager.ChangeScore(ScoreManager.bulletScore);
        }
    }

}
