using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkConnector : MonoBehaviour {

    public ChunkLogger chunkLogger;
    public Vector3 startPos;


	// Use this for initialization
	void Start () {

        if (gameObject.tag == "Bullet") {
            startPos = gameObject.GetComponent<BulletBehavior>().drone.GetComponent<ChunkConnector>().startPos;
            chunkLogger = gameObject.GetComponent<BulletBehavior>().drone.GetComponentInParent<ChunkLogger>();
            chunkLogger.Connect(gameObject.GetComponent<BulletBehavior>().drone);

        } else {
            startPos = transform.localPosition;
            chunkLogger = GetComponentInParent<ChunkLogger>();
            chunkLogger.Connect(gameObject);
        }
    }
}
