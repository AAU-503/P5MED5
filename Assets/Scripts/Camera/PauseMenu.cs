using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Transform canvas;
    public PlayerBehavior player;
    public CameraController camera;

        // Update is called once per frame
        void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(true);
                player.enabled = false;
                camera.enabled = false;
                Time.timeScale = 0;
            } else
            {
                canvas.gameObject.SetActive(false);
                player.enabled = true;
                camera.enabled = true;
                Time.timeScale = 1;
            }
        }

        
	}
    public void OnClickResume()
    {
        canvas.gameObject.SetActive(false);
        player.enabled = true;
        camera.enabled = true;
        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        canvas.gameObject.SetActive(false);
        player.enabled = true;
        camera.enabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
