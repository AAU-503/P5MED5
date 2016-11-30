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

    public static int session;
    public Text levelText;
    public TileManager tileManager;

    void Awake() {
        // Check For 'TimesLaunched', Set To 0 If Value Isnt Set (First Time Being Launched)
        session = PlayerPrefs.GetInt("TimesLaunched", 0);

        // After Grabbing 'TimesLaunched' we increment the value by 1
        session = session + 1;

        // Set 'TimesLaunched' To The Incremented Value
        PlayerPrefs.SetInt("TimesLaunched", session);

        // Now I Would Destroy The Script Or Whatever You
        // Want To Do To Prevent It From Running Multiple
        // Times In One Launch Session

    }

    static public void ChangeScore(int amount) {
        playerScore += amount;
    }

    void Update () {
        scoreText.text = "Score: " + (int)(playerScore);
    }


    void OnGUI()
	{
        scoreText.text = "Score: " + (int)(playerScore);
        levelText.text = "Level: " + TileManager.currentLevel;
    }
    
}

