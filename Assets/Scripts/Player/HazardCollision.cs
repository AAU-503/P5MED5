using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour
{

    bool isLava = false;
    float setTime;

    public ParticleSystem fire;
    public ParticleSystem plasmaParticles;
    public AudioSource fireSound;
    public AudioSource plasmaSound;


    public AudioSource robotSound;



    public GameObject toast;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        fireSound = audios[1];
        plasmaSound = audios[2];
        robotSound = audios[3];
    }

    void Update()
    {
        if (fire.IsAlive() && Time.time > setTime)
        {
            fire.Stop();
        }
    }


    void FixedUpdate()
    {

        RaycastHit hit;
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, dwn, out hit))
        {
            if (hit.collider.gameObject.tag == "Lava" && hit.distance < 0.5f && isLava == false)
            {
                isLava = true;

                ScoreManager.ChangeScore(+ScoreManager.lavaScore);
                Instantiate(toast, hit.transform.position + new Vector3(3.0f, 1.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.lavaScore);

                fire.Play();
                fireSound.Play();

                setTime = Time.time + 1.0f;


                ScoreManager.ChangeScore(+ScoreManager.lavaScore);
                hit.collider.GetComponent<TileChunkBridge>().SetState(-1);


            } else if (hit.collider.gameObject.tag != "Lava") {
                isLava = false;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit))
        {

            if (hit.collider.gameObject.tag == "Box" && hit.distance < 0.2f) {

                hit.collider.GetComponent<TileChunkBridge>().SetState(-1);

                hit.collider.gameObject.GetComponent<BoxBehavior>().OnBadCollision();
                Instantiate(toast, hit.transform.position + new Vector3(3.0f, 1.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.boxFailScore);
            }

            if (hit.collider.gameObject.tag == "Explosive" && hit.distance < 0.2f) {

                hit.collider.GetComponent<TileChunkBridge>().SetState(-1);

                hit.collider.gameObject.GetComponent<ExplosiveBehavior>().OnBadCollision();
                Instantiate(toast, hit.transform.position + new Vector3(3.0f, 1.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.explosiveHitScore);
            }
        }
    }



    void OnTriggerEnter(Collider collider) { // Ability to pick up coins adding to score in ScoreManager and attacking boxes

        if (collider.gameObject.CompareTag("Coin")) {
            collider.GetComponent<TileChunkBridge>().SetState(1);
            Destroy(collider.gameObject);
            ScoreManager.ChangeScore(ScoreManager.coinsScore);
            Instantiate(toast, collider.transform.position + new Vector3(3.0f, 1.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.coinsScore);

        }

 	if (collider.gameObject.tag == "Enemy")
        {
            robotSound.Play();
            ScoreManager.ChangeScore(ScoreManager.enemyFailScore);
            Instantiate(toast, collider.transform.position + new Vector3(2.0f, 0.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.enemyFailScore);

        }

        if (collider.gameObject.tag == "Bullet") {
            // ChunkLogger
            //collider.gameObject.GetComponent<BulletBehavior>().drone.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.GetComponent<BulletBehavior>().drone, -1, collider.gameObject.GetComponent<BulletBehavior>().drone.GetComponentInParent<PrefabDescription>().instance, collider.gameObject.GetComponent<ChunkConnector>().startPos, "Bullet");

            plasmaParticles.Play();
            plasmaSound.Play();

            collider.GetComponent<TileChunkBridge>().SetState(-1);
            collider.gameObject.GetComponent<BulletBehavior>().Destroy();

            ScoreManager.ChangeScore(ScoreManager.bulletScore);
            Instantiate(toast, collider.transform.position + new Vector3(3.0f, 1.0f, -2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.bulletScore);
        }
    }
}
