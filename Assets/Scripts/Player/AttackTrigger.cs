using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    private bool attacked = true;

    void OnTriggerEnter(Collider col)
    {

            print("hit " + col.gameObject.tag);


        if(col.CompareTag("Box")) {
			col.gameObject.GetComponent<BoxBehavior> ().Attacked()	;

        }

        if(col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<EnemyBehavior> ().Attacked();

        }
    }	
}