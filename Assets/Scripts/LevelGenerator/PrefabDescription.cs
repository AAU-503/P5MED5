using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDescription : MonoBehaviour {

    public int length;
    public int spacingMax;
    public int spacingMin;
    public int spacing;

    public int instance;


    // Use this for initialization
    void Start() {
        ChunkLogger.instance++;
        instance = ChunkLogger.instance;

        spacing = Random.Range(spacingMin, spacingMax);
    }

    // Update is called once per frame
    void Update() {

    }
}
