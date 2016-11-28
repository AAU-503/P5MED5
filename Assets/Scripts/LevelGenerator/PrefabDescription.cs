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
        ChunkMemory.instance++;
        instance = ChunkMemory.instance;

        spacing = Random.Range(spacingMin, spacingMax);
    }
}
