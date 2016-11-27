using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for communicating information about objects to <see cref="ChunkMemory"/> 
/// </summary>
public class TileChunkBridge : MonoBehaviour {

    public int state; // State of the object (+1, 0, -1).
    public GameObject obj; // Reference to the tile.  
    public Vector3 startPos; // The tile spawn position. 
    private ChunkMemory chunkMem;

	void Start () {
        // Send information regarding specific game objects to ChunkLog.cs.
        if (gameObject.tag == "Bullet") { // Bullets need to be logged at the drone start-position, therefore we treat them as a special case. 
            obj = gameObject.GetComponent<BulletBehavior>().drone;
            startPos = obj.GetComponent<TileChunkBridge>().startPos;
            chunkMem = obj.GetComponentInParent<ChunkMemory>();
        } else { // Otwerwise we get the local position of whatever object we are dealing with. 
            obj = gameObject;
            startPos = obj.transform.localPosition;
            chunkMem = obj.GetComponentInParent<ChunkMemory>();
        }

        chunkMem.UpdateMemory(obj);
    }

    /// <summary>
    /// Set the state of the object (+/- 1). The state is zero by default.
    /// </summary>
    public void SetState(int state) {
        switch (state) {
            case 1:
                this.state = 1;
                break;
            case -1:
                this.state = -1;
                break;
            default:
                print("State was not set.");
                break;
        }

        Sync();
    }

    /// <summary>
    /// Send information to <see cref="ChunkMemory"/> for later export.  
    /// </summary>
    public void Sync() {
        chunkMem.UpdateMemory(gameObject);
    }
}
