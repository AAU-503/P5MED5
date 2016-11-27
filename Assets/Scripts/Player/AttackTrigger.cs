using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public GameObject toast;


    private bool attacked = true;

    void OnTriggerEnter(Collider col)
    {

        if(col.CompareTag("Box")) {
            col.gameObject.GetComponent<BoxBehavior> ().Attacked();
            Instantiate(toast, col.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.boxHitScore);
        }

        if(col.CompareTag("Explosive")) {
            col.gameObject.GetComponent<ExplosiveBehavior>().Attacked();
            Instantiate(toast, col.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.explosiveHitScore);
        }

        if (col.gameObject.tag == "Enemy") {
            ScoreManager.ChangeScore(ScoreManager.enemyKillScore);
			col.gameObject.GetComponent<EnemyBehavior> ().Attacked();
            Instantiate(toast, col.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.enemyKillScore);

        }
    }	
}