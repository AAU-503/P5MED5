using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    private bool attacked = true;

    void OnTriggerEnter(Collider col)
    {

        if(col.CompareTag("Box")) {
            col.gameObject.GetComponent<BoxBehavior> ().Attacked();
        }

        if(col.CompareTag("Explosive")) {
            col.gameObject.GetComponent<ExplosiveBehavior>().Attacked();
        }

        if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<EnemyBehavior> ().Attacked();

        }
    }	
}