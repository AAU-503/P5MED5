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

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		offsetPos = (transform.position.x - GameObject.FindWithTag("Player").GetComponent<Transform>().position.x) / 2;
		toPlayer = Vector3.Normalize(GameObject.FindWithTag("Player").GetComponent<Transform>().position - transform.position + new Vector3(offsetPos, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {


		transform.position += toPlayer  * speed * Time.deltaTime;
		
		if (Time.time > startTime + speed) {
			Destroy(gameObject);
		}
	}
}
