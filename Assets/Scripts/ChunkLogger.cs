using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLogger : MonoBehaviour {

    public GameObject player;
    private GameObject quad;
    private GameObject[,] posMem;

    public int chunkHeight;
    public int chunkWidth;
    private bool inside;
    public static bool insideCheck;
    private const float margin = 0.01f;
    private const float resolution = 1.0f;

    // Use this for initialization
    void Start() {
        posMem = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];

        quad = Resources.Load<GameObject>("Debug/Quad");
        quad.transform.localScale = new Vector3((1.0f / resolution) - margin, (1.0f / resolution) - margin, 1);

        for (float j = 0; j < chunkHeight; j += 1 / resolution) {
            for (float i = 0; i < chunkWidth; i += 1 / resolution) {
                posMem[(int)(i * resolution), (int)(j * resolution)] = Instantiate(quad,
                    new Vector3(transform.position.x + i - (quad.transform.localScale.x + margin) * (resolution - 1) / 2,
                    transform.position.y + j - (quad.transform.localScale.y + margin) * (resolution - 1) / 2, transform.position.z + -2), Quaternion.identity);
            }
        }
    }
    
    // Update is called once per frame
    void Update() {
        Check();
    }

    // Check if the player is inside a chunk. 
    void Check() {

        if (player.transform.position.x - transform.position.x > 0 && player.transform.position.x - transform.position.x < chunkWidth && !inside) {

            // This is stupid :P
            inside = true;
            insideCheck = true;
            Exporter.setChunk(posMem);
            Exporter.instance++;
        } else if (player.transform.position.x - transform.position.x > chunkWidth && inside) {
            inside = false;
            insideCheck = false;
        }
    }
}

