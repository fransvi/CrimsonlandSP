using UnityEngine;
using System.Collections;

public class GameCore : MonoBehaviour {

    public float playerCredits;
    public bool[] stagesUnlocked;
    public float[] upgradeLevels;
    float playerLevel;

    float lastLevelScore;
    bool lastLevelWon;
    int lastLevelEnemiesKilled;

    float nextLevelEnemyAmount;
    float nextLevelEnemySpawnRate;
    int nextLevelBGnum;
    bool nextLevelChargers;
    bool nextLevelSnipers;
    bool nextLevelCars;
    int nextCurrentStage;

    int nextEnemiesLeft;

    bool cursorVisible;


    public GameManager gm;

    // Use this for initialization
    void Start () {

        //Load player stats from database smh
        cursorVisible = true;
        DontDestroyOnLoad(transform.gameObject);
        loadMenu();

    }

    public bool[] getStagesUnlocked()
    {
        return stagesUnlocked;
    }
    public void setLastLevelWon(bool b)
    {
        lastLevelWon = b;
    }

    public bool getLastLevelWon()
    {
        return lastLevelWon;
    }

    public void setCursorVisible(bool b)
    {
        cursorVisible = b;
    }

    // Update is called once per frame
    void Update () {

        if (cursorVisible)
        {
            Cursor.visible = true;
        }else
        {
            Cursor.visible = false;
        }
	
	}

    void loadMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void loadLossScreen()
    {
        Application.LoadLevel("VictoryScreen");
    }

    public void recieveUpgradeLevels(float[] lvls)
    {
        playerLevel = 0;
        upgradeLevels = lvls;
        for(int i = 0; i < upgradeLevels.Length; i++)
        {
            playerLevel += upgradeLevels[i];
        }
    }

    public float getPlayerLevel()
    {
        return playerLevel;
    }

    public float[] getUpgradeLevels()
    {
        return upgradeLevels;
    }

    public void recieveCredits(float creds)
    {
        playerCredits += creds;
    }

    public float getPlayerCredits()
    {
        return playerCredits;
    }
    public void preloadLevelData(int levelNum)
    {

        if(levelNum == 5)
        {
            nextCurrentStage = 0; nextLevelEnemyAmount = 50; nextLevelEnemySpawnRate = 0.1f; nextLevelBGnum = 0; nextLevelChargers = false; nextLevelSnipers = false; nextLevelCars = false; nextEnemiesLeft = 50;
        }
        else if(levelNum == 6)
        {
            nextCurrentStage = 1; nextLevelEnemyAmount = 100; nextLevelEnemySpawnRate = 0.1f; nextLevelBGnum = 1; nextLevelChargers = true; nextLevelSnipers = false; nextLevelCars = false; nextEnemiesLeft = 100;
        }
        else if(levelNum == 7)
        {
            nextCurrentStage = 2; nextLevelEnemyAmount = 150; nextLevelEnemySpawnRate = 0.05f; nextLevelBGnum = 2; nextLevelChargers = true; nextLevelSnipers = false; nextLevelCars = true; nextEnemiesLeft = 150;
        }
        else if(levelNum == 8)
        {
            nextCurrentStage = 3; nextLevelEnemyAmount = 250; nextLevelEnemySpawnRate = 0.05f; nextLevelBGnum = 3; nextLevelChargers = true; nextLevelSnipers = true; nextLevelCars = true; nextEnemiesLeft = 250;
        }
    }

    public void startNewLevel()
    {
        gm = FindObjectOfType<GameManager>();
        gm.InitStage(nextCurrentStage, nextLevelEnemyAmount, nextLevelEnemySpawnRate, nextLevelBGnum, nextLevelChargers, nextLevelSnipers, nextLevelCars);
        lastLevelScore = 0;
        lastLevelEnemiesKilled = 0;
    }

    public void lastLevelScoreGained(float scr)
    {
        lastLevelScore += scr;
    }
    public void lastLevelEnemySlain()
    {
        lastLevelEnemiesKilled += 1;
        nextEnemiesLeft -= 1;
    }

    public int getNextEnemiesLeft()
    {
        return nextEnemiesLeft;
    }

    public int getLastLevelEnemiesSlain()
    {
        return lastLevelEnemiesKilled;
    }

    public float getLastLevelScore()
    {
        return lastLevelScore;
    }

    public void setStageUnlocked(int stage)
    {
        for(int i = 0; i < stagesUnlocked.Length; i++)
        {
            if(i == stage)
            {
                stagesUnlocked[i] = true;
            }
        }
    }
}
