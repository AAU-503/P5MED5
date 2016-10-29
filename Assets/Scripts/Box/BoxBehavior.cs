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
        ScoreManager.ChangeScore(+ScoreManager.boxHitScore);
		if (!isDestroyed) {
			particle.Play();
			isDestroyed = true;
		}
    }

    public void OnBadCollision() {
<<<<<<< HEAD
		CameraController.setShake ();

=======
        ScoreManager.ChangeScore(+ScoreManager.boxFailScore);
>>>>>>> 4948abad4e11a7c8da32445387d2bb62d11c5b30
        if (!isDestroyed) {
            particle.Play();
            isDestroyed = true;
            }
    }
}
