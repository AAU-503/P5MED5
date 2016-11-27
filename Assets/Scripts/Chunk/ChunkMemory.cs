using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information from objects obtained through <see cref="TileChunkBridge"/> and eventually passes it to <see cref="Exporter"/>.  
/// </summary>
public class ChunkMemory : MonoBehaviour {

    private GameObject player;
    private GameObject quad;
    public GameObject[,] quadContainer;
    public GameObject[,] objMem;

    public static int instance;

    public int chunkHeight;
    public int chunkWidth;
    private bool check = false;
    private static bool debug = true;

    private const float margin = 0.01f;
    private const float resolution = 1.0f;

    void Start() {
        player = GameObject.FindWithTag("Player");
        objMem = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];

        if (debug)
            ShowDebug();
    }

    // Update is called once per frame
    void Update() {
        CheckPlayerExit();

        if (CheckPlayerExit() && !check) {
            LogTile();
            check = !check;
        }
    }

    public void UpdateMemory(GameObject obj) {

        // Load object into memory.
        objMem[(int)obj.GetComponent<TileChunkBridge>().startPos.x, (int)obj.GetComponent<TileChunkBridge>().startPos.y] = obj;

        if (debug) {
            if (obj.GetComponent<TileChunkBridge>().state == 1) {
                quadContainer[(int)obj.GetComponent<TileChunkBridge>().startPos.x, (int)obj.GetComponent<TileChunkBridge>().startPos.y].GetComponent<Renderer>().material.color = new Vector4(0f, 2f, 0f, 0.1f);
            }

            if (obj.GetComponent<TileChunkBridge>().state == -1) {
                quadContainer[(int)obj.GetComponent<TileChunkBridge>().startPos.x, (int)obj.GetComponent<TileChunkBridge>().startPos.y].GetComponent<Renderer>().material.color = new Vector4(2f, 0f, 0f, 0.1f);
            }
        }
    }

    public bool attack;

    /// <summary>
    /// Check if the player has exited the chunk and return a boolean. 
    /// </summary>
    bool CheckPlayerExit() {
        if (player.transform.position.x - transform.position.x > chunkWidth + 1) {

            if (debug) {
                for (int j = 0; j < quadContainer.GetLength(1); j++) {
                    for (int i = 0; i < quadContainer.GetLength(0); i++) {
                        Destroy(quadContainer[i, j]);
                    }
                }
            }
            return true;
        }
        return false;
    }


    public void LogTile() {
            Exporter.Set(gameObject);
    }

    // Draw quads representing the memory in front of each chunk. 
    void ShowDebug() {
        quadContainer = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];

        quad = Resources.Load<GameObject>("Debug/Quad");
        quad.transform.localScale = new Vector3((1.0f / (resolution * 2)) - margin, (1.0f / (resolution * 2)) - margin, (1.0f / (resolution * 2)) - margin);

        for (float j = 0; j < chunkHeight; j += 1 / resolution) {
            for (float i = 0; i < chunkWidth; i += 1 / resolution) {
                quadContainer[(int)(i * resolution), (int)(j * resolution)] = Instantiate(quad,
                    new Vector3(transform.position.x + i - (quad.transform.localScale.x + margin) * (resolution - 1) / 2,
                    transform.position.y + j - (quad.transform.localScale.y + margin) * (resolution - 1) / 2, transform.position.z + -2), Quaternion.identity);
            }
        }
    }
}

