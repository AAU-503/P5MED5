using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager_Old : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();

    public GameObject[] level1Chunks;
    public GameObject[] level2Chunks;
    public GameObject[] level3Chunks;

    public GameObject currentTile;
    public GameObject groundTile;

    private PrefabDescription prefabDescription;

    private int target;
    private int spacing;
    private int level;
    private int x;
    private int px;
    private Vector3 startPos;

    void Start() {

        //load to array
        level1Chunks = Resources.LoadAll<GameObject>("Prefabs/Level1");
        level2Chunks = Resources.LoadAll<GameObject>("Prefabs/Level2");
        level3Chunks = Resources.LoadAll<GameObject>("Prefabs/Level3");

        x = (int)(transform.position.x);
        px = (int)transform.position.x - 1;

        startPos = transform.position;

    }


    // Use this for initialization
    void Update() {
        //Debug.Log("x:" + x + "px:" + px);


        if (ScoreManager.playerScore > 500) {
            level = 3;
        } else if (ScoreManager.playerScore > 200) {
            level = 2;
        } else {
            level = 1;
        }

        if (px != x) {
            //Debug.Log(transform.position.x);

            Spawn();

            px = (int)transform.position.x;
        }
        x = (int)transform.position.x;

    }

    void Spawn() {

        if (currentTile == null) {
            //when player in level 1

            if (level == 1) {
                //generates chunk from level1 folder
                currentTile = Instantiate(level1Chunks[Random.Range(0, level1Chunks.Length)], startPos, Quaternion.identity);
            } else if (level == 2) {
                //generates chunk from level2 folder
                currentTile = Instantiate(level2Chunks[Random.Range(0, level2Chunks.Length)], startPos, Quaternion.identity);
            } else if (level == 3) {
                //generates chunk from level3 folder
                currentTile = Instantiate(level3Chunks[Random.Range(0, level3Chunks.Length)], startPos, Quaternion.identity);
            }

            prefabDescription = currentTile.GetComponent<PrefabDescription>();

            //adds above instantiated object
            tiles.Add(currentTile);
            //uses random spaceing in the range beween object spaceingMax and spaceing values
            spacing = Random.Range(prefabDescription.spacingMin, prefabDescription.spacingMax);
            //calculates the tiles possition on prefab location
            target = (int)currentTile.GetComponent<Transform>().position.x + prefabDescription.length;

        }

        if (x >= currentTile.GetComponent<Transform>().position.x + prefabDescription.length) {
            print("ground");
        }

            // if current tile x position is in the range of spacing, spawn ground tile
            if (x >= (int)currentTile.GetComponent<Transform>().position.x + prefabDescription.length && x < target + spacing) {
            
            //spawn ground tiles
            tiles.Add(Instantiate(groundTile, new Vector3(tiles[tiles.Count - 1].transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity));

        } else if (x == target + spacing) {
            // if current tile x position is after spacing position spawn prefab chunk
            tiles.Add(currentTile);
            /*
            calculating level range (so that for level 1 only level 1 chunks would be spawn,
            but for levels 2 and 3 we could get chenks from lower levels as well)
            */
            int chosenLevel = Random.Range(1, level + 1);

            switch (chosenLevel) {
                case 3:
                    //loads radom object from level 3 folder
                    currentTile = Instantiate(level3Chunks[Random.Range(0, level3Chunks.Length)], new Vector3(tiles[tiles.Count - 1].transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
                    break;

                case 2:
                    //loads radom object from level 2 folder
                    currentTile = Instantiate(level2Chunks[Random.Range(0, level2Chunks.Length)], new Vector3(tiles[tiles.Count - 1].transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
                    break;

                case 1:
                    //loads radom object from level 1 folder
                    currentTile = Instantiate(level1Chunks[Random.Range(0, level1Chunks.Length)], new Vector3(tiles[tiles.Count - 1].transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
                    break;

                default:
                    Debug.Log("Level out of range");
                    break;

            }

            prefabDescription = currentTile.GetComponent<PrefabDescription>();

            spacing = Random.Range(prefabDescription.spacingMin, prefabDescription.spacingMax);

            target = (int)currentTile.GetComponent<Transform>().position.x + prefabDescription.length;
        }
        if (tiles.Count > 40) {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        }

    }

}
