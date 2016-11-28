using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public GameObject toast;


    private bool attacked = true;

    void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("Box")) {
            collider.gameObject.GetComponent<BoxBehavior> ().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(1);
            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, 1, collider.transform.localPosition, "Attacked");
	    Instantiate(toast, collider.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.boxHitScore);
        }

        if (collider.CompareTag("Explosive")) {
            collider.gameObject.GetComponent<ExplosiveBehavior>().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(-1);
            Instantiate(toast, collider.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.explosiveHitScore);
            // ChunkLogger
            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, -1, collider.transform.localPosition, "Attacked");
        }

        if (collider.gameObject.tag == "Enemy") {
            // ChunkLogger
            ScoreManager.ChangeScore(ScoreManager.enemyKillScore);
	    collider.gameObject.GetComponent<EnemyBehavior>().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(1);


            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, 1, collider.GetComponent<ChunkConnector>().startPos, "Attacked");

            Instantiate(toast, collider.transform.position + new Vector3(3.0f,2.0f,-2.0f), Quaternion.identity).GetComponentInChildren<ScoreText>().setText(ScoreManager.enemyKillScore);

        }
    }	
}