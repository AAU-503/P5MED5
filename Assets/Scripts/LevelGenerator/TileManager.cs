using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

    public GameObject checker;
    public GameObject[] tile;

    public List<GameObject> tiles = new List<GameObject>();

	private int currentLevel;
    private int x;
    private int px;
	private int lastTileWidth;
	private int prefabWidth;
	private int safetyRange = 7;
	private int nextSafety;

	//private bool inLastTile = 0;

    void Start() {
        x = (int)transform.position.x;
        px = (int)transform.position.x - 1;
		currentLevel = 1;
		lastTileWidth = 1;

    }//Start


    void Update() {

		/*if (px != x){
			inLastTile = 1;
		}*/

		if (px != x && x >= lastTileWidth) { //if px == x-1, and position has changed more than or equal to prefab length
            //Debug.Log("x:" + x + "px:" + px);
            //Debug.Log(transform.position.x);
            Spawn();

            px = (int)transform.position.x;
        }
        x = (int)transform.position.x;

    }//Update

    void Spawn() {
		
		if (nextSafety <= 0) {
			if (currentLevel == 1) { //Elements 1 through 3 are level 1
				int lvl1 = Random.Range (1, 4);
				tiles.Add (Instantiate (tile [lvl1], transform.position, Quaternion.identity));
				prefabWidth = 1;
			}
			if (currentLevel == 2) { //Elements 4 through 12 are level 2
				int lvl2 = Random.Range (4, 10);
				tiles.Add (Instantiate (tile [lvl2], transform.position, Quaternion.identity));
				prefabWidth = 2;
			}
			if (currentLevel == 3) { //Elements 13 through 19 are level 3
				int lvl3 = Random.Range (13, 8);
				tiles.Add (Instantiate (tile [lvl3], transform.position, Quaternion.identity));
				prefabWidth = 3;
			}
			nextSafety += safetyRange;
		}//if nextSafety
		else { //else add safe space (Tile_Ground)
			tiles.Add (Instantiate (tile [0], transform.position, Quaternion.identity));
			prefabWidth = 1;
			nextSafety--;
		}
		lastTileWidth = x + prefabWidth - 1;

        if (tiles.Count > 50) {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        }
        
    }//Spawn
}
