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

                fire.Play();
                fireSound.Play();

                setTime = Time.time + 1.0f;

                print(hit.collider.transform.localPosition);

                // ChunkLogger
                Exporter.LogTile(hit.collider.gameObject, hit.collider.gameObject.transform.parent.gameObject, -1, hit.collider.transform.localPosition);

            } else if (hit.collider.gameObject.tag != "Lava") {
                isLava = false;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit)) {

			if (hit.collider.gameObject.tag == "Box" && hit.distance < 0.2f) {
                // ChunkLogger
                Exporter.LogTile(hit.collider.gameObject, hit.collider.gameObject.transform.parent.gameObject, -1, hit.collider.transform.localPosition);


                hit.collider.gameObject.GetComponent<BoxBehavior>().OnBadCollision();
                
            }

            if (hit.collider.gameObject.tag == "Explosive" && hit.distance < 0.2f) {
                // ChunkLogger
                Exporter.LogTile(hit.collider.gameObject, hit.collider.gameObject.transform.parent.gameObject, -1, hit.collider.transform.localPosition);


                hit.collider.gameObject.GetComponent<ExplosiveBehavior>().OnBadCollision();
            }

        }
    }


    void OnTriggerEnter(Collider collider) { 

        if (collider.gameObject.CompareTag("Coin")) {
            // ChunkLogger
            Exporter.LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, 1, collider.transform.localPosition);

            Destroy(collider.gameObject);
            ScoreManager.ChangeScore(ScoreManager.coinsScore);
        }

        if (collider.gameObject.tag == "Enemy") {
            // ChunkLogger
            Exporter.LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, -1, collider.gameObject.GetComponent<ChunkConnector>().startPos);

            ScoreManager.ChangeScore(ScoreManager.enemyFailScore);
            
        }

        if (collider.gameObject.tag == "Bullet") {
            // ChunkLogger
            collider.gameObject.GetComponent<BulletBehavior>().drone.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.GetComponent<BulletBehavior>().drone, -1, collider.gameObject.GetComponent<ChunkConnector>().startPos, "Bullet");

            plasma.Play();
            plasmaSound.Play();
            collider.gameObject.GetComponent<BulletBehavior>().Destroy();

            ScoreManager.ChangeScore(ScoreManager.bulletScore);
        }
    }

}
