using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    static float playerScore = 0;

	static public int coinsScore = 10;
	static public int boxHitScore = 2;
	static public int boxFailScore = -5;
	static public int enemyKillScore = 10;
	static public int enemyFailScore = -10;
	static public int lavaScore = -15;


	// Update is called once per frame
	void Update () {
        playerScore += Time.deltaTime * 10;
	}

    static public void ChangeScore(int amount)
    {
		print ("score: " + amount);
        playerScore += amount;
    }

    void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + (int)(playerScore));
	}
    
}
