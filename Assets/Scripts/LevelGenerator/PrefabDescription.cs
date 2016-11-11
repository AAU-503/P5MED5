using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDescription : MonoBehaviour {

    public int length;
    public int spacingMax;
    public int spacingMin;
    public int spacing;




    // Use this for initialization
    void Start() {
        spacing = Random.Range(spacingMin, spacingMax);
    }

    // Update is called once per frame
    void Update() {

    }
}
