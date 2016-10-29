using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour {

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
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if (!particle.IsAlive())
            Destroy(gameObject);
        }
    }

    public void Attacked() {
		if (!isDestroyed) {
			particle.Play();
			isDestroyed = true;
		}
    }

    public void OnBadCollision() {
		CameraController.setShake ();

        if (!isDestroyed) {
            particle.Play();
            isDestroyed = true;
            }
    }
}
