using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLogger : MonoBehaviour {

    private GameObject player;
    private GameObject quad;
    public GameObject[,] posMem;
    public GameObject[,] objMem;

    public int chunkHeight;
    public int chunkWidth;
    private bool inside;
    public static bool currentChunk;
    private const float margin = 0.01f;
    private const float resolution = 1.0f;

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player");

        posMem = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];
        objMem = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];

        //quad = Resources.Load<GameObject>("Debug/Quad");
        //quad.transform.localScale = new Vector3((1.0f / resolution) - margin, (1.0f / resolution) - margin, 1);

        //for (float j = 0; j < chunkHeight; j += 1 / resolution) {
        //    for (float i = 0; i < chunkWidth; i += 1 / resolution) {
        //        posMem[(int)(i * resolution), (int)(j * resolution)] = Instantiate(quad,
        //            new Vector3(transform.position.x + i - (quad.transform.localScale.x + margin) * (resolution - 1) / 2,
        //            transform.position.y + j - (quad.transform.localScale.y + margin) * (resolution - 1) / 2, transform.position.z + -2), Quaternion.identity);
        //    }
        //}
    }

    // Update is called once per frame
    void Update() {
        Check();
    }

    public void Connect(GameObject obj) {
        //print("Length x: " + objMem.GetLength(0));

        //print("Length y: " + objMem.GetLength(1));

        objMem[(int)obj.GetComponent<ChunkConnector>().startPos.x, (int)obj.GetComponent<ChunkConnector>().startPos.y] = obj;
    }

    public bool attack;

    // Check if the player is inside a chunk. 
    void Check() {
        if (!currentChunk && player.transform.position.x - transform.position.x > - 1 && player.transform.position.x - transform.position.x < chunkWidth) {
            Exporter.setChunk(posMem);
            Exporter.CreateMemory();
            currentChunk = true;
            Exporter.instance++;
            print(currentChunk);
        } else if (currentChunk && player.transform.position.x - transform.position.x > chunkWidth && player.transform.position.x - transform.position.x < chunkWidth + 1) {
            Exporter.WriteToCSV();
            currentChunk = false;

            //for (int j = 0; j < posMem.GetLength(1); j++) {
            //    for (int i = 0; i < posMem.GetLength(0); i++) {
            //        Destroy(posMem[i, j]); 
            //    }
            //}

        } else if (attack && !currentChunk) {
            currentChunk = true;
            Exporter.setChunk(posMem);
            Exporter.CreateMemory();
            Exporter.instance++;
        }
    }

    public void LogTile(GameObject tile, GameObject chunk, int result, Vector3 tilePos, string attribute) {
        /* Should log the instance correctly. Right now if the player is hit by a bullet, the memory is cleared and the instance will increment. We should assign the instance to the
           chunk with an unique identifier if the identifier has not already been assigned an instance number. Else we use the old instance number */  
        
        //If not current tile 
            Exporter.setChunk(posMem);
            Exporter.CreateMemory();
            currentChunk = true;
            Exporter.instance++;
            Exporter.LogTile(tile, chunk, result, tilePos, attribute);
        
    }
}

