using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform player;
    public float speed = 0.1f; 

	void Start () {
	
	}
	
	void Update () {
        transform.Translate(speed, 0, 0, Space.World);
	}
}
