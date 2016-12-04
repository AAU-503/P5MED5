using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    public Vector3 direction;
    public float speed = 5.0f;
    public float acceleration = -2;
    public float startTime;
    public float lifeTime = 5.0f;
    public float normOffset;
    public float offsetPos;
    private bool isDestroyed;
    public ParticleSystem particle;
    public GameObject drone;
    private Renderer[] renderes;

    // Use this for initialization
    public void Init(GameObject drone) {
        this.drone = drone;
        renderes = GetComponentsInChildren<Renderer>();

        startTime = Time.time;
        offsetPos = (transform.position.x - GameObject.FindWithTag("Player").GetComponent<Transform>().position.x) / 2;

        if (drone.GetComponent<EnemyBehavior>().isMovingY) {
            renderes[0].material.SetColor("_TintColor", new Color(0.1f, 0, 0, 1));
            renderes[1].material.SetColor("_TintColor", new Color(1, 0, 0, 1));
            renderes[2].material.SetColor("_TintColor", new Color(1, 0, 0, 1));
            renderes[3].material.SetColor("_TintColor", new Color(1, 0, 0, 1));

            direction = Vector3.Normalize(new Vector3(-1, 0, 0));

        } else {
            direction = Vector3.Normalize(new Vector3(-1, -1, 0));
        }
    }

    // Update is called once per frame
    void Update() {

        if (Time.time > startTime + lifeTime) {
            Destroy(gameObject);
        }

        if (isDestroyed) {

            foreach (Renderer element in renderes) {
                element.enabled = false;
            }

        } else {


            if (acceleration <= 1) {
                acceleration += 2 * Time.deltaTime;
            }

            if (acceleration > 0)
                transform.position += direction * speed * acceleration * Time.deltaTime;
        }
    }

    public void Destroy() {

        isDestroyed = true;

    }

    public void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.tag != "Enemy" && hit.gameObject.tag != "Hitbox") {

            foreach (Renderer element in renderes) {
                element.enabled = false;
            }

            GetComponent<BoxCollider>().enabled = false;
        }
    }

} 
