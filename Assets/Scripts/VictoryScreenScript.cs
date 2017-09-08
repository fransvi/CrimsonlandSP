using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour {


    public GameObject menuCamera;
    GameObject GameManager;
    float scoreEarned;
    int enemiesSlain;
    public Text scoreEarnedText;
    public Text enemiesSlainText;
    public Text mainText;
    GameCore core;
    public bool LevelWon;


    // Use this for initialization
    void Start () {
        core = FindObjectOfType<GameCore>();
        GameManager = GameObject.Find("GameManagerObject");
        if (core.getLastLevelWon())
        {
            mainText.text = "Stage Cleared!";
            core.setLastLevelWon(false);
        }else
        {
            mainText.text = "Defeat";
            core.setLastLevelWon(false);
        }

        scoreEarned = core.getLastLevelScore();
        enemiesSlain = core.getLastLevelEnemiesSlain();
        scoreEarnedText.text = "You earned " + scoreEarned + " credits.";
        enemiesSlainText.text = "You killed " + enemiesSlain + " enemies,";
        core.recieveCredits(scoreEarned);

	
	}


	
	// Update is called once per frame
	void Update () {
        menuCamera.transform.Rotate(new Vector3(0.02f, 0.02f, 0));
    }

    public void goToStore()
    {
        Application.LoadLevel("StoreMenu");
    }

    public void Quit()
    {
        Application.LoadLevel("MainMenu");
    }

    public void Continue()
    {
        Application.LoadLevel("LevelSelect");
    }
}
