using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    //speed can be adjusted to make them harder or easier
    public float speed = 1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(-speed * Time.deltaTime, Mathf.Cos(Time.time * 10) / 100, 0));
	}
}
