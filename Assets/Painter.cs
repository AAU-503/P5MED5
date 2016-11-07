using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour {

    public Renderer renderer;
    public Texture2D m_MainTexture;


    // Use this for initialization
    void Start() {
        m_MainTexture = renderer.material.mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update() {
        HitTestUVPosition();


    }


    void HitTestUVPosition() {
        RaycastHit hit;
        Color C = Color.white;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit)) {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null) {
                print("meshCollider is null");
                return;
            }

            Vector2 pixelUV = new Vector2(hit.textureCoord.x * m_MainTexture.width, hit.textureCoord.y * m_MainTexture.height);
            m_MainTexture.SetPixel((int)pixelUV.x, (int)pixelUV.y, new Color(1,1,1,1));
            m_MainTexture.Apply();
            print(C);


        }


    }
}



