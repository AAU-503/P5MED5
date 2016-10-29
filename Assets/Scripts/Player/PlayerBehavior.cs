using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public CameraController cameraController;
    public ScoreManager scoreManager;

    private bool attacked = true;

    // Use this for initialization
    void Start() {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(cameraController.speed, 0, 0);
    }

    void OnTriggerEnter(Collider other) { // Ability to pick up coins adding to score in ScoreManager and attacking boxes
        if (other.gameObject.CompareTag("Coin")) {
            other.gameObject.SetActive(false);
            scoreManager.ChangeScore(1);
        }
        if (other.isTrigger != true && other.CompareTag("Box")) {
            print(other.gameObject);
            other.SendMessageUpwards("Attacked", attacked);
        }
    }
}