using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnapToGridInParent : MonoBehaviour {

    public float snapValue = 1;
    public float depth = 0;
    private float startPosX;
    private float startPosY;

    void Start() {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

    }

    void Update() {

        float snapInverse = 1 / snapValue;
        float x, y, z;

        // If snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
        // so 1.45 to nearest .5 is 1.5
        x = (Mathf.Round(startPosX + CameraController.xAdd * snapInverse) / snapInverse) - 0.5f;
        y = (Mathf.Round(transform.position.y * snapInverse) / snapInverse) - 0.5f;
        z = depth;  

        transform.position = new Vector3(x, y, z);

    }
}

