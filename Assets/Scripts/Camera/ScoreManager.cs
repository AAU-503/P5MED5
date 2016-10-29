using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    float playerScore = 0;

	// Update is called once per frame
	void Update () {
        playerScore += Time.deltaTime * 10;
	}

    public void ChangeScore(int amount)
    {
		print ("score: " + amount);
        playerScore += amount;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + (int)(playerScore));
    }
}
