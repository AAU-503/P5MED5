using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehavior : MonoBehaviour {

    public AudioClip boxBreak;

    private Renderer rend;
    private ParticleSystem particle;

    public bool isDestroyed = false;

    void Start() {
        rend = GetComponent<Renderer>();
        particle = GetComponentInChildren<ParticleSystem>();

        rend.enabled = true;
        
    }

    // Update is called once per frame
    void Update () {
        if (isDestroyed)
        {
            rend.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if (!particle.IsAlive())
            Destroy(gameObject);
        }
    }

    public void Attacked() {
        CameraController.setShake(1.0f);
        CameraController.setSlowmotion();

        ScoreManager.ChangeScore(+ScoreManager.explosiveHitScore);
		if (!isDestroyed) {
            GetComponent<AudioSource>().Play();
			particle.Play();
			isDestroyed = true;
            
		}
    }

    public void OnBadCollision() {
        CameraController.setShake(1.0f);
        CameraController.setSlowmotion();

        ScoreManager.ChangeScore(+ScoreManager.explosiveFailScore);
        if (!isDestroyed) {
            GetComponent<AudioSource>().Play();
            particle.Play();
            isDestroyed = true;
            }
    }
}
