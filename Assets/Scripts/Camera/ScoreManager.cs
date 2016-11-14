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
<<<<<<< HEAD
    public Text scoreText;
   
=======

    public static int session;
>>>>>>> refs/remotes/origin/EventLogger

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
        Destroy(this);
    }

    static public void ChangeScore(int amount) {
		print ("score: " + amount);
        playerScore += amount;
    }

<<<<<<< HEAD
    void OnGUI()
	{
        scoreText.text = "Score: " + (int)(playerScore);
    }
    
}
=======
    void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + (int)(playerScore));
	}
}
>>>>>>> refs/remotes/origin/EventLogger
