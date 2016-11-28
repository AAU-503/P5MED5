using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
	public Vector3 toPlayer;
	public float speed = 7.0f;
	public float startTime; 
	public float lifeTime = 2.0f;
	public float normOffset;
	public float offsetPos;
    private bool isDestroyed;
    public ParticleSystem particle;
    public GameObject drone;
    private Renderer[] renderes;

	// Use this for initialization
	public void Init (GameObject drone) {
        this.drone = drone;

		startTime = Time.time;
		offsetPos = (transform.position.x - GameObject.FindWithTag("Player").GetComponent<Transform>().position.x) / 2;
		toPlayer = Vector3.Normalize(GameObject.FindWithTag("Player").GetComponent<Transform>().position - transform.position + new Vector3(offsetPos, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time > startTime + speed) {
			Destroy(gameObject);
		}

        if (isDestroyed) {
            renderes = GetComponentsInChildren<Renderer>();

            foreach (Renderer element in renderes) {
                element.enabled = false;
            }
            
        } else {
            transform.position += toPlayer * speed * Time.deltaTime;
        }
    }

    public void Destroy() {

        isDestroyed = true;

    }
}
