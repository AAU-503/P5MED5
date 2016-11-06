using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLogger : MonoBehaviour {

    public GameObject player;
    private GameObject quad;
    private GameObject[,] posMem;

    public int chunkHeight;
    public int chunkWidth;

    private const float detectionThreshold = 0.2f;
    private const float margin = 0.01f;
    private const float resolution = 4.0f;

    // Use this for initialization
    void Start() {
        posMem = new GameObject[(int)(chunkWidth * resolution), (int)(chunkHeight * resolution)];

        quad = Resources.Load<GameObject>("Debug/Quad");
        quad.transform.localScale = new Vector3((1.0f / resolution) - margin, (1.0f / resolution) - margin, 1);

        for (float j = 0; j < chunkHeight; j += 1 / resolution) {
            for (float i = 0; i < chunkWidth; i += 1 / resolution) {
                posMem[(int)(i * resolution), (int)(j * resolution)] = GameObject.Instantiate(quad,
                    new Vector3(transform.position.x + i - (quad.transform.localScale.x + margin) * (resolution - 1) / 2,
                    transform.position.y + j - (quad.transform.localScale.y + margin) * (resolution - 1) / 2, transform.position.z), Quaternion.identity);
            }
        }
    }

    int index_x;
    int index_y;
    
    // Update is called once per frame
    void Update() {

        //if (player.transform.position.x > transform.position.x) {
            //index_x = (int)((player.transform.position.x - transform.position.x) * resolution);
            //index_y = (int)((player.transform.position.y - transform.position.y) * resolution);
        //}

        //posMem[index_x, index_y].GetComponent<Renderer>().material.color = Color.green;


        print(player.transform.position.x - transform.position.x);

        if (player != null) {
            for (int j = 0; j < chunkHeight * resolution; j += 1) {
                for (int i = 0; i < chunkWidth * resolution; i += 1) {
                    if (posMem[i, j].transform.position.x < player.transform.position.x + detectionThreshold && posMem[i, j].transform.position.x > player.transform.position.x - detectionThreshold
                        && posMem[i, j].transform.position.y < player.transform.position.y + detectionThreshold && posMem[i, j].transform.position.y > player.transform.position.y - detectionThreshold) {
                        posMem[i, j].GetComponent<Renderer>().material.color = new Vector4(0f, 255f, 0f, 0.1f);
                    }
                }
            }
        }
    }
}

