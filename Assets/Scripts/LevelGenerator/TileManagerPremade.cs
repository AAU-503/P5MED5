using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManagerPremade : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();
    int[] order = new int[52] {3, 1, 12, 6, 5, 0, 7, 12, 0, 9, 2, 3, 9, 2, 6, 8, 11, 5, 4, 7, 11, 10, 4, 2, 1, 8, 10, 11, 7, 9, 6, 5, 10, 8, 1, 3, 4, 0, 12, 0, 2, 4, 1, 11, 6, 8, 7, 5, 10, 9, 3, 12};

    int index = 0;
    int count = 0;

    public GameObject player;
    public GameObject spacing_prefab;
    public GameObject empty_prefab;
    private GameObject currentTile;
    
    public GameObject[] premadeChunks;
    private PrefabDescription prefabDescription;

    private int target;
    private int spacing;
    private int level;

    public static int currentLevel = 1;

    public int offset = 10;
    public int counter = 1;
    private int length;
    public string scene; 

    // Use this for initialization
    void Start() {
        premadeChunks = Resources.LoadAll<GameObject>("Prefabs/Premade");

        transform.position = new Vector3(player.transform.position.x - offset, transform.position.y, transform.position.z);
        currentLevel = 1;
        for (int i = 0; i < 40; i++) {
            currentTile = Instantiate(spacing_prefab, new Vector3(transform.position.x + i, transform.position.y, transform.position.z), Quaternion.identity);
            tiles.Add(currentTile);
        }

        length = currentTile.GetComponent<PrefabDescription>().length;
        spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
        target = 0;
        index = 0;
    }

    // Update is called once per frame
    void Update() {
        level = SetLevel();
        

        for (int i = 0; i < tiles.Count; i++) {
            if (tiles[i].transform.position.x + offset < player.transform.position.x) {

                if (counter >= target) {

                    switch (level) {

                        case 2:
                            SceneManager.LoadScene(scene);
                            break;
                        case 1:

                            count++;

                            if (index < order.Length) {

                                currentTile = Instantiate(premadeChunks[order[index]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                index++;

                            } else { 
                                currentTile = Instantiate(spacing_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                            }

                            break;
                            default:
                            Debug.Log("Level out of range");
                            break;
                    }

                    length = currentTile.GetComponent<PrefabDescription>().length;
                    spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
                    target = length + spacing;

                    tiles.Add(currentTile);

                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);
                    ResetCounter();

                } else if (counter >= length) {

                    tiles.Add(Instantiate(spacing_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity));
                    counter++;
                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);

                } else {
                    tiles.Add(Instantiate(empty_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity));
                    counter++;
                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);

                }
            }
        }
    }

    int SetLevel() {
        if (count >= 100)
        {
            return 2;
        } else {
            return 1;
        }
    }

    void ResetCounter() {
        counter = 1;
    }
}