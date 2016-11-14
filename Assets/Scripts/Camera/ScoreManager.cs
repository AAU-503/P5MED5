using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    static public float playerScore = 0;

	static public int coinsScore = 10;
	static public int boxHitScore = 2;
	static public int boxFailScore = -5;
	static public int enemyKillScore = 10;
	static public int enemyFailScore = -5;
	static public int lavaScore = -10;
	static public int bulletScore = -5;
    static public int explosiveHitScore = -10;
    static public int explosiveFailScore = -10;
    public Text scoreText;
   


    // Update is called once per frame
    void Update () {

    }

    static public void ChangeScore(int amount)
    {
		print ("score: " + amount);
        playerScore += amount;
    }

    void OnGUI()
	{
        scoreText.text = "Score: " + (int)(playerScore);
    }
    
}
