using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	private float score;
	public float enemyCount;
	public float spawnRate;
	GameObject stage;
	public GameObject player;
	public GameObject enemyGrunt;
    public GameObject enemyShooter;
    public GameObject enemyCharger;
    public GameObject enemyCar;
    public GameObject spawnPoint;
	private List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject pauseMenu;
	private float timeLeft;
    private float scoreNeededToWin;
    bool spawnPointsReady;
    bool stageWon;
    bool isPaused = false;
    GameCore core;
    private int enemiesKilled;
    public Texture[] stageBackgrounds;
    bool hasChargers;
    bool hasSnipers;
    bool hasCars;
    int bgNum;
    int currentStage;



    // Use this for initialization
    void Start () {
        core = FindObjectOfType<GameCore>();
        stage = GameObject.FindGameObjectWithTag("Stage");
        score = 0;
		timeLeft = spawnRate;
        spawnPointsReady = false;
        stageWon = false;
        generateSpawnPoints();
        core.startNewLevel();


	
	}

    public void InitStage(int current, float eC, float sR, int bGN, bool hC, bool hS, bool hasC)
    {
        currentStage = current;
        enemyCount = eC;
        spawnRate = sR;
        hasChargers = hC;
        hasSnipers = hS;
        hasCars = hasC;
        scoreNeededToWin = enemyCount * 200;
        stage.GetComponent<Renderer>().material.mainTexture = stageBackgrounds[bGN];
    }


    void Awake()
    {
        scoreNeededToWin = enemyCount * 200;
    }
    void generateSpawnPoints()
    {
        float y = 20;
        float x = 30;
        for (int i = 0; i < 8; i++)
        {
            GameObject SPW = Instantiate(spawnPoint, new Vector3(-33.5f, y , 4), stage.transform.rotation) as GameObject;
            spawnPoints.Add(SPW);
            GameObject SPE = Instantiate(spawnPoint, new Vector3(33.5f, y, 4), stage.transform.rotation) as GameObject;
            spawnPoints.Add(SPE);
            GameObject SPN = Instantiate(spawnPoint, new Vector3(x, 22.5f, 4), stage.transform.rotation) as GameObject;
            spawnPoints.Add(SPN);
            GameObject SPS = Instantiate(spawnPoint, new Vector3(x, -22.5f, 4), stage.transform.rotation) as GameObject;
            spawnPoints.Add(SPS);

            x -= 7.5f;
            y -= 5f;
        }
        spawnPointsReady = true;

    }

    // Update is called once per frame
    void Update () {

        if(core.getLastLevelScore() >= scoreNeededToWin)
        {
            stageWon = true;
            loadVictoryScreen();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.gameObject.SetActive(false);
            }

        }



        timeLeft -= Time.deltaTime;
		if(timeLeft < 0 && enemyCount > 0 && spawnPointsReady)
		{
			int random = Random.Range (0, spawnPoints.Count - 1);
			GameObject enemy = Instantiate (enemyGrunt, spawnPoints[random].transform.position, enemyGrunt.transform.rotation) as GameObject;
            random = Random.Range(0, spawnPoints.Count - 1);

            float sniperChance = Random.Range(0f, 100f);
            if (hasSnipers && sniperChance > 85)
            {
                GameObject enemy2 = Instantiate(enemyShooter, spawnPoints[random].transform.position, enemyShooter.transform.rotation) as GameObject;
                enemyCount -= 1;
            }
            float carChance = Random.Range(0f, 100f);
            if (hasCars && carChance > 95)
            {
                GameObject enemy3 = Instantiate(enemyCar, spawnPoints[random].transform.position, enemyShooter.transform.rotation) as GameObject;
                enemyCount -= 1;
            }
            float chargerChance = Random.Range(0f, 100f);
            if (hasSnipers && chargerChance > 85)
            {
                GameObject enemy4 = Instantiate(enemyCharger, spawnPoints[random].transform.position, enemyShooter.transform.rotation) as GameObject;
                enemyCount -= 1;
            }

            timeLeft = spawnRate;
			enemyCount -= 1;
		}
	}



    public void loadVictoryScreen()
    {
        core.setStageUnlocked(currentStage+1);
        core.setLastLevelWon(true);
        Application.LoadLevel("VictoryScreen");
    }

    public void toMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void continueGame()
    {
        isPaused = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public float getEnemyCount()
    {
        return enemyCount;
    }

	void OnGUI()
	{
	}
}
