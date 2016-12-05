using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour {

    public List<GameObject> tiles = new List<GameObject>();

    public GameObject player;
    public GameObject spacing_prefab;
    public GameObject empty_prefab;
    private GameObject currentTile;
   
    public GameObject[] level1Chunks;
    public GameObject[] level2Chunks;
    public GameObject[] level3Chunks;

    public List<int> lvl1;
    public List<int> lvl2;
    public List<int> lvl3;

    private PrefabDescription prefabDescription;

    public static int currentLevel;
    public static int lvl1Length = 45;
    public static int lvl2Length = 70;
    public static int lvl3Length = 60;


    private int target;
    private int spacing;
    private int level;
    private int length;
    private int endCounter;

    private float startTime;

    public int offset = 10;
    public int counter = 1;

    public float level2Time = 45;
    public float level3Time = 135;

    public string scene; 

    // Use this for initialization
    void Start() {
        level1Chunks = Resources.LoadAll<GameObject>("Prefabs/Level1");
        level2Chunks = Resources.LoadAll<GameObject>("Prefabs/Level2");
        level3Chunks = Resources.LoadAll<GameObject>("Prefabs/Level3");

        transform.position = new Vector3(player.transform.position.x - offset, transform.position.y, transform.position.z);
        currentLevel = 1;
        for (int i = 0; i < 40; i++) {
            currentTile = Instantiate(spacing_prefab, new Vector3(transform.position.x + i, transform.position.y, transform.position.z), Quaternion.identity);
            tiles.Add(currentTile);
        }

        length = currentTile.GetComponent<PrefabDescription>().length;
        spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
        target = 0;
        startTime = Time.time;

        // Put tiles into lists for level 1 and shuffle
        for (int i = 0; i < lvl1Length; i++) {
            int id = i % level1Chunks.Length;
            lvl1.Add(id);
            Randomizer.Shuffle(lvl1);
        }

        // Put tiles into lists for level 2 and shuffle
        for (int i = 0; i < lvl2Length; i++) {
            int id = i % level2Chunks.Length;
            lvl2.Add(id);
            Randomizer.Shuffle(lvl2);
        }

        // Put tiles into lists for level 3 and shuffle
        for (int i = 0; i < lvl3Length; i++) {
            int id = i % level3Chunks.Length;
            lvl3.Add(id);
            Randomizer.Shuffle(lvl3);
        }

    }

    // Update is called once per frame
    void Update() {
        level = SetLevel();

        for (int i = 0; i < tiles.Count; i++) {
            if (tiles[i].transform.position.x + offset < player.transform.position.x) {

                if (counter >= target) {

                    switch (level) {

                        case 5:
                            SceneManager.LoadScene(scene);
                            break;
                        case 4:
                            currentTile = Instantiate(spacing_prefab, new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                            endCounter++;   
                            break;
                        case 3:

                            float j = Random.Range(0f, 1f);
                            if (j > 0.30f || lvl2.Count == 0) {

                                if (lvl3.Count != 0) {
                                    currentTile = Instantiate(level3Chunks[lvl3[0]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                    lvl3.RemoveAt(0);
                                }
                            } else {

                                if (lvl2.Count != 0) {
                                    currentTile = Instantiate(level2Chunks[lvl2[0]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                    lvl2.RemoveAt(0);
                                }

                            }

                            break;
                        case 2:

                            float k = Random.Range(0f, 1f);
                            if (k > 0.30f || lvl1.Count == 0) {

                                if (lvl2.Count != 0) {
                                    currentTile = Instantiate(level2Chunks[lvl2[0]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                    lvl2.RemoveAt(0);
                                }
                            } else {

                                if (lvl1.Count != 0) {
                                    currentTile = Instantiate(level1Chunks[lvl1[0]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                    lvl1.RemoveAt(0);
                                }

                            }

                            break;
                        case 1:

                            if (lvl1.Count != 0) {
                                currentTile = Instantiate(level1Chunks[lvl1[0]], new Vector3((tiles[tiles.Count - 1].transform.position.x + 1), transform.position.y, transform.position.z), Quaternion.identity);
                                lvl1.RemoveAt(0);
                            }

                            break;
                    }

                    length = currentTile.GetComponent<PrefabDescription>().length;
                    spacing = Random.Range(currentTile.GetComponent<PrefabDescription>().spacingMin, currentTile.GetComponent<PrefabDescription>().spacingMax);
                    target = length + spacing;


                    tiles.Add(currentTile);

                    Destroy(tiles[i]);
                    tiles.RemoveAt(i);
                    ResetCounter();

                } else if (counter > length - 1) {

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

        if (endCounter >= 40) {
            return 5;
        } else if (lvl1.Count + lvl2.Count + lvl3.Count == 0) {
            return 4;
        } else if (lvl2.Count < lvl2Length / 3 && lvl1.Count == 0) {
            currentLevel = 3;
            return 3;
        } else if (lvl1.Count < lvl1Length / 2) {
            currentLevel = 2;
            return 2;
        } else {
            currentLevel = 1;
            return 1;
        }
      

    }

    void ResetCounter() {
        counter = 1;
    }

}