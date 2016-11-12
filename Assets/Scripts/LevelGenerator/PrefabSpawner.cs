using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    public GameObject prefab;

    void Awake() {
        GameObject go = Instantiate(prefab, transform.position, transform.rotation);
        go.transform.parent = transform.parent;
    }
}