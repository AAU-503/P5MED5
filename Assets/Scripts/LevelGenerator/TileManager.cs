﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();

    public GameObject player;
    public GameObject spacing_prefab;
    public GameObject empty_prefab;
    private GameObject currentTile;

    public GameObject[] level1Chunks;
    public GameObject[] level2Chunks;
    public GameObject[] level3Chunks;

    private PrefabDescription prefabDescription;

    private int target;
    private int spacing;
    private int level;

    public int offset = 10;
    public int counter = 1;
    private int length;




    // Use this for initialization
    void Start() {
        level1Chunks = Resources.LoadAll<GameObject>("Prefabs/Level1");
        level2Chunks = Resources.LoadAll<GameObject>("Prefabs/Level2");
        level3Chunks = Resources.LoadAll<GameObject>("Prefabs/Level3");

        transform.position = new Vector3(player.transform.position.x - offset, transform.position.y, transform.position.z);

        for (int i = 0; i < 40; i++) {
            currentTile = Instantiate(spacing_prefab, new Vector3(transform.position.x + i, transform.position.y, transform.position.z), Quaternion.identity);
            tiles.Add(currentTile);
        }

        length = currentTile.GetComponent<PrefabDescription>().length;
        spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
        target = 0;
    }

    // Update is called once per frame
    void Update() {
        level = SetLevel();

        for (int i = 0; i < tiles.Count; i++) {
            if (tiles[i].transform.position.x + offset < player.transform.position.x && !tiles[i].GetComponent<LogicScript>().check) {

                if (counter >= target) {

                    switch (level) {
                        case 3:
                            currentTile = Instantiate(level3Chunks[Random.Range(0, level3Chunks.Length)], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                            break;
                        case 2:
                            currentTile = Instantiate(level2Chunks[Random.Range(0, level2Chunks.Length)], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                            break;
                        case 1:
                            currentTile = Instantiate(level1Chunks[Random.Range(0, level1Chunks.Length)], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                            break;
                            Debug.Log("Level out of range");
                            break;
                    }

                    length = currentTile.GetComponent<PrefabDescription>().length;
                    spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
                    target = length + spacing;

                    print("Target: " + target + " length: " + length + " spacing: " + spacing);

                    tiles.Add(currentTile);

                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);
                    ResetCounter();

                    print(counter);


                } else if (counter > length - 1) {

                    tiles.Add(Instantiate(spacing_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity));
                    counter++;
                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);

                    print(counter);

                } else {

                    tiles.Add(Instantiate(empty_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity));
                    counter++;
                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);

                    print(counter);

                }
            }
        }
    }

    int SetLevel() {
        if (ScoreManager.playerScore > 500) {
            return 3;
        } else if (ScoreManager.playerScore > 200) {
            return 2;
        } else {
            return 1;
        }
    }

    void ResetCounter() {
        counter = 1;
    }
}