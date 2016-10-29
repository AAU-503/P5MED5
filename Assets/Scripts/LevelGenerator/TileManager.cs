using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

    public GameObject checker;
    public GameObject[] tile;

    public List<GameObject> tiles = new List<GameObject>();

    private int x;
    private int px;

    void Start() {
        x = (int)transform.position.x;
        px = (int)transform.position.x - 1;
    }


    // Use this for initialization
    void Update() {

        if (px != x) {
            //Debug.Log("x:" + x + "px:" + px);
            //Debug.Log(transform.position.x);

            Spawn();

            px = (int)transform.position.x;
        }
        x = (int)transform.position.x;
    }

    void Spawn() {

        tiles.Add(Instantiate(tile[0], transform.position, Quaternion.identity));

        if (tiles.Count > 50) {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        }
        
    }
}
