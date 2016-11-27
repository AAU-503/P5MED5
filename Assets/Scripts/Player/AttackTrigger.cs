using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    private bool attacked = true;

    void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("Box")) {
            collider.gameObject.GetComponent<BoxBehavior> ().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(1);
            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, 1, collider.transform.localPosition, "Attacked");

        }

        if (collider.CompareTag("Explosive")) {
            collider.gameObject.GetComponent<ExplosiveBehavior>().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(-1);

            // ChunkLogger
            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, -1, collider.transform.localPosition, "Attacked");
        }

        if (collider.gameObject.tag == "Enemy") {
            // ChunkLogger
            ScoreManager.ChangeScore(ScoreManager.enemyKillScore);
			collider.gameObject.GetComponent<EnemyBehavior>().Attacked();
            collider.GetComponent<TileChunkBridge>().SetState(1);


            //collider.GetComponentInParent<ChunkLogger>().LogTile(collider.gameObject, collider.gameObject.transform.parent.gameObject, GetComponentInParent<PrefabDescription>().instance, 1, collider.GetComponent<ChunkConnector>().startPos, "Attacked");


        }
    }	
}