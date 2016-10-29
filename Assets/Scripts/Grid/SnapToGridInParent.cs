using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGridInParent : MonoBehaviour {

    public GameObject parent;

    public float snapValue = 1;
    public float offset = 0;
    public float depth = 0;

    private float x_relative_to_parent;
    private float y_relative_to_parent;

    void Start() {
        x_relative_to_parent = transform.position.x - parent.transform.position.x;
        y_relative_to_parent = transform.position.y - parent.transform.position.y;
    }


    void Update() {
        float snapInverse = 1 / snapValue;

        float x, y, z;

        // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
        // so 1.45 to nearest .5 is 1.5
        x = (Mathf.Round(x_relative_to_parent + parent.transform.position.x * snapInverse) / snapInverse) + offset;
        y = (Mathf.Round(y_relative_to_parent + parent.transform.position.y * snapInverse) / snapInverse) + offset;
        z = depth;  // depth from camera

        transform.position = new Vector3(x, y, z);
    }
}
