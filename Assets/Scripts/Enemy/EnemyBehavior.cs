using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    //speed can be adjusted to make them harder or easier
    public float speed = 1;

    public bool isDestroyed = false;

    void Start() {

    }

    // Update is called once per frame
    void Update () {
		 transform.Translate(new Vector3(-speed * Time.deltaTime, Mathf.Cos(Time.time * 10) / 100, 0));

        if (isDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void Attacked() {
		print("hello world");

		if (!isDestroyed) {
			isDestroyed = true;
		}
    }

    public void OnBadCollision() {


    }
}

