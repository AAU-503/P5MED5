using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour {

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
        if (isDestroyed) {
            rend.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;

        }
    }

    public void Attacked() {
        ScoreManager.ChangeScore(+ScoreManager.boxHitScore);
		if (!isDestroyed) {
            GetComponent<AudioSource>().Play();
			particle.Play();
			isDestroyed = true;
            
		}
    }

    public void OnBadCollision() {
		CameraController.setShake ();

        ScoreManager.ChangeScore(+ScoreManager.boxFailScore);
        if (!isDestroyed) {
            GetComponent<AudioSource>().Play();
            particle.Play();
            isDestroyed = true;
            }
    }
}
