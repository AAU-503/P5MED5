using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public string scene;

    public void OnClickPlay()
    {
        //Application.LoadLevel("MainScene");
        SceneManager.LoadScene(scene);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
