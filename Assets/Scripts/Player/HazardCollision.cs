using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour {

	ScoreManager scoreManager;

    bool isLava = false;

	void Start() {
		scoreManager = GameObject.Find ("Main Camera").GetComponent<ScoreManager> ();
	}
		

    void FixedUpdate() {

        RaycastHit hit;
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, dwn, out hit)) {
            if (hit.collider.gameObject.tag == "Lava" && hit.distance < 0.5f && isLava == false) {
                isLava = true;
				scoreManager.ChangeScore (-10);

                print("lava");
            } else if (hit.collider.gameObject.tag != "Lava") {
                isLava = false;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit)) {

			if (hit.collider.gameObject.tag == "Box" && hit.distance < 0.2f) {
                //print("box");
                hit.collider.gameObject.GetComponent<BoxBehavior>().OnBadCollision();
            } else if (hit.collider.gameObject.tag != "Box") {
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit))
        {
			if (hit.collider.gameObject.tag == "Enemy" && hit.distance < 0.2f)
            {
                //right now it just prints enemy to the debug log, I don't what script I need to change stuff in for it to subtract score
                Debug.Log("enemy", gameObject);

            }
            else if (hit.collider.gameObject.tag != "Enemy")
            {
            }
        }
    }


    void OnCollisionEnter(Collision col) {
        //Debug.Log("OnCollisionEnter : this :" + name + "  :  other : " + col.gameObject.name);

        if (col.gameObject.tag == "Box") {
            //Debug.Log("BOX");
        }
    }

    // Update is called once per frame
    void Update() {


    }
}
